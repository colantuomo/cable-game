using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    [SerializeField]
    private CableManager _cableManager;
    [SerializeField]
    private ConnectionTableManager _connectionTableManager;
    private PassiveConnectionPoint PassiveConnectionPoint { get; set; }

    public CableManager HoldCable()
    {
        _connectionTableManager.AttachCableToPlayer(_cableManager);
        return _cableManager;
    }

    public void ReleaseCable()
    {
        _cableManager.ReleaseCable();
    }

    public void Connect(PassiveConnectionPoint passiveConnectionPoint)
    {
        PassiveConnectionPoint = passiveConnectionPoint;
    }

    public void Disconnect()
    {
        PassiveConnectionPoint = null;
    }

    public bool IsConnected()
    {
        return PassiveConnectionPoint != null;
    }

    public void DisconnectFromOriginPoint()
    {
        PassiveConnectionPoint.Disconnect();
    }
}
