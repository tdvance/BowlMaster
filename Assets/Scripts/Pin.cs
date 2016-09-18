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
        float x = transform.eulerAngles.x;
        float z = transform.eulerAngles.z;
        if(x > fallenThreshold && x < 360 - fallenThreshold)
        {
            return false;
        }
        if (z > fallenThreshold && z < 360 - fallenThreshold)
        {
            return false;
        }
        return true;
    }
}
