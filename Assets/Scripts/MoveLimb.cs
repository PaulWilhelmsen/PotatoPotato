using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveLimb : MonoBehaviour
{
    public float Speed = 1000f;
    public float MotorTimer = 1f;
    private bool _motorOn = false;
    private HingeJoint2D _hinge;

    public void Start()
    {
        _hinge = gameObject.GetComponent<HingeJoint2D>();
    }
    
    public void Execute()
    {
        print($"Moving limb {gameObject.name}");
        StartCoroutine("UseMotor");
    }

    public IEnumerator UseMotor()
    {
        print($"Starting corutine for {gameObject.name} motorStatus: {_motorOn}");
        if (_motorOn)
            yield break;
        
        _motorOn = true;
        _hinge.useMotor = true;
        yield return new WaitForSeconds(MotorTimer);
        _hinge.useMotor = false;
        _motorOn = false;
    }
}
