using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableConnection : MonoBehaviour
{
    private List<Transform> _cablePoints;
    private LineRenderer _cable;
    void Start()
    {
        _cablePoints = GetCablePositions();
        _cable = GetComponent<LineRenderer>();
        _cable.positionCount = _cablePoints.Count;
    }

    void Update()
    {
        SetCableRender();
    }

    private List<Transform> GetCablePositions()
    {
        List<Transform> childrens = new();
        childrens.Add(transform);
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            childrens.Add(child);
        }
        return childrens;
    }

    void SetCableRender()
    {
        var cables = _cablePoints.ConvertAll(transform => transform.position).ToArray();
        _cable.SetPositions(cables);
    }
}
