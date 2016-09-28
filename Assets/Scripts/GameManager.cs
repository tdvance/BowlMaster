using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameManager : MonoBehaviour {
    private List<int> bowls = new List<int>();

    private PinSetter pinSetter;
    private PinCounter pinCounter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;


    // Use this for initialization
    void Start () {
        pinCounter = FindObjectOfType<PinCounter>();
        pinSetter = FindObjectOfType<PinSetter>();
        ball = FindObjectOfType<Ball>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Bowl(int pinFall) {
        bowls.Add(pinFall);
        ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
        pinSetter.PerformAction(nextAction);

        //scoreDisplay.FillRollCard(new int[] {0,1, 2,3, 4,5, 6,4, 10, 10, 0,10, 1,1, 1,1, 10, 10, 10 }.ToList());
        scoreDisplay.FillRollCard(bowls);

        ball.Reset();
    }
}
