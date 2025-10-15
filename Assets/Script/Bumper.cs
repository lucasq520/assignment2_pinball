using UnityEngine;

public class Bumper : MonoBehaviour
{
    public AudioClip hitClip;
    public float volume = 1f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (hitClip != null)
            {
                audioSource.PlayOneShot(hitClip, volume);
            }

        }
    }
}
