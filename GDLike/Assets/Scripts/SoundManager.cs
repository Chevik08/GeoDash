using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] private AudioClip _hover;
    [SerializeField] private AudioClip _clicked;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audio.PlayOneShot(_hover);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (TryGetComponent<AudioSource>(out AudioSource audio))
        {
            audio.PlayOneShot(_clicked);
        }
    }
}
