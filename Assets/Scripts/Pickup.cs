using UnityEngine;

namespace Assets.Scripts
{
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

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "PlayerPart") return;
            
            SetNewPosition(other.transform.position);
            ConnectJoint(other.gameObject);
        }

        private void SetNewPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        private void ConnectJoint(GameObject connectTo)
        {
            _joint.connectedBody = connectTo.GetComponent<Rigidbody2D>();
        }
    }
}
