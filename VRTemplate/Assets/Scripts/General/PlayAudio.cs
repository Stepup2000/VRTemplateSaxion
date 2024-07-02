using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSimplified : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    public void PlayAudio()
    {
        SoundManager.Instance.PlaySoundAtLocation(audioClip, transform.position);
    }
}
