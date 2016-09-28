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
}