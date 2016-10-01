using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private List<int> bowls = new List<int>();

    private PinSetter pinSetter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;
    private AudioSource[] audioSources;

    public AudioClip strikeSound;
    public AudioClip perfectSound;
    public AudioClip gutterSound;
    public AudioClip turkeySound;
    public AudioClip spareSound;

    // Use this for initialization
    void Start() {
        pinSetter = FindObjectOfType<PinSetter>();
        ball = FindObjectOfType<Ball>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        audioSources = GetComponents<AudioSource>();

        scoreDisplay.Reset();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Bowl(int pinFall) {
        bowls.Add(pinFall);
        ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
        pinSetter.PerformAction(nextAction);

        scoreDisplay.Reset();
        string rollsString = scoreDisplay.FillRolls(bowls);
        scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(bowls));

        if (rollsString.EndsWith("X") || rollsString.EndsWith("X ")) {
            Strike();
        }

        if (rollsString.EndsWith("/")) {
            Spare();
        }

        if(rollsString == "X X X X X X X X X XXX") {
            PerfectGame();
            GameOver();
        }
        if (IsTurkey(rollsString)) {
            Turkey();
        }
        if (nextAction != ActionMaster.Action.EndGame) {
            ball.Reset();
        } else {
           
            GameOver();
        }
    }

    private bool IsTurkey(string rollString) {
        int count = 0;
        for (int i = rollString.Length - 1; i >= 0; i--) {
            if (rollString[i] == ' ') {
                continue;
            }
            if (rollString[i] == 'X') {
                count++;
            } else {
                break;
            }
            if (count == 3) {
                return true;
            }
        }
        return false;
    }

    public void Split() {
        Debug.Log("Split");
    }

    public void Gutter() {
        Debug.Log("Gutter");
        audioSources[1].PlayOneShot(gutterSound, 0.25f);
    }


    public void Turkey() {
        audioSources[1].PlayOneShot(turkeySound);
        Debug.Log("Turkey");
    }


    public void Strike() {
        audioSources[0].PlayOneShot(strikeSound);
        Debug.Log("Strike");
    }

    public void Spare() {
        audioSources[0].PlayOneShot(spareSound);
        Debug.Log("Spare");
    }

    public void PerfectGame() {
        audioSources[1].PlayOneShot(perfectSound);
        Debug.Log("Perfect!");
    }

    public void GameOver() {

        Invoke("StartScreen", 5f);
    }

    public void StartScreen() {
        SceneManager.LoadScene(0);
    }


}
