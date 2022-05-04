using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{

    public bool isCorrect = false;
    public PreTestManager pretestManager;

    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct");
            pretestManager.correct();
        }
        else
        {
            Debug.Log("Wrong");
            pretestManager.wrong();
        }
    }
}
