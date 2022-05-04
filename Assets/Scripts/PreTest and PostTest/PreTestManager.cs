using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreTestManager : MonoBehaviour
{
    [Header("Pre Test UI")]
    public GameObject intructionF;
    public GameObject instructionS;
    public GameObject testPanel;



    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestions; 

    public Text QuestionTxt;

    public void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        QnA.RemoveAt(currentQuestions);
        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length ; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestions].Answer[i]; 

            if(QnA[currentQuestions].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestions = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestions].Question;
            SetAnswer();
        }

        else
        {
            Debug.Log("Quiz Done");
        }
        

        
    }


    public void Next()
    {
        intructionF.SetActive(false);
        instructionS.SetActive(true);

    }

    public void showTest()
    {
        intructionF.SetActive(false);
        instructionS.SetActive(false);
        testPanel.SetActive(true);

    }
}
