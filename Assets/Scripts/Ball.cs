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
        audioSource = GetComponent<AudioSource>();

        Launch();
    }

    public void Launch()
    {
        rigidBody.velocity = Vector3.forward * launchSpeed+Vector3.right;

        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
