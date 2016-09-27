using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreMaster {

    public enum GameState {
        TopOfFrame,
        MiddleOfFrame,
        BonusRoll,
        GameOver
    };


    // Returns a list of individual (non-cumulative) frame scores
    public static List<int> ScoreFrames(List<int> rolls) {
        int[] frames = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] ballsNeeded = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        int frame = 1;
        int ballsThisFrame = 0;
        foreach (int pins in rolls) {
            ballsThisFrame++;
            for (int i = 0; i < frame; i++) {
                if (ballsNeeded[i] > 0) {
                    ballsNeeded[i]--;
                    frames[i] += pins;
                }
            }
            if (ballsThisFrame == 1 && pins < 10) {
                ballsNeeded[frame - 1]++;
            } else {
                if (ballsThisFrame == 1 && pins == 10) { //strike
                    ballsNeeded[frame - 1] += 2;
                } else if (frames[frame - 1] == 10 && ballsThisFrame == 2 && ballsNeeded[frame - 1] == 0) {//spare
                    ballsNeeded[frame - 1]++;
                }
                if (frame < 10) {
                    ballsThisFrame = 0;
                    frame++;
                }
            }
        }
        List<int> frameList = new List<int>();
        for (int i = 0; i < 10; i++) {//only return complete frames
            if (ballsNeeded[i] == 0) {
                frameList.Add(frames[i]);
            }
        }
        return frameList;
    }

    //Returns a list of cumulative scores, like a normal score card
    public static List<int> ScoreCumulative(List<int> rolls) {
        int runningTotal = 0;
        List<int> cumulativeScores = new List<int>();
        foreach (int frameScore in ScoreFrames(rolls)) {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }
        return cumulativeScores;
    }
}
