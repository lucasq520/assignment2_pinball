using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpringLauncher : MonoBehaviour
{
    [Header("Launch Settings")]
    public float chargeDistance = 1f;
    public float chargeSpeed = 2f;
    public float releaseSpeed = 15f;
    public KeyCode launchKey = KeyCode.Space;

    [Header("Audio Settings")]
    public AudioClip rocketChargeClip;
    public AudioClip rocketShootClip;
    public float volume = 1f;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private bool isCharging = false;
    private bool isReleasing = false;
    private float chargeAmount = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        startPos = rb.position;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Update()
    {
        if (Input.GetKey(launchKey))
        {
            if (!isCharging)
            {
                isCharging = true;
                isReleasing = false;

                if (rocketChargeClip != null)
                {
                    audioSource.clip = rocketChargeClip;
                    audioSource.loop = true;
                    audioSource.volume = volume;
                    audioSource.Play();
                }
            }

            if (chargeAmount < 1f)
            {
                chargeAmount += Time.deltaTime * chargeSpeed;
                chargeAmount = Mathf.Clamp01(chargeAmount);
            }

            rb.MovePosition(startPos - Vector2.up * chargeAmount * chargeDistance);
        }

        if (isCharging && Input.GetKeyUp(launchKey))
        {
            isCharging = false;
            isReleasing = true;

            if (audioSource.isPlaying && audioSource.clip == rocketChargeClip)
            {
                audioSource.Stop();
            }

            if (rocketShootClip != null)
            {
                audioSource.PlayOneShot(rocketShootClip, volume);
            }
        }

        if (isReleasing)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, startPos, releaseSpeed * Time.deltaTime));

            if (Vector2.Distance(rb.position, startPos) < 0.01f)
            {
                rb.position = startPos;
                isReleasing = false;
                chargeAmount = 0f;
            }
        }
    }
}
