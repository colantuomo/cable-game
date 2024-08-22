using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTableManager : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    public void AttachCableToPlayer(CableManager cable)
    {
        cable.HoldCable(_player);
    }
}
