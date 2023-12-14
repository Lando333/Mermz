using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;
    public AudioClip pressSound;
    private AudioSource audioSource;

    void Start()
    {
        // Ensure there is an AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void OnButtonClick()
    {
        audioSource.PlayOneShot(pressSound);
    }
}
