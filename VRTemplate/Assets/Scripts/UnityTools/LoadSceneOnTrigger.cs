using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void OnTriggerEnter(Collider other)
    {
        LevelManager.Instance.LoadLevel(_sceneName);
    }
}
