using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class QuestionGeneratorTestScript {

    [Test]
    public void GenerateQuestion_Instruction_Not_Null_Test()
    {
        // Use the Assert class to test conditions.
        var questionGenerator = GameObject.Find("Question Generator");
        CognitiveInstruction c = questionGenerator.GetComponent<QuestionGenerator>().GenerateQuestion();
        Assert.AreNotEqual(c.instruction, null);

    }

    [Test]
    public void GenerateQuestion_ProduceOptions_Not_Null_Test()
    {
        // Use the Assert class to test conditions.
        var questionGenerator = GameObject.Find("Question Generator");
        CognitiveInstruction c = questionGenerator.GetComponent<QuestionGenerator>().GenerateQuestion();
        Assert.AreNotEqual(c.ProduceOptions(), null);

    }

    [Test]
    public void GenerateQuestion_ProduceRightAnswers_Not_Empty_Test()
    {
        // Use the Assert class to test conditions.
        var questionGenerator = GameObject.Find("Question Generator");
        CognitiveInstruction c = questionGenerator.GetComponent<QuestionGenerator>().GenerateQuestion();
        Assert.AreNotEqual(c.ProduceRightAnswers(c.ProduceOptions()).Count, 0);

    }
}
