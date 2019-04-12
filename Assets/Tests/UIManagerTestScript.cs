using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class UIManagerTestScript {

    [Test]
    public void UIManager_UpdateProgressSliderRanges_Test() {
        // Use the Assert class to test conditions.
        
        var testUIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        int min = 0;
        int max = 30;
        testUIManager.UpdateProgressSliderRanges(min, max);

        var testSlider = GameObject.Find("Progress Slider").GetComponent<Slider>();
        Assert.AreEqual(testSlider.minValue, min);
        Assert.AreEqual(testSlider.maxValue, max);

    }

    [Test]
    public void UIManager_UpdateProgressSliderValue_Test()
    {
        var testUIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        int value = 10;
        testUIManager.UpdateProgressSliderValue(value);
        var testSlider = GameObject.Find("Progress Slider").GetComponent<Slider>();
        Assert.AreEqual(testSlider.value, value);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator UIManagerTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
