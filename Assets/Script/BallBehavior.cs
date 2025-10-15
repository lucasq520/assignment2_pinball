using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bumper"))
        {
            GameManager.instance.AddScore(100);
        }

        if (collision.gameObject.CompareTag("Spinner"))
        {
            GameManager.instance.AddScore(50);
        }

        if (collision.gameObject.CompareTag("Satelite"))
        {
            GameManager.instance.AddScore(50);
        }

        if (collision.gameObject.CompareTag("Boost"))
        {
            GameManager.instance.AddScore(25);
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
            GameManager.instance.LoseLife();
        }
    }
}
