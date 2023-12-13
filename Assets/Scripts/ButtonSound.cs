using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip pressSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter()
    {
        StartCoroutine(PlayThenGo());
    }
    IEnumerator PlayThenGo()
    {
        audioSource.PlayOneShot(hoverSound);
        yield return new WaitForSeconds(0.3f);
    }

    public void OnButtonClick()
    {
        audioSource.PlayOneShot(pressSound);
    }
}