using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Ball ball;
    private Vector3 offsetFromBall;

    // Use this for initialization
    void Start()
    {
        offsetFromBall = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 ballPosition = ball.transform.position + offsetFromBall;
        transform.position = new Vector3(
            Mathf.Clamp(ballPosition.x, -100f, 100f),
            Mathf.Clamp(ballPosition.y, 30f, 200f),
            Mathf.Clamp(ballPosition.z, -1000f, 1829f));

    }
}
