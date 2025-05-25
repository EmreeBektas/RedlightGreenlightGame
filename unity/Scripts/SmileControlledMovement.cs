using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.SceneManagement;

public class SmileControlledMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool isSmiling = false;
    private bool hasLost = false;

    private Thread listenerThread;
    private UdpClient udpClient;
    private AudioSource gameAudio;

    public GameObject loseText; // UI'dan atanacak "Kaybettin" yazısı

    void OnEnable()
    {
        StartSmileListener();
    }

    void OnDisable()
    {
        StopSmileListener();
    }

    void Start()
    {
        gameAudio = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hasLost) return;

        bool isMusicPlaying = gameAudio.isPlaying;

        if (isSmiling && !isMusicPlaying)
        {
            Debug.Log("Kırmızı ışıkta gülme tespit edildi! Kaybettin...");
            hasLost = true;
            if (loseText != null) loseText.SetActive(true);
            Invoke("RestartScene", 2f);
        }

        if (isSmiling && isMusicPlaying)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void StartSmileListener()
    {
        listenerThread = new Thread(ListenForSmile);
        listenerThread.IsBackground = true;
        listenerThread.Start();
    }

    void StopSmileListener()
    {
        if (listenerThread != null && listenerThread.IsAlive)
        {
            listenerThread.Abort();
            listenerThread = null;
        }
        if (udpClient != null)
        {
            udpClient.Close();
            udpClient = null;
        }
    }

    void ListenForSmile()
    {
        udpClient = new UdpClient(5005);
        while (true)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5005);
                byte[] data = udpClient.Receive(ref endPoint);
                string message = Encoding.UTF8.GetString(data);
                isSmiling = message.Trim().ToLower() == "smile";
            }
            catch { }
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnApplicationQuit()
    {
        StopSmileListener();
    }
}
