using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWinTrigger : MonoBehaviour
{
    public GameObject winTextObject;
    public SmileControlledMovement movementScript;
   // public AudioSource gameMusic; // EKLEDİK
    public GameObject GameManagera;

    void Start()
    {
        if (movementScript == null)
        {
            movementScript = GetComponent<SmileControlledMovement>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            winTextObject.SetActive(true);
            movementScript.enabled = false;
            GameManagera.SetActive(false); // MÜZİĞİ DURDURDUK ✅
        }
    }
}
