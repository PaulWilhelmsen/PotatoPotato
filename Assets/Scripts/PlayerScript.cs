using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public SpriteRenderer img;
    public GameObject deathZone;
    public LayerMask groundLayers;
    public Text loseText;
    public Text scoreText;
    public float moveSpeed;
    public float jumpPower;
    public float jumpCooldownInSeconds;
    public float loudnessThreshold;
    public float maxVelocity = 10;
    private SpriteRenderer mouth;
    public float maxXReached;

    private bool isMoving;
    public float speed;
    private bool isDead;

    public AudioSource microphoneSource;
    public AudioSource jumpSoundSource;
    public AudioSource deathSound;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    float jumpTimestamp;
    float voiceTimestamp;

    public float MaxReached()
    {
        return maxXReached;
    }

    void Start()
    {
        img.color = new Color(0, 0, 0, 0);
        maxXReached = 0;
        loseText.gameObject.SetActive(false);
        isMoving = true;
        isDead = false;
        mouth = gameObject.GetComponentsInChildren<SpriteRenderer>().Where(r => r.name == "Mouth").Single();
        Assert.IsNotNull(mouth);
        jumpTimestamp = Time.time;
        voiceTimestamp = Time.time;

        microphoneSource.clip = Microphone.Start(null, true, 10, 44100);
        microphoneSource.Play();
    }

    void Update()
    {
        var deathZoneMin = Mathf.Max(maxXReached - 25, deathZone.transform.localPosition.x + 0.01f);
        deathZone.transform.SetPositionAndRotation(new Vector3(deathZoneMin, deathZone.transform.localPosition.y, deathZone.transform.localPosition.z), new Quaternion());
        var rb = GetComponent<Rigidbody2D>();
        maxXReached = Mathf.Max(rb.position.x, maxXReached);
        scoreText.text = $"Score: {(int)maxXReached}";
        speed = rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxVelocity);
        var loudness = 0f;
        if(Time.time >= jumpTimestamp && Time.time >= voiceTimestamp)
        {
            microphoneSource.mute = false;
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
            microphoneSource.mute = true;
            Jump(jumpMultiplier);
        }
    }

    void Jump(float jumpMultiplier)
    {
        jumpSoundSource.Play();
        var mouthDirection = mouth.transform.position - gameObject.transform.position;
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();
        var force = jumpPower * jumpMultiplier;
        rigidbody.AddForce(new Vector2(mouthDirection.x * -force, mouthDirection.y * -force), ForceMode2D.Impulse);
    }

    float GetVolume()
    {
        float[] samples = new float[microphoneSource.clip.samples * microphoneSource.clip.channels];
        microphoneSource.clip.GetData(samples, 0);

        var clipLoudness = 0f;
        for (int i = 0; i < samples.Length; ++i)
        {
            samples[i] = samples[i] * 0.5f;
            clipLoudness += Mathf.Abs(samples[i]);
        }

        return clipLoudness /= microphoneSource.clip.samples * microphoneSource.clip.channels;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "DeathZone")
        {
            Die();
        }
    }

    void Die()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (!isDead)
        {
            deathSound.Play();
        }
        isDead = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        loseText.gameObject.SetActive(true);
        isMoving = false;
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
