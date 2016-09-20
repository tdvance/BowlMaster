using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {
    private Ball ball;
    private Vector3 positionOfDragStart;
    private float timeOfDragStart;
    public float speedMultiplier = 2;
    public float aimCorrection = 2;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}

    public void MoveStart(float xNudge)
    {
        //Debug.Log("xNudge = " + xNudge);
        if (ball.readyToPlay)
        {
            float x = ball.transform.position.x + xNudge;
            if (x < 105f / 2 && x > -105f / 2)
            {
                ball.transform.Translate(Vector3.right * xNudge, Space.World);
            }
        }
    }
	
	public void DragStart()
    {
        //Capture time and position of start of drag
        timeOfDragStart = Time.time;
        positionOfDragStart = Input.mousePosition;
        //Debug.Log("DragStart: " + startPos + " at time " + startTime);
    }

    public void DragEnd()
    {
        //launch the ball
        Vector3 v = Input.mousePosition - positionOfDragStart;

        Vector3 velocity = new Vector3(v.x/aimCorrection,0,v.y*speedMultiplier)/(Time.time - timeOfDragStart);
        //Debug.Log("DragEnd: " + velocity);

        ball.Launch(velocity);
    }
}
