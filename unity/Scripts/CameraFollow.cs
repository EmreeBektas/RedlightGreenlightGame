using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // takip edilecek obje
    public Vector3 offset = new Vector3(0f, 5f, -7f); // konum farký

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
