using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlaySoundAtLocation(_audioClip, transform.position);
    }
}
