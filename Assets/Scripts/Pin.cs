using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
    public float fallenThreshold = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(name + " is standing: " + IsStanding());
	}
    public bool IsStanding()
    {
        if(transform.position.y < -1)// pin fell off
        {
            return false;
        }
        float pitch = transform.eulerAngles.x;
        float roll = transform.eulerAngles.z;
        if(pitch > fallenThreshold && pitch < 360 - fallenThreshold)//pin is not very vertical
        {
            return false;
        }
        if (roll > fallenThreshold && roll < 360 - fallenThreshold)
        {
            return false;
        }
        return true;
    }
}
