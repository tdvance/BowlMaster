using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster actionMaster;

    [SetUp]
    public void SetUp() {
        actionMaster = new ActionMaster();
    }

    [Test]
	public void T01_OneStrikeReturnsEndTurn()
	{
        //strike on frame 1
        actionMaster.setBowlNum(1);
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
	}

    [Test]
    public void T02_BowlEightReturnsTidy() {
        //8 on top of frame 2
        actionMaster.setBowlNum(3);
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T02_BowlTwoReturnsEndTurn() { //spare on frame 2
        actionMaster.setBowlNum(4);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }
}

