using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    [SerializeField]
    private Transform _startPoint, _endingPoint;

    private void Start()
    {
        //gameObject.SetActive(false);
    }
    public void SetStartPoint(Vector3 position)
    {
        _startPoint.position = position;
    }

    public void SetEndingPoint(Vector3 position)
    {
        _endingPoint.SetParent(_startPoint);
        _endingPoint.position = position;
    }

    public void HoldCable(Transform parent)
    {
        gameObject.SetActive(true);
        _endingPoint.SetParent(parent);
        _endingPoint.position = parent.position;
    }

    public void ReleaseCable()
    {
        _endingPoint.SetParent(_startPoint);
    }
}
