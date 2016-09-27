using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
    private List<int> bowls = new List<int>();

    private PinSetter pinSetter;
    private PinCounter pinCounter;
    private Ball ball;


    // Use this for initialization
    void Start () {
        pinCounter = FindObjectOfType<PinCounter>();
        pinSetter = FindObjectOfType<PinSetter>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Bowl(int pinFall) {
        bowls.Add(pinFall);
        ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
        pinSetter.PerformAction(nextAction);
        ball.Reset();
    }
}
