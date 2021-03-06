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
    public GameObject panelPretest;
    public GameObject postTestScoreTxt;
    public GameObject popUp;

    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestions;

    public int mmr;
    public int finalmmr;
    public int score;
    public int pretescore;
    int totalQuestion = 0;

    public Text QuestionTxt;
    public TMP_Text ScoreText;
    public TMP_Text mmrText;

   

    public void Start()
    {
      
        generateQuestion();
    }
    private void Update()
    {
        
    }

    private void Awake()
    {
       
        //StartCoroutine(NextQeustion());
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


        if (mmr <= 30 && mmr > 24)
        {
            mmrText.text = ("1400");
            finalmmr = int.Parse(mmrText.text);
            GlobalVariable.TotalScore = finalmmr;

        }
        else if (mmr <= 24 && mmr > 16)
        {
            mmrText.text = ("1100");
            finalmmr = int.Parse(mmrText.text);
            GlobalVariable.TotalScore = finalmmr;
        }
        else if (mmr <= 16 && mmr > 12)
        {
            mmrText.text = ("800");
            finalmmr = int.Parse(mmrText.text);
            GlobalVariable.TotalScore = finalmmr;
        }
        else if (mmr <= 12 && mmr > 6)
        {
            mmrText.text = ("500");
            finalmmr = int.Parse(mmrText.text);
            GlobalVariable.TotalScore = finalmmr;
        }
        else if (mmr <= 6 && mmr > 0)
        {
            mmrText.text = ("200");
            finalmmr = int.Parse(mmrText.text);
            GlobalVariable.TotalScore = finalmmr;
        }

    }

    public void saveMMR()
    {
        GlobalVariable.PreTestScore = score;
        panelPretest.SetActive(false);
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;

            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestions].Answer[i];

            if (QnA[currentQuestions].CorrectAnswer == i + 1)
            {

                options[i].GetComponent<AnswerScript>().isCorrect = true;

            }

        }
    }

    void AutoNext()
    {

    }

    IEnumerator NextQeustion()
    {
        WaitForSeconds wait = new WaitForSeconds(5);
        for (int i = 0; i < 30 ; i++)
        { 
            QnA.RemoveAt(currentQuestions);
            generateQuestion();
            yield return wait;
           
        }
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

    void generateQuestion()
    {
        if (QnA.Count > 0)
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
        StartCoroutine(NextQeustion()); //Triger sin Caroutine
        intructionF.SetActive(false);
        instructionS.SetActive(false);
        testPanel.SetActive(true);

    }
}
