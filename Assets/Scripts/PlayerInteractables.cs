using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractables : MonoBehaviour
{
    [SerializeField]
    private float sphereRadius = 1f;
    private CableManager _currentHoldCable;
    private ConnectionPoint _lastConnectionPoint;
    private PassiveConnectionPoint _lastPassiveConnectionPoint;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump"))
        {
            Vector3 spherePosition = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Connection"));
            if (hitColliders.Length == 0) return;

            var hitCollider = hitColliders[0];
            if (IsHoldingACable())
            {
                PassiveConnectionPoint passiveConnectionPoint = hitCollider.GetComponent<PassiveConnectionPoint>();
                if (passiveConnectionPoint != null && !passiveConnectionPoint.IsUsed())
                {
                    _currentHoldCable.HoldCable(passiveConnectionPoint.transform);
                    passiveConnectionPoint.Connect(_lastConnectionPoint);
                    _lastPassiveConnectionPoint = passiveConnectionPoint;
                    GameManager.Singleton.ConnectCable(_currentHoldCable);
                    _currentHoldCable = null;
                    return;
                }
                //_currentHoldCable.ReleaseCable();
                //_currentHoldCable = null;
                return;
            }

            if (hitColliders != null)
            {
                ConnectionPoint connectionPoint = hitCollider.GetComponent<ConnectionPoint>();
                if (connectionPoint != null)
                {
                    if (_lastPassiveConnectionPoint != null && _lastConnectionPoint == connectionPoint)
                    {
                        _lastPassiveConnectionPoint.Disconnect();
                        GameManager.Singleton.DisconnectCable();
                    }

                    if (connectionPoint.IsConnected())
                    {
                        //print("This is a already connected point");
                        connectionPoint.DisconnectFromOriginPoint();
                        GameManager.Singleton.DisconnectCable();
                    }

                    _currentHoldCable = connectionPoint.HoldCable();
                    _lastConnectionPoint = connectionPoint;
                    return;
                }
            }
        }
    }

    private bool IsHoldingACable()
    {
        return _currentHoldCable != null;
    }
}
