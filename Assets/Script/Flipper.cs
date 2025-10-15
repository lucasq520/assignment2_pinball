using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class FlipperControl : MonoBehaviour
{
    [SerializeField] private bool isLeftFlipper = true;
    public float flipSpeed = 2000f;
    public float returnSpeed = -2000f;
    public float motorTorque = 100f;

    public AudioClip flipperSound;
    public float volume = 1f;

    private HingeJoint2D hinge;
    private AudioSource audioSource;
    private bool soundPlayed = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();

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
        JointMotor2D motor = hinge.motor;
        bool keyPressed = isLeftFlipper ? Input.GetKey(KeyCode.A) : Input.GetKey(KeyCode.D);

        motor.motorSpeed = keyPressed ? flipSpeed : returnSpeed;
        motor.maxMotorTorque = motorTorque;
        hinge.motor = motor;
        hinge.useMotor = true;

        if (keyPressed && !soundPlayed)
        {
            soundPlayed = true;

            if (flipperSound != null)
                audioSource.PlayOneShot(flipperSound, volume);
        }

        if (!keyPressed)
        {
            soundPlayed = false;
        }
    }
}
