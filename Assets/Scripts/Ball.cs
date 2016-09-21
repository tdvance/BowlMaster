using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float minLaunchSpeed = 700;
    public float maxLaunchSpeed = 2000;
    private AudioSource audioSource;
    private Rigidbody rigidBody;
    private Vector3 ballStartPosition;

    public bool readyToPlay;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        audioSource = GetComponent<AudioSource>();
        ballStartPosition = transform.position;
        readyToPlay = true;
    }


    public void Launch(Vector3 velocity)
    {
        float m = velocity.magnitude;
        if(m < minLaunchSpeed)
        {
            velocity *= minLaunchSpeed / m;
        }
        else if (m > maxLaunchSpeed)
        {
            velocity *= maxLaunchSpeed / m;
        }
        Debug.Log("Velocity = " + velocity.magnitude);

        rigidBody.velocity = velocity;
        rigidBody.useGravity = true;

        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Play();
        readyToPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 5)
        {
            FindObjectOfType<PinSetter>().RollComplete();
        }
    }

    public void Reset()
    {
        transform.position = ballStartPosition;
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        readyToPlay = true;
    }
}
