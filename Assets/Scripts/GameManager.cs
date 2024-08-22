using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InterfaceManager _interfaceManager;
    private SoundManager _soundManager;
    public static GameManager Singleton { get; private set; }
    private int _cableConnecteds;
    [SerializeField]
    private int _totalCablesToConnect, _nextLevelNumber;

    [SerializeField]
    private Transform _levelOutDoor;
    [SerializeField]
    private PlayerController _playerController;

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
        _interfaceManager.LoadScene();
    }

    private void Start()
    {
        _interfaceManager.UpdateCableText(0, _totalCablesToConnect);
        _soundManager = GetComponent<SoundManager>();
    }

    public event Action<CableManager> OnConnectCable;
    public void ConnectCable(CableManager cable)
    {
        _cableConnecteds++;
        OnConnectCable?.Invoke(cable);
        _interfaceManager.UpdateCableText(_cableConnecteds, _totalCablesToConnect);
        _soundManager.PlayCableConnected();
        if (_cableConnecteds >= _totalCablesToConnect)
        {
            OpenOutDoor();
        }
    }

    public event Action OnDisconnectCable;
    public void DisconnectCable()
    {
        _cableConnecteds--;
        OnDisconnectCable?.Invoke();
        _interfaceManager.UpdateCableText(_cableConnecteds, _totalCablesToConnect);

    }

    public void OpenOutDoor()
    {
        _soundManager.PlayLevelPassed();
        _levelOutDoor.DOMoveY(2.5f, 3f);
    }

    public void LoadNextLevel()
    {
        DOTween.KillAll();
        _playerController.enabled = false;
        _interfaceManager.FadeScreenToBlack().OnComplete(() =>
        {
            if (_nextLevelNumber >= 5)
            {
                SceneManager.LoadScene("FinalMenu");
                return;
            }
            SceneManager.LoadScene("Level" + _nextLevelNumber);
        });
    }

    public void RestartLevel()
    {
        _interfaceManager.FadeScreenToBlack(1f).OnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

}
