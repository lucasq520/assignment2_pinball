using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip bounceClip;
    public float volume = 0.8f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 0.5f && bounceClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(bounceClip, volume);
        }
    }
}
