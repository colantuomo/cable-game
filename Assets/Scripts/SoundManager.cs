using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _cableConnected, _levelPassed, _cableGrab;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCableConnected()
    {
        _audioSource.PlayOneShot(_cableConnected);
    }

    public void PlayCableGrab()
    {
        _audioSource.PlayOneShot(_cableGrab);
    }

    public void PlayLevelPassed()
    {
        _audioSource.PlayOneShot(_levelPassed);
    }
}
