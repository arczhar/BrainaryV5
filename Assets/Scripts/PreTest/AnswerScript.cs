﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{

    public bool isCorrect = false;
    public PreTestManager pretestManager;

    public Color startColor;

    private void Start()
    {
        //startColor = GetComponent<Image>().color;
    }
    public void Answer()
    {
        if(isCorrect)
        {
          //  GetComponent<Image>().color = Color.green;
         
            pretestManager.correct();
        }
        else
        {
            //GetComponent<Image>().color = Color.red;
    
            pretestManager.wrong();
        }
    }
}
