using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScriptP : MonoBehaviour
{
    //POST TEST

    public bool isCorrect = false;
    public PostTesTManager postTestManager;

    public void Answer()
    {
        if (isCorrect)
        {
            //  GetComponent<Image>().color = Color.green;
            
            postTestManager.correct();
        }
        else
        {
            //GetComponent<Image>().color = Color.red;
          
            postTestManager.wrong();
           
        }


    }



}
