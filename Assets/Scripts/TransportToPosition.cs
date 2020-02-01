using UnityEngine;

namespace Assets.Scripts
{
    public class TransportToPosition : MonoBehaviour
    {
        public void NewPosition(Transform position)
        {
            gameObject.transform.position = position.position;
        }
    }
}
