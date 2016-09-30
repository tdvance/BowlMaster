using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private AudioSource audioSource;
    private Rigidbody rigidBody;
    private Vector3 ballStartPosition;
    private PinCounter pinCounter;

    private MeshRenderer meshRenderer;
    private Material material;

    public bool readyToPlay;

    // Use this for initialization
    void Start() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.materials[StartScreen.BallSelected];
        meshRenderer.materials = new Material[] { material };
        switch (StartScreen.BallSelected) {
            case 0:
                Debug.Log("Mars selected");
                break;
            case 1:
                Debug.Log("Green Jupiter selected");
                break;
            case 2:
                Debug.Log("White Jupiter selected");
                break;
            default:
                throw new UnityException("Unrecognized ball choice: " + StartScreen.BallSelected);
        }
        pinCounter = FindObjectOfType<PinCounter>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        audioSource = GetComponent<AudioSource>();
        ballStartPosition = transform.position;
        readyToPlay = true;
    }


    public void Launch(Vector3 velocity) {

        rigidBody.velocity = velocity;
        rigidBody.useGravity = true;

        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Play();
        readyToPlay = false;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < 5)//if ball falls off lane, tell pinsetter to activate
        {
            pinCounter.BeginCounting();
        }
    }

    public void Reset() {
        transform.position = ballStartPosition;
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        //readyToPlay = true;
    }
}
