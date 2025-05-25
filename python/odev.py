import cv2
import mediapipe as mp
import socket
import time

mp_face = mp.solutions.face_mesh
face = mp_face.FaceMesh(static_image_mode=False, max_num_faces=1)

udp_ip = "127.0.0.1"
udp_port = 5005
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

def detect_smile(landmarks):
    mouth_left = landmarks[61]
    mouth_right = landmarks[291]
    top_lip = landmarks[13]
    bottom_lip = landmarks[14]
    mouth_width = abs(mouth_right.x - mouth_left.x)
    mouth_open = abs(top_lip.y - bottom_lip.y)
    return mouth_open / mouth_width > 0.1

cap = cv2.VideoCapture(0)

while cap.isOpened():
    success, frame = cap.read()
    if not success:
        continue
    rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    results = face.process(rgb)

    if results.multi_face_landmarks:
        for face_landmarks in results.multi_face_landmarks:
            landmarks = face_landmarks.landmark
            if detect_smile(landmarks):
                sock.sendto(b"smile", (udp_ip, udp_port))
            else:
                sock.sendto(b"no_smile", (udp_ip, udp_port))

    time.sleep(0.1)

cap.release()
