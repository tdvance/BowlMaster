using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PinCounter : MonoBehaviour {
    private GameManager gameManager;
    private static GameObject pinsStandingTextBox;
    public float maxSettleTime = 3f;
    private float lastChangeTime = 0;
    private bool ballEnteredBox = false;
    private int lastStandingCount = -1; //-1 = no value
    private int startNumStanding = 10;

    // Use this for initialization
    void Start() {
        pinsStandingTextBox = GameObject.Find("PinsStanding");
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        if (ballEnteredBox) {
            pinsStandingTextBox.GetComponent<Text>().text = CountStanding().ToString(); // update pin standing count
            CheckStanding();
        }
    }

    public void BeginCounting() {
        ballEnteredBox = true;
        pinsStandingTextBox.GetComponent<Text>().color = Color.red;
    }
 

    public void ResetCount() {
        pinsStandingTextBox.GetComponent<Text>().text = "10"; //reset pin count
        pinsStandingTextBox.GetComponent<Text>().color = Color.black;
        startNumStanding = 10;
    }


    private void RecordCount() {
        ballEnteredBox = false;
        lastStandingCount = -1;
        lastChangeTime = 0;
        pinsStandingTextBox.GetComponent<Text>().color = Color.green; //freeze pin count
        int numStanding = CountStanding();
        int numFallen = startNumStanding - numStanding;
        startNumStanding = numStanding;
        gameManager.Bowl(numFallen);
    }

    private int CountStanding() {
        int howManyStanding = 0;

        foreach (Pin aPin in FindObjectsOfType<Pin>()) {
            if (aPin.IsStanding()) {
                howManyStanding++;
            }
        }

        return howManyStanding;
    }


    private void CheckStanding() {
        //update the lastStandingCount variable
        int currentStandingCount = CountStanding();

        if (currentStandingCount != lastStandingCount || /*defensive programming: */ lastChangeTime == 0) {
            lastStandingCount = currentStandingCount;
            lastChangeTime = Time.time;
        }

        //call PinsHaveSettled()  when ready
        if (Time.time - lastChangeTime > maxSettleTime) {
            RecordCount();
        }
    }    

}
