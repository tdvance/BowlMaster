using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    private ActionMaster actionMaster;

    [SetUp]
    public void SetUp() {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T01_OneStrikeReturnsEndTurn() {
        //strike on frame 1
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02_BowlEightReturnsTidy() {
        //8 on top of frame 2
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03_BowlTwoReturnsEndTurn() { //spare on frame 2
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T03a_YoutubeTest() {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));

        Assert.AreEqual(endTurn, actionMaster.Bowl(10));

        Assert.AreEqual(tidy, actionMaster.Bowl(3));
        Assert.AreEqual(endTurn, actionMaster.Bowl(4));

        Assert.AreEqual(endTurn, actionMaster.Bowl(10));

        Assert.AreEqual(tidy, actionMaster.Bowl(2));
        Assert.AreEqual(endTurn, actionMaster.Bowl(8));

        Assert.AreEqual(endTurn, actionMaster.Bowl(10));

        Assert.AreEqual(endTurn, actionMaster.Bowl(10));

        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(endTurn, actionMaster.Bowl(0));

        Assert.AreEqual(endTurn, actionMaster.Bowl(10));

        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(reset, actionMaster.Bowl(2));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));

    }


    [Test]
    public void T04Bowl28SpareReturnsEndTurn() {
        actionMaster.Bowl(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T05CheckResetAtStrikeInLastFrame() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T06CheckResetAtSpareInLastFrame() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        actionMaster.Bowl(1);
        Assert.AreEqual(reset, actionMaster.Bowl(9));
    }

    [Test]
    public void T07YouTubeRollsEndInEndGame() {
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T08GameEndsAtBowl20() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T10Bowl20TidyAfterStrike() {
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 10 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(9));
    }

    [Test]
    public void T09ZeroThenTen() {
        for (int i = 0; i < 9; i++) {
            actionMaster.Bowl(0);
            Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        }
    }

    [Test]
    public void T11Bowl20TidyAfterStrike2() {
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 10 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T12ZeroTenFiveOne() {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(5));
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void T13TenthFrameTurkey() {
        int[] rolls = { 1, 1,  1, 1,  1, 1,  1, 1,  1, 1,  1, 1,  1, 1,  1, 1,  1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));

    }

    [Test]
    public void T1010thFrameStrikeOpenFrame() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T11PerfectGame() {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T12GutterBallMania() {
        int[] rolls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T13SpareStrikeInTenthFrame() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(reset, actionMaster.Bowl(2));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T14TurkeyMidGame() {
        int[] rolls = { 10, 10, 10, 10, 10 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T15Open10th() {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(9));
        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T16SpareIn9th() {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(9));
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T17LotsOfSpares() {
        int[] rolls = { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(9));
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T18StrikeSpareIn10th() {
        int[] rolls = { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(7));
        Assert.AreEqual(endGame, actionMaster.Bowl(3));
    }

    [Test]
    public void T19StrikeAsSpareMidGame() {
        int[] rolls = { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1 };
        foreach (int roll in rolls) {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(5));
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }


}

