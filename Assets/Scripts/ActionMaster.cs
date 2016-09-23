using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {
        Tidy,
        Reset,
        EndTurn,
        EndGame
    }

    public int[] bowls = new int[25];
    private int bowl = 1;

    //for testing--shouldn't need this for production
    public void setBowlNum(int num) {
        bowl = num;
    }

    public Action Bowl(int pins) {
        if (pins < 0 || pins > 10) {
            throw new UnityException("Argument 'pins' should be from 0 to 10, but was " + pins);
        }
        bowls[bowl] = 10;
        if (pins == 10) {
            bowl += 2;
            return Action.EndTurn;
        }

        if(bowl % 2 != 0) {//middle of frame or last frame
            bowl += 1;
            return Action.Tidy;
        }
        if(bowl % 2 == 0) {
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("No action returned");
    }
}
