using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Script
{
    public class CameraScript : MonoBehaviour
    {
        public GameObject TargetPlayer;
        float _nextTimeToSearch = 0;
        public float ZoomSpeed = 0.5f;
        public float X;
        public float Y;
        public float dampTime = 0.2f;
        public Vector3 DelayedFollowTarget;             //A delayed vector of the position of the target allowing the target to walk without the camera following
        public bool IsFollowing;
        public GameObject Target;
        public Vector3 Margin;                          //How far the avatar is able to walk.. 20 and higher will get laggy

        public void CameraMovement()                    //The cameras target with delay and margin 
        {
            if (!IsFollowing) return;
        
            X = DelayedFollowTarget.x;
            Y = DelayedFollowTarget.y;

            if (Target.transform.position.x <= Margin.x)           //Mathf.Abs(x - target.transform.position.x
            {
                X = Mathf.Lerp(X, Target.transform.position.x, Time.deltaTime);
            }
            if (Mathf.Abs(Y - Target.transform.position.y) > Margin.y)
            {
                Y = Mathf.Lerp(Y, Target.transform.position.y, Time.deltaTime);
            }
            DelayedFollowTarget = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, DelayedFollowTarget, dampTime);
            //transform.(delayedFollowTarget);
        }

        void Start()
        {
            // Target = GameObject.FindGameObjectWithTag("Player");
            Assert.IsNotNull(Target, $"Target is null in {nameof(CameraScript)} on {gameObject.name}");
        }

        void Update()
        {
            if (Target != null) return;
        
            FindPlayer();
            if(Target != null)
                DelayedFollowTarget = Target.transform.position;
        }
        void FindPlayer()
        {
            if (_nextTimeToSearch <= Time.time)
            {
                //if(GameObject.FindObjectOfType<Player_controller>() != null)
                //target = GameObject.FindObjectOfType<Player_controller>().gameObject;
                //GameObject searchResultRed = GameObject.FindGameObjectWithTag("Player");
                //if (searchResultRed != null)
                //{
                //    target = searchResultRed;
                //}
                _nextTimeToSearch = Time.time + 0.5f;
            }
        }
        void LateUpdate()
        {
            if (Target == null) return;
        
            CameraMovement();

        }
    }
}

