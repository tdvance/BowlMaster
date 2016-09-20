using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    private GameObject pinsStandingTextBox;
    private bool ballEnteredBox;
    private float lastChangeTime = 0;

    private Ball ball;
    private Vector3 ballStartPosition;

    public int lastStandingCount = -1; //-1 = no value
    public float maxSettleTime = 3f;

    // Use this for initialization
    void Start()
    {
        pinsStandingTextBox = GameObject.Find("PinsStanding");
        ballEnteredBox = false;

        ball = FindObjectOfType<Ball>();

        if (!pinsStandingTextBox)
        {
            Debug.LogError("PinsStanding text box not found!");
        }
    }


    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.transform.parent.gameObject.GetComponent<Ball>())
        {
            RollComplete();
        }
    }

    public void RollComplete()
    {
        ballEnteredBox = true;
        pinsStandingTextBox.GetComponent<Text>().color = Color.red;
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.transform.parent.gameObject.GetComponent<Pin>())
        {
            Destroy(obj.gameObject.transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ballEnteredBox)
        {
            pinsStandingTextBox.GetComponent<Text>().text = CountStanding().ToString();
            CheckStanding();
        }
    }

    void CheckStanding()
    {
        //update the lastStandingCount variable
        int currentStandingCount = CountStanding();
        if (currentStandingCount != lastStandingCount || /*defensive programming: */ lastChangeTime == 0)
        {
            lastStandingCount = currentStandingCount;
            lastChangeTime = Time.time;
        }

        //call PinsHaveSettled()  when ready
        if(Time.time - lastChangeTime  > maxSettleTime)
        {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled()
    {
        ballEnteredBox = false;
        lastStandingCount = -1;
        lastChangeTime = 0;
        pinsStandingTextBox.GetComponent<Text>().color = Color.green;
        ball.Reset();
    }


    int CountStanding()
    {
        int howManyStanding = 0;

        foreach (Pin aPin in FindObjectsOfType<Pin>())
        {
            if (aPin.IsStanding())
            {
                howManyStanding++;
            }
        }

        return howManyStanding;
    }
}
