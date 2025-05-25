using UnityEngine;
using System.Collections;

public class LightCycleManager : MonoBehaviour
{
    private AudioSource audioSource;

    public float greenLightDuration = 5f; // m�zik �alacak s�re
    public float redLightDuration = 3f;   // m�zik duracak s�re

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(LightCycle());
    }

    IEnumerator LightCycle()
    {
        while (true)
        {
            // YE��L I�IK: M�zik �als�n
            audioSource.Play();
            Debug.Log("Ye�il I��k!");
            yield return new WaitForSeconds(greenLightDuration);

            // KIRMIZI I�IK: M�zik dursun
            audioSource.Stop();
            Debug.Log("K�rm�z� I��k!");
            yield return new WaitForSeconds(redLightDuration);
        }
    }
}
