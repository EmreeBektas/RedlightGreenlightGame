using UnityEngine;
using System.Collections;

public class LightCycleManager : MonoBehaviour
{
    private AudioSource audioSource;

    public float greenLightDuration = 5f; // müzik çalacak süre
    public float redLightDuration = 3f;   // müzik duracak süre

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(LightCycle());
    }

    IEnumerator LightCycle()
    {
        while (true)
        {
            // YEÞÝL IÞIK: Müzik çalsýn
            audioSource.Play();
            Debug.Log("Yeþil Iþýk!");
            yield return new WaitForSeconds(greenLightDuration);

            // KIRMIZI IÞIK: Müzik dursun
            audioSource.Stop();
            Debug.Log("Kýrmýzý Iþýk!");
            yield return new WaitForSeconds(redLightDuration);
        }
    }
}
