using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ScoreDisplay : MonoBehaviour {

    public Text[] bowls = new Text[21];
    public Text[] frameScores = new Text[10];

    public static string FormatRolls(List<int> rolls) {
        string result = "";

        return result;
    }

    public void Reset() {
        for (int i = 0; i < bowls.Length; i++) {
            bowls[i].text = " ";
        }
        for (int i = 0; i < frameScores.Length; i++) {
            frameScores[i].text = "   ";
        }
    }

    public void FillRolls(List<int> rolls) {
        int i = 0;
        foreach (char c in FormatRolls(rolls).ToCharArray()) {
            bowls[i].text = c.ToString();
            i++;
        }
    }

    public void FillFrames(List<int> frames) {
        for (int i = 0; i < frames.Count; i++) {
            frameScores[i].text = frames[i].ToString();
        }
    }

    public void FillRollCard_bad(List<int> rolls) {
        Reset();
        int rollNum = 0;
        int lastRoll = 0;
        foreach (int roll in rolls) {
            if (roll == 0) {
                bowls[rollNum].text = "-";
                rollNum++;
            } else if (roll == 10 && (rollNum % 2 == 0 || (lastRoll == 10 && rollNum >= 20))) {//strike
                if (rollNum % 2 == 0) {
                    bowls[rollNum].text = "X";
                    if (rollNum < 20) {
                        rollNum += 2;
                    } else {
                        rollNum++;
                    }
                }
            } else {
                if (rollNum % 2 == 1 && roll + lastRoll == 10) {//spare
                    bowls[rollNum].text = "/";
                } else {
                    bowls[rollNum].text = roll.ToString();
                }
                rollNum++;
            }

            lastRoll = roll;
        }
        while (rollNum < 21) {
            bowls[rollNum].text = " ";
            rollNum++;
        }
    }
}
