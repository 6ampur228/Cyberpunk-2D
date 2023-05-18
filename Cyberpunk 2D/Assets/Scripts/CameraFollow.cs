using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _movementSpeed; 

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 targetCameraPosition = _target.position + _offset;

        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, _movementSpeed * Time.deltaTime);
    }
}
