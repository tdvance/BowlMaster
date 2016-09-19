using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    GameObject pinsStandingTextBox;
    // Use this for initialization
    void Start()
    {
        pinsStandingTextBox = GameObject.Find("PinsStanding");

        if (!pinsStandingTextBox)
        {
            Debug.LogError("PinsStanding text box not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        pinsStandingTextBox.GetComponent<Text>().text = CountStanding().ToString();
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
