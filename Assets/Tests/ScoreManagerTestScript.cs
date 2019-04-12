using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ScoreManagerTestScript {

    [Test]
    public void ScoreManager_Score_Zero_Or_Above_Test() {
        // Use the Assert class to test conditions.
        var scoreManager = GameObject.Find("Score Manager");
        int testScore = scoreManager.GetComponent<ScoreManager>().GetEndScore();
        Assert.GreaterOrEqual(testScore, 0);
    }

    [Test]
    public void ScoreManager_UpdateScore_Increases_Score_Test()
    {
        // Use the Assert class to test conditions.
        var scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        int oldScore = scoreManager.GetEndScore();
        scoreManager.UpdateScore();
        int newScore = scoreManager.GetEndScore();

        Assert.Greater(newScore, oldScore);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator ScoreManagerTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
