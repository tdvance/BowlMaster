
//Make T01Bowl23 pass

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

        return frameList;
    }



//Make T02Bowl234 pass

