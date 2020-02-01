using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    private List<GameObject> _jointSpots;
    
    // Start is called before the first frame update
    void Start()
    {
        _jointSpots = new List<GameObject>();
        var jointSpots = FindObjectsOfType<JointData>();
        print($"joint spots found {jointSpots.Length}");
        foreach (var jointSpot in jointSpots)
        {
            print("Adding joint spot");
            _jointSpots.Add(jointSpot.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Input();
    }

    private void Input()
    {
        if (UnityEngine.Input.GetKey(KeyCode.A))
            MoveLimbs(Enums.JointSpot.TopLeft);

        if (UnityEngine.Input.GetKey(KeyCode.S))
            MoveLimbs(Enums.JointSpot.TopRight);

        if (UnityEngine.Input.GetKey(KeyCode.D))
            MoveLimbs(Enums.JointSpot.DownRight);

        if (UnityEngine.Input.GetKey(KeyCode.F))
            MoveLimbs(Enums.JointSpot.DownLeft);
        
        FindObjectOfType<FaceScript>().SwitchFace(Enums.FaceType.Happy);
    }

    public void MoveLimbs(Enums.JointSpot joint)
    {
        var getJoint = _jointSpots.SingleOrDefault(spot => spot.GetComponent<JointData>().Spot == joint);
        print($"Executing joint {getJoint?.name}");
        
        if (getJoint == null)
            return;
        
        getJoint.GetComponent<JointData>().MoveLimb();
    }
}
