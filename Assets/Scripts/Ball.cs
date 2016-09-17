using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float launchSpeed = 1000;
    private AudioSource audioSource;
    private Rigidbody rigidBody;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        audioSource = GetComponent<AudioSource>();

    }
    

    public void Launch(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
        rigidBody.useGravity = true;

        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
