using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public LayerMask groundLayers;
    public float moveSpeed;
    public float jumpPower;
    public float jumpCooldownInSeconds;
    public float loudnessThreshold;
    private SpriteRenderer mouth;

    AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    float jumpTimestamp;
    float voiceTimestamp;

    void Start()
    {
        mouth = gameObject.GetComponentsInChildren<SpriteRenderer>().Where(r => r.name == "Mouth").Single();
        jumpTimestamp = Time.time;
        voiceTimestamp = Time.time;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.Play();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var loudness = 0f;
        if(Time.time >= jumpTimestamp && Time.time >= voiceTimestamp)
        {
            audioSource.mute = false;
            voiceTimestamp = Time.time + 0.2f;
            loudness = GetVolume() * 1000f;
            print(loudness);
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
}
