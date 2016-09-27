using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster  {
    // Returns a list of individual (non-cumulative) frame scores
    public static List<int> ScoreFrames(List<int> rolls) {
        List<int> frameList = new List<int>();

        

        return frameList;
    }

    //Returns a list of cumulative scores, like a normal score card
    public static List<int> ScoreCumulative(List<int> rolls) {
        int runningTotal = 0;
        List<int> cumulativeScores = new List<int>();
        foreach(int frameScore in ScoreFrames(rolls)) {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }
        return cumulativeScores;
    }
}
