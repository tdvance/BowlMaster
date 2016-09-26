using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
    private AudioSource audioSource;
    private Ball ball;

    public float fallenThreshold = 10;
    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(name + " is standing: " + IsStanding());
    }
    public bool IsStanding() {
        if (transform.position.y < -1)// pin fell off
        {
            return false;
        }
        float pitch = transform.eulerAngles.x;
        float roll = transform.eulerAngles.z;
        if (pitch > fallenThreshold && pitch < 360 - fallenThreshold)//pin is not very vertical
        {
            return false;
        }
        if (roll > fallenThreshold && roll < 360 - fallenThreshold) {
            return false;
        }
        return true;
    }

    void OnCollisionEnter(Collision obj) {
        if (ball.transform.position.z>1000) {
            audioSource.Play();
        }
    }
}
