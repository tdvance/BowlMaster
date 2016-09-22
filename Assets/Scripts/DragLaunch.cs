using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour
{
    private Ball ball;
    private Vector3 positionOfDragStart;
    private float timeOfDragStart;

    [Tooltip("Multiply ball launch forward speed by this amount")]
    public float speedMultiplier = 2; 

    [Tooltip("Divide ball launch sideways speed by this amount")]
    public float aimCorrection = 2;

    public float minLaunchSpeed = 700;
    public float maxLaunchSpeed = 2000;

    // Use this for initialization
    void Start()
    {
        ball = GetComponent<Ball>();
    }

    public void MoveStart(float xNudge)
    {
        if (ball.readyToPlay)
        {
            float x = ball.transform.position.x + xNudge;
            if (x < 105f / 2 && x > -105f / 2)//ensure ball is in lane
            {
                ball.transform.Translate(Vector3.right * xNudge, Space.World);
            }
        }
    }

    public void DragStart()
    {

        //Capture time and position of start of mouse/touchscreen drag
        timeOfDragStart = Time.time;
        positionOfDragStart = Input.mousePosition;
    }

    public void DragEnd()
    {
        //launch the ball at velocity determined by swipe
        Vector3 v = Input.mousePosition - positionOfDragStart;

        Vector3 velocity = new Vector3(v.x / aimCorrection, 0, v.y * speedMultiplier) / (Time.time - timeOfDragStart);

        float m = velocity.magnitude;
        if (m < minLaunchSpeed)
        {
            velocity *= minLaunchSpeed / m;
        }
        else if (m > maxLaunchSpeed)
        {
            velocity *= maxLaunchSpeed / m;
        }
        //Debug.Log("Velocity = " + velocity.magnitude);

        if (ball.readyToPlay)
        {
            ball.Launch(velocity);
        }
    }
}
