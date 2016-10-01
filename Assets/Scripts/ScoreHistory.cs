using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreHistory : MonoBehaviour {

    public Text lastScore;
    public Text topScore;

    // Use this for initialization
    void Start() {
        if (lastScore && topScore) {
            SetScores();
        }
    }

    void SetScores() {
        if (PlayerPrefs.HasKey("LastScore")) {
            lastScore.text = "Last Score: " + PlayerPrefs.GetInt("LastScore");
        }
        if (PlayerPrefs.HasKey("TopScore")) {
            topScore.text = "Top Score: " + PlayerPrefs.GetInt("TopScore");
        }
    }

    public void SubmitScore(int score) {
        PlayerPrefs.SetInt("LastScore", score);
        if (PlayerPrefs.HasKey("TopScore")) {
            PlayerPrefs.SetInt("TopScore", Mathf.Max(score, PlayerPrefs.GetInt("TopScore")));
        } else {
            PlayerPrefs.SetInt("TopScore", score);
        }
    }
}
