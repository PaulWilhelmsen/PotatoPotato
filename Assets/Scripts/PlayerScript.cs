using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public LayerMask groundLayers;
    public float moveSpeed;
    public float jumpPower;
    public float jumpCooldownInSeconds;

    AudioSource audioSource;
    private float currentUpdateTime = 0f;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;
    private float clipLoudness;
    private float[] clipSampleData;

    float targetMoveSpeed;
    float jumpTimeStamp;

    void Start()
    {
        jumpTimeStamp = Time.time;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.Play();
    }

    void Update()
    {
        // Audio Loudness
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
            audioSource.clip.GetData(samples, 0);

            for (int i = 0; i < samples.Length; ++i)
            {
                samples[i] = samples[i] * 0.5f;
                clipLoudness += Mathf.Abs(samples[i]);
            }

            clipLoudness /= sampleDataLength;
        }
    }

    void FixedUpdate()
    {
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();
        var mouth = gameObject.GetComponentsInChildren<SpriteRenderer>().Where(r => r.name == "Mouth").Single();
        var mouthDirection = mouth.transform.position - gameObject.transform.position;

        // Horizontal movement speed (messes up air speed)
        //targetMoveSpeed = Mathf.Lerp(rigidbody.velocity.x, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Time.deltaTime * 10);
        //rigidbody.velocity = new Vector2(targetMoveSpeed, rigidbody.velocity.y);

        if (Time.time >= jumpTimeStamp &&
            (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            jumpTimeStamp = Time.time + jumpCooldownInSeconds;
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, (1f * jumpPower));
            rigidbody.AddForce(new Vector2(mouthDirection.x * -jumpPower * clipLoudness, mouthDirection.y * -jumpPower * clipLoudness), ForceMode2D.Impulse);
        }
    }
}
