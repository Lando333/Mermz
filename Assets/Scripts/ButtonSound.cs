    using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip pressSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = AudioManager.instance.audioSource;
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
