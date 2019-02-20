using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CognitiveObstacle : Obstacle {

    protected CognitiveInstruction cognitiveInstruction;

    public override void InteractWithObstacle(string instruction)
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckAnswer(string answer)
    {
        return base.CheckAnswer(answer);
    }

    public override void UpdateInstructionForObstacle()
    {
        throw new System.NotImplementedException();
    }

    public override void ActivateObstacle()
    {
        base.ActivateObstacle();
    }


    virtual public void Initialise(CognitiveInstruction instruction){}
}
