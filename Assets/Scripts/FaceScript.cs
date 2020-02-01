using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceScript : MonoBehaviour
{
    public float parentRotationZ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var parent = gameObject.transform.parent;
        parentRotationZ = gameObject.transform.parent.gameObject.transform.rotation.z;
        transform.SetPositionAndRotation(parent.position, new Quaternion(parent.rotation.x, parent.rotation.y, parentRotationZ, parent.rotation.w));
    }
}
