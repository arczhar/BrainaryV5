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
    public GameObject resultPanel;



    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestions;

    public int mmr;
    public int score;
    int totalQuestion = 0;

    public Text QuestionTxt;
    public TMP_Text ScoreText;
    public TMP_Text mmrText;
   
    public void Start()
    {
        
        totalQuestion = QnA.Count;
        generateQuestion();
    }

    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestions);
        generateQuestion();
    }
    public void wrong()
    {
        QnA.RemoveAt(currentQuestions);
        generateQuestion();
    }

     void testDone()
     {
        ScoreText.text = score + "";
        calculateMMR();
        testPanel.SetActive(false);
        resultPanel.SetActive(true);
     }
    void calculateMMR()
    {
        mmr = int.Parse(ScoreText.text);

        if (mmr <= 10)
        {
            mmrText.text = ("500");
     
        }      

    }

    void saveMMR()
    {

        mmrText.text = string.Format("{0}", GlobalVariable.TotalScore);
        //textTotalLose.text = string.Format("{0}x", GlobalVariable.TotalLose);
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
            //Debug.Log("Quiz Done");
            testDone();
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
