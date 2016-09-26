using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    private GameObject pinsStandingTextBox;
    
    private bool ballEnteredBox = false;
    private float lastChangeTime = 0;

    private Ball ball;

    private int lastStandingCount = -1; //-1 = no value


    public float maxSettleTime = 3f;
    public float distanceToRaise = 40f;
    public GameObject tenPinsPrefab;

    private ActionMaster actionMaster = new ActionMaster(true);
    private int startNumStanding = 10;

    private Animator animator;

    private static PinSetter instance;

    // Use this for initialization
    void Start()
    {
        pinsStandingTextBox = GameObject.Find("PinsStanding");
        ball = FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();

        if (!pinsStandingTextBox)
        {
            Debug.LogError("PinsStanding text box not found!");
        }
        if (!ball) {
            Debug.LogError("Ball not found!");
        }

        instance = this;
    }


    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.transform.parent.gameObject.GetComponent<Ball>())
        {
            RollComplete();//activate pinsetter when ball goes through
        }
    }

    public void RollComplete()
    {
        ballEnteredBox = true;
        pinsStandingTextBox.GetComponent<Text>().color = Color.red;
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.transform.parent.gameObject.GetComponent<Pin>()) //disintegrate pins that try to escape
        {
            Destroy(obj.gameObject.transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ballEnteredBox)
        {
            pinsStandingTextBox.GetComponent<Text>().text = CountStanding().ToString(); // update pin standing count
            CheckStanding();
        }
    }

    public void RaisePins()
    {
        foreach (Pin aPin in FindObjectsOfType<Pin>())
        {
            if (aPin.IsStanding()) //if pin is still standing, pull it out of the way of the swiper
            {
                aPin.GetComponent<Rigidbody>().useGravity = false;
                aPin.GetComponent<Rigidbody>().velocity = Vector3.zero;
                aPin.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                aPin.transform.rotation = Quaternion.identity;
                aPin.transform.Translate(Vector3.up * distanceToRaise, Space.World);
            }
        }
        
    }

    public void LowerPins()
    {
        foreach (Pin aPin in FindObjectsOfType<Pin>())
        {
            if (aPin.IsStanding()) //put pins back down for second ball roll
            {
                aPin.transform.position -= new Vector3(0, aPin.transform.position.y, 0);
                aPin.GetComponent<Rigidbody>().velocity = Vector3.zero;
                aPin.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                aPin.GetComponent<Rigidbody>().useGravity = true;
            }else
            {
                Destroy(aPin.gameObject); // in case swiper misses a pin, e.g. pin knocked out into the lane on wrong side of swiper
            }
        }
        ball.readyToPlay = true;
    }

    public void RenewPins()
    {
        Instantiate(tenPinsPrefab);
        pinsStandingTextBox.GetComponent<Text>().text = "10"; //reset pin count
        pinsStandingTextBox.GetComponent<Text>().color = Color.black;
        startNumStanding = 10;
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
        pinsStandingTextBox.GetComponent<Text>().color = Color.green; //freeze pin count
        int numStanding = CountStanding();
        int numFallen =  startNumStanding - numStanding;
        startNumStanding = numStanding;
        ActionMaster.Action action = actionMaster.Bowl(numFallen);
        switch (action) {
            case ActionMaster.Action.Tidy:
                animator.SetTrigger("Tidy");
                break;
            case ActionMaster.Action.EndGame:
            case ActionMaster.Action.EndTurn:
            case ActionMaster.Action.Reset:
                animator.SetTrigger("Reset");
                break;
        }
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

    public static void Strike() {
        instance.GetComponent<AudioSource>().Play();
    }
}
