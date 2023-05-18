using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronPatrol : Patrol
{
    [SerializeField] private Detector _detector;

    private void OnEnable()
    {
        _detector.Detected += OnDetected;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnDetected;
    }

    private void OnDetected()
    {
        ForbidPatrol();
    }
}
