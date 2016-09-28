﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ScoreDisplay : MonoBehaviour {

    public Text[] bowls = new Text[21];
    public Text[] frameScores = new Text[10];

    public static string FormatRolls(List<int> rolls) {
        string result = "";
        int previousRoll = -1;
        foreach (int roll in rolls) {
            if (result.EndsWith("X") && result.Length < 19) {
                result += " ";
            }
            result += RollChar(roll, previousRoll);
            if (previousRoll == -1 && roll < 10 && result.Length < 20) {
                previousRoll = roll;
            } else {
                previousRoll = -1;
            }
        }
        return result;
    }

    public static string RollChar(int roll, int previousRoll) {
        if (roll == 0) {
            return "-";
        }
        if (roll == 10 && previousRoll < 0) {
            return "X";
        }
        if (previousRoll >= 0 && previousRoll + roll == 10) {
            return "/";
        }
        return roll.ToString();
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
}
