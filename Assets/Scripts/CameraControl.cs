using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Ball ball;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 p = ball.transform.position + offset;
        transform.position = new Vector3(
            Mathf.Clamp(p.x, -100f, 100f),
            Mathf.Clamp(p.y, 30f, 200f),
            Mathf.Clamp(p.z, -1000f, 1829f));

    }
}
