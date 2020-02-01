using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointData : MonoBehaviour
{
    public Enums.JointSpot Spot;
    public bool IsTaken { get; private set; }
    private GameObject _currentAttachment;
    public float MaxLimit;
    public float MinLimit;
    public float ZRotation;

    public JointAngleLimits2D Limits()
    {
        return new JointAngleLimits2D() {max = MaxLimit, min = MinLimit};
    }

    public void Awake()
    {
        IsTaken = false;
    }

    public void SetIsTaken(bool setTaken)
    {
        IsTaken = setTaken;
    }

    public void SetAttachment(GameObject attach)
    {
        _currentAttachment = attach;
    }

    public void MoveLimb()
    {
        if (_currentAttachment != null)
            _currentAttachment.GetComponent<MoveLimb>().Execute();
    }
}