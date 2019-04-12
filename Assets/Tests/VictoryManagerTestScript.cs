using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class VictoryManagerTestScript {

    [Test]
    public void Victory_Before_Not_Achieved_Test()
    {
        // Use the Assert class to test conditions.
        var testVictory = new GameObject();
        testVictory.AddComponent<Victory>();
        var testVictoryComponent = testVictory.GetComponent<Victory>();
        bool before = testVictoryComponent.victoryAchieved;
        Assert.AreEqual(false, before);
    }

    [Test]
    public void Victory_After_PlayerVictory_Test() {
        // Use the Assert class to test conditions.
        var testVictory = GameObject.Find("Victory Manager").GetComponent<Victory>();
        bool before = testVictory.victoryAchieved;
        testVictory.PlayerVictory(1f, 0);
        bool after = testVictory.victoryAchieved;
        Assert.AreNotEqual(before, after);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator VictoryManagerTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
