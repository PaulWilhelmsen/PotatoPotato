using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //new public Rigidbody2D rigidbody;
    public LayerMask groundLayers;
    public float moveSpeed;
    public float jumpPower;
    public ContactFilter2D groundFilter;
    
    float targetMoveSpeed;
    bool grounded;

    public Vector3 debugMouthDirection;


    void Start()
    {
    }

    void Update()
    {
        grounded = gameObject.GetComponent<Collider2D>().IsTouching(groundFilter);
    }

    void FixedUpdate()
    {
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();
        var mouth = gameObject.GetComponentsInChildren<SpriteRenderer>().Where(r => r.name == "Mouth").Single();
        var mouthDirection = mouth.transform.position - transform.position;

        // Gets movement speed and applies it
        targetMoveSpeed = Mathf.Lerp(rigidbody.velocity.x, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Time.deltaTime * 10);
        rigidbody.velocity = new Vector2(targetMoveSpeed, rigidbody.velocity.y);

        // Jumps if not grounded
        if (grounded && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, (1f * jumpPower));
        }
    }
}
