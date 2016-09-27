using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster  {

    public enum GameState {
        TopOfFrame,
        MiddleOfFrame,
        BonusRoll,
        GameOver
    };


    // Returns a list of individual (non-cumulative) frame scores
    public static List<int> ScoreFrames(List<int> rolls) {
        List<int> frameList = new List<int>();
        GameState state = GameState.TopOfFrame;
        int frame = -1;
        int index = -1;
        foreach(int pins in rolls) {
            switch (state) {
                case GameState.TopOfFrame:
                    frameList.Add(pins);
                    frame = frameList.Count;
                    index = frame - 1;
                    if(pins < 10) {
                        state = GameState.MiddleOfFrame;
                    }
                    break;
                case GameState.MiddleOfFrame:
                    frameList[index] += pins;
                    if(frame < 10) {
                        state = GameState.TopOfFrame;
                    }
                    break;
                case GameState.BonusRoll:
                    frameList[index] += pins;
                    break;
                case GameState.GameOver:
                    throw new UnityException("Attempt to roll when game is over");
                default:
                    throw new UnityException("Unhandled state " + state);
            }
        }       
        if(state == GameState.MiddleOfFrame) {//special case: frame not done yet
            frameList.RemoveAt(index);
        }
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
