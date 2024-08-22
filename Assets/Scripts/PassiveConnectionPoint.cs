using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveConnectionPoint : MonoBehaviour
{
    private ConnectionPoint PointConnected { set; get; }
    public bool IsUsed()
    {
        return PointConnected != null;
    }

    public void Connect(ConnectionPoint connectionPoint)
    {
        PointConnected = connectionPoint;
        connectionPoint.Connect(this);
    }

    public void Disconnect()
    {
        PointConnected.Disconnect();
        PointConnected = null;
    }
}
