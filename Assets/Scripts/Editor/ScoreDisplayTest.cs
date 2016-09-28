using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest {

    [SetUp]
    public void SetUp() {
    }

    [Test]
    public void T00PassingTest(){
        Assert.Pass();
    }

    [Test]
    public void T01Bowl1() {
        int[] rolls = { 1 };
        string rollsString = "1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T02BowlX() {
        int[] rolls = { 10 };
        string rollsString = "X";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T03BowlXXX() {
        int[] rolls = { 10, 10, 10 };
        string rollsString = "X X X";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T04Perfect() {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        string rollsString = "X X X X X X X X X XXX";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    // http://slocums.homestead.com/gamescore.html
    [Test]
    [Category("Verification")]
    public void TG01Gold() {
        int[] rolls = { 10, 7, 3, 9, 0, 10, 0, 8, 8, 2, 0, 6, 10, 10, 10, 8, 1 };
        string rollsString = "X 7/9-X -88/-6X X X81";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}