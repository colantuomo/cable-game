using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _targetPoint;
    private Tween _tween;
    [SerializeField]
    private float _movingSpeed = 5f;
    [SerializeField]
    private LoopType _loopType = LoopType.Restart;

    private void Start()
    {
        _tween = transform.DOMove(_targetPoint.position, _movingSpeed).SetLoops(-1, _loopType);
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    print($"Collision Enter - {other.name}");
        //    other.transform.SetParent(transform);
        //}
    }

    private void OnTriggerExit(Collider other)
    {

        //if (other.CompareTag("Player"))
        //{
        //    print($"Collision Exit - {other.name}");
        //    other.transform.SetParent(null);
        //}
    }
}
