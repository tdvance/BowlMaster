using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour {


    private Ball ball;
    private PinCounter pinCounter;
    private Animator animator;

    public GameObject tenPinsPrefab;

    public float distanceToRaise = 40f;


    // Use this for initialization
    void Start() {
        ball = FindObjectOfType<Ball>();
        pinCounter = FindObjectOfType<PinCounter>();
        animator = GetComponent<Animator>();
    }


    public void Tidy() {
        animator.SetTrigger("Tidy");
    }

    public void Reset() {
        animator.SetTrigger("Reset");
    }

    public void RaisePins() {
        foreach (Pin aPin in FindObjectsOfType<Pin>()) {
            if (aPin.IsStanding()) //if pin is still standing, pull it out of the way of the swiper
            {
                aPin.Raise(distanceToRaise);
            }
        }
    }

    public void LowerPins() {
        foreach (Pin aPin in FindObjectsOfType<Pin>()) {
            if (aPin.IsStanding()) //put pins back down for second ball roll
            {
                aPin.Lower();
            } else {
                Destroy(aPin.gameObject); // in case swiper misses a pin, e.g. pin knocked out into the lane on wrong side of swiper
            }
        }
        ball.readyToPlay = true;
    }

    public void RenewPins() {
        Instantiate(tenPinsPrefab);
        pinCounter.ResetCount();
    }
    
    
    public void PerformAction(ActionMaster.Action action) {
        switch (action) {
            case ActionMaster.Action.Tidy:
                Tidy();
                break;
            case ActionMaster.Action.EndGame:
            case ActionMaster.Action.EndTurn:
            case ActionMaster.Action.Reset:
                Reset();
                break;
        }
    }

    //ball enters pinsetter area
    void OnTriggerEnter(Collider obj) {
        if (obj.gameObject.transform.parent.gameObject.GetComponent<Ball>()) {
            pinCounter.BeginCounting();//activate pinsetter when ball goes through
        }
    }


    //pin exits pinsetter area
    void OnTriggerExit(Collider obj) {
        if (obj.gameObject.transform.parent.gameObject.GetComponent<Pin>()) //disintegrate pins that try to escape
        {
            Destroy(obj.gameObject.transform.parent.gameObject);
        }
    }
}
