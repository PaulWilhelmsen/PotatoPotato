using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //new public Rigidbody2D rigidbody;
    public LayerMask groundLayers;
    public float moveSpeed;
    public float jumpPower;
    float targetMoveSpeed;

    bool grounded;

    void Start()
    {
    }

    void Update()
    {
        // Checks if gameObjects overlaps ground layer
        grounded = Physics2D.OverlapArea(
            new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f),
            groundLayers);
    }

    void FixedUpdate()
    {
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();

        // Gets movement speed and applies it
        targetMoveSpeed = Mathf.Lerp(rigidbody.velocity.x, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Time.deltaTime * 10);
        rigidbody.velocity = new Vector2(targetMoveSpeed, rigidbody.velocity.y);

        // Jumps if not grounded
        if(grounded && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
