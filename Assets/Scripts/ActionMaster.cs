using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ActionMaster {

    public enum Action {
        Tidy,
        Reset,
        EndTurn,
        EndGame
    }

    public int[] bowls = new int[21];
    private int bowl = 1;
    private bool debug = false;

    public static Action NextAction(List<int> pinFalls) {
        ActionMaster temporaryInstance = new ActionMaster();
        Action currentAction = Action.EndGame;
        foreach(int numPins in pinFalls) {
            currentAction = temporaryInstance.Bowl(numPins);
        }
        return currentAction;
    }

    public ActionMaster() {
        this.debug = false;
    }

    public ActionMaster(bool debug) {
        this.debug = debug;
    }

    private Action Bowl(int pins) {
        if (debug) {
            Debug.Log("Bowl " + pins);
        }
        if (pins < 0 || pins > 10) {
            throw new UnityException("Argument 'pins' should be from 0 to 10, but was " + pins);
        }
        bowls[bowl - 1] = pins;

        //special cases for tenth frame

        if (bowl == 20) {
            if (bowls[19 - 1] + bowls[20 - 1] < 10) {
                return Action.EndGame;
            } else if (bowls[20 - 1] < 10 && bowls[19 - 1] == 10) {
                bowl += 1;
                return Action.Tidy;
            } else {//bonus frame
                bowl += 1;
                return Action.Reset;
            }
        }

        if (bowl == 21) {
            return Action.EndGame;
        }

        if (bowl == 19 && pins == 10) {//bonus frames
            bowl += 1;
            return Action.Reset;
        }

        //normal cases 

        if (pins == 10) {
            if (bowl % 2 == 0) {
                bowl += 1;
            } else { //strike
                bowl += 2;
            }
            return Action.EndTurn;
        }

        if (bowl % 2 != 0) {//middle of frame
            bowl += 1;
            return Action.Tidy;
        }
        if (bowl % 2 == 0) {
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("No action returned");
    }

}
