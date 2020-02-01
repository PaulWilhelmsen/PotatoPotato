using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public GameObject targetPlayer;
    float nextTimeToSearch = 0;
    public float zoomSpeed = 0.5f;
    public float x;
    public float y;
    public float dampTime = 0.2f;
    public Vector3 delayedFollowTarget;             //A delayed vector of the position of the target allowing the target to walk without the camera following
    public bool isFollowing;
    public GameObject target;
    public Vector3 margin;                          //How far the avatar is able to walk.. 20 and higher will get laggy

    public void cameraMovement()                    //The cameras target with delay and margin 
    {
        x = delayedFollowTarget.x;
        y = delayedFollowTarget.y;


        if (isFollowing)
        {
            if (target.transform.position.x <= margin.x)           //Mathf.Abs(x - target.transform.position.x
            {
                x = Mathf.Lerp(x, target.transform.position.x, Time.deltaTime);
            }
            if (Mathf.Abs(y - target.transform.position.y) > margin.y)
            {
                y = Mathf.Lerp(y, target.transform.position.y, Time.deltaTime);
            }
            delayedFollowTarget = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, delayedFollowTarget, dampTime);
            //transform.(delayedFollowTarget);
        }
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (target == null)
        {
            FindPlayer();
            if(target != null)
            delayedFollowTarget = target.transform.position;
        }
    }
    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            //if(GameObject.FindObjectOfType<Player_controller>() != null)
            //target = GameObject.FindObjectOfType<Player_controller>().gameObject;
            //GameObject searchResultRed = GameObject.FindGameObjectWithTag("Player");
            //if (searchResultRed != null)
            //{
            //    target = searchResultRed;
            //}
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
    void LateUpdate()
    {
        if (target != null)
        {
            //if (target.transform.position.x > -7 && target.transform.position.x < 7) //OLd code, useful if there were to be any 
                cameraMovement();
        }

    }
}

