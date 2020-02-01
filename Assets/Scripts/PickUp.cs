using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private HingeJoint2D _joint;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _joint = GetComponent<HingeJoint2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Hit with {other.gameObject.name} where tag is {other.gameObject.tag}");
        if (other.gameObject.tag != "JointSpot") return;

        if(other.GetComponent<JointData>().IsTaken)
            return;
        
        SetNewPosition(other.transform.position);
        ConnectJoint(other.gameObject);
    }

    private void SetNewPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    private void ConnectJoint(GameObject connectTo)
    {
        var jointData = connectTo.GetComponent<JointData>();
        print($"{gameObject.name} has bool on collition with {connectTo.name} | Joint is {jointData.IsTaken}");

        _joint.connectedBody = connectTo.GetComponent<Rigidbody2D>();
        _joint.limits = jointData.Limits();
        
        jointData.SetIsTaken(true);
        jointData.SetAttachment(gameObject);
        gameObject.transform.parent = connectTo.transform;
    }
}