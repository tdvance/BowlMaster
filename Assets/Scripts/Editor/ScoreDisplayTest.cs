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
}