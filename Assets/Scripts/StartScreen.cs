using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour {
    public static int BallSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame(int ball) {
        BallSelected = ball;
        SceneManager.LoadScene(1);

    }
}
