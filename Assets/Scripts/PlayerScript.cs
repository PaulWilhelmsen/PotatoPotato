using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public LayerMask groundLayers;
    public Text loseText;
    public float moveSpeed;
    public float jumpPower;
    public float jumpCooldownInSeconds;
    public float loudnessThreshold;
    public float maxVelocity = 10;
    private SpriteRenderer mouth;

    private bool isMoving;
    float speed;
    private bool hasTouchedGround = false;

    AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    float jumpTimestamp;
    float voiceTimestamp;

    void Start()
    {
        loseText.gameObject.SetActive(false);
        isMoving = true;
        mouth = gameObject.GetComponentsInChildren<SpriteRenderer>().Where(r => r.name == "Mouth").Single();
        Assert.IsNotNull(mouth);
        jumpTimestamp = Time.time;
        voiceTimestamp = Time.time;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.Play();
    }

    void Update()
    {
        speed = GetComponent<Rigidbody2D>().velocity.magnitude;
        if (hasTouchedGround && Mathf.Abs(speed) < 0.018)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            loseText.gameObject.SetActive(true);
            isMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.R))
            {
                print("REEEEE");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxVelocity);
        var loudness = 0f;
        if(Time.time >= jumpTimestamp && Time.time >= voiceTimestamp)
        {
            audioSource.mute = false;
            voiceTimestamp = Time.time + 0.2f;
            loudness = GetVolume() * 1000f;
        }

        if ((Time.time >= jumpTimestamp && 
            (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))|| loudness > loudnessThreshold))
        {
            var jumpMultiplier = 1f;
            if(loudness > loudnessThreshold)
            {
                jumpMultiplier = loudness / loudnessThreshold;
            }
            jumpTimestamp = Time.time + jumpCooldownInSeconds;
            audioSource.mute = true;
            Jump(jumpMultiplier);
        }
    }

    void Jump(float jumpMultiplier)
    {
        var mouthDirection = mouth.transform.position - gameObject.transform.position;
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();
        var force = jumpPower * jumpMultiplier;
        rigidbody.AddForce(new Vector2(mouthDirection.x * -force, mouthDirection.y * -force), ForceMode2D.Impulse);
    }

    float GetVolume()
    {
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        var clipLoudness = 0f;
        for (int i = 0; i < samples.Length; ++i)
        {
            samples[i] = samples[i] * 0.5f;
            clipLoudness += Mathf.Abs(samples[i]);
        }

        return clipLoudness /= audioSource.clip.samples * audioSource.clip.channels;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            hasTouchedGround = true;
        }
    }
}
