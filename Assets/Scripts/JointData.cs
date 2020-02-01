using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointData : MonoBehaviour
{
    public Enums.JointSpot Spot;
    private bool _isTaken;
    public float MaxLimit;
    public float MinLimit;

    public JointLimits Limits()
    {
        return new JointLimits {max = MaxLimit, min = MinLimit} ;
    }

    public void Awake()
    {
        _isTaken = false;
    }

    public void SetIsTaken(bool setTaken)
    {
        _isTaken = setTaken;
    }
}
