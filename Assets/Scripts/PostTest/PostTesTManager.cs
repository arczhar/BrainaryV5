using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PostTesTManager : MonoBehaviour
{
    [Header("Post Test UI")]
    public GameObject intructionF;
    public GameObject testPanel;
    public GameObject resultPanel;
    public GameObject panelPostTest;
    public GameObject postScorerTxt;
    public GameObject SurveyPanel;
    public int score;
    public int postTestScore;
    int totalQuestion = 0;

    public Text QuestionTxt;
    public TMP_Text ScoreText;

    [Header("Manager")]
    public List<QuestionAndAnswerP> QnA;
    public GameObject[] options;
    public int currentQuestions;

    private void Start()
    {
        totalQuestion = QnA.Count;
        generateQuestion();
    }
  

    public void correct()
    {
        score += 1;
        StartCoroutine(NextQeustion());
    }
    public void wrong()
    {

        StartCoroutine(NextQeustion());
    }
    IEnumerator NextQeustion()
    {
        WaitForSeconds wait = new WaitForSeconds(4.2F);
        for (int i = 0; i < 30; i++)
        {
            QnA.RemoveAt(currentQuestions);
            generateQuestion();
            yield return wait;
        }

    }
    void testDone()
    {
        ScoreText.text = score + "";
        testPanel.SetActive(false);
        resultPanel.SetActive(true);
    }

    public void saveResult()
    {
        GlobalVariable.PostTestScore = score; 
        postScorerTxt.SetActive(true);
        panelPostTest.SetActive(false);
        SurveyPanel.SetActive(true);
    }
    void setAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScriptP>().isCorrect = false;

            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestions].Answer[i];

            if (QnA[currentQuestions].CorrectAnswer == i + 1)
            {

                options[i].GetComponent<AnswerScriptP>().isCorrect = true;

            }

        }
    }
    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestions = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestions].Question;
            setAnswer();
        }
        else
        {
            Debug.Log("Quiz Done");
            testDone();
        }

    }

    public void Next()
    {
        intructionF.SetActive(false);
        testPanel.SetActive(true);
        StartCoroutine(NextQeustion());

    }


}
