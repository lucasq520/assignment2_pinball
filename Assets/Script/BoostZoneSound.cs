using UnityEngine;

public class BoostZoneSound : MonoBehaviour
{
    public AudioClip boostClip;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (boostClip != null)
            {
                audioSource.PlayOneShot(boostClip, volume);
            }
        }
    }
}
