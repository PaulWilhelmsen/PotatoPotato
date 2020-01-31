using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float Speed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(new Vector3(x: 0, y: 1) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(new Vector3(x: 1, y: 0) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(new Vector3(x: -1, y: 0) * Time.deltaTime * Speed);
        }
    }
}
