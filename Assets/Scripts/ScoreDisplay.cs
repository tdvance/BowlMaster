using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ScoreDisplay : MonoBehaviour {

    public Text[] bowls = new Text[21];
    public Text[] frameScores = new Text[10];

	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset() {
        for(int i=0; i<bowls.Length; i++) {
            bowls[i].text = " ";
        }
        for (int i = 0; i < frameScores.Length; i++) {
            frameScores[i].text = "   ";
        }
    }
}
