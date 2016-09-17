using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {
    private Ball ball;
    private Vector3 startPos;
    private float startTime;
    private bool ballIsMoving;
    public float velocityScale = 2;
    public float velocityCorrect = 2;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
        ballIsMoving = false;
	}

    public void MoveStart(float xNudge)
    {
        //Debug.Log("xNudge = " + xNudge);
        if (!ballIsMoving)
        {
            float x = ball.transform.position.x + xNudge;
            if (x < 105f / 2 && x > -105f / 2)
            {
                ball.transform.Translate(Vector3.right * xNudge);
            }
        }
    }
	
	public void DragStart()
    {
        //Capture time and position of start of drag
        startTime = Time.time;
        startPos = Input.mousePosition;
        //Debug.Log("DragStart: " + startPos + " at time " + startTime);
    }

    public void DragEnd()
    {
        //launch the ball
        Vector3 v = Input.mousePosition - startPos;

        Vector3 velocity = new Vector3(v.x/velocityCorrect,0,v.y*velocityScale)/(Time.time - startTime);
        //Debug.Log("DragEnd: " + velocity);

        ball.Launch(velocity);
        ballIsMoving = true;
    }
}
