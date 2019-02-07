using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {

    public bool isCleared = false;

    public abstract void InteractWithObstacle(string instruction);

    public abstract void UpdateInstructionForObstacle();

    virtual public void ActivateObstacle() { }

    virtual public bool CheckAnswer(string answer) {
        return false;
    }
}
