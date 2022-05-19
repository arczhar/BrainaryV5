using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class SurveyScript : MonoBehaviour
{
    public GameObject SurveyPanel;
    public GameObject FeedBackPanel;

    public Text namet;
    private string InputName;
    public Text course;
    private string InputCourse;
    public Text gender;
    private string InputGender;
    public TMP_Text totalgGames;
    public TMP_Text totalWins;
    public TMP_Text totalloose;
    public TMP_Text mmr;
    public TMP_Text pretestScore;
    public TMP_Text posttestScore;
    public Text test;
    private string testF;

    public GameObject alert;

    private string Namet;
    private string Course;
    private string Gender;
    private string TotalgGames;
    private string TotalWins;
    private string Totalloose;
    private string Mmr;
    private string PretestScore;
    private string PosttestScore;

    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfsorzK8fcwJ2hG1mpgwlnVnYjxPbsRZnCHW4dTD_2UlR2ROA/formResponse";

    IEnumerator Post(string namee, string coursee, string genderr, string totalgamess, string totalwinss, string totalloose, string mmrr, string pretestScoree, string posttestScoree)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.363315195", namee);
        form.AddField("entry.393028410", coursee);
        form.AddField("entry.330611923", genderr);
        form.AddField("entry.1100045297", totalgamess);
        form.AddField("entry.1036641033", totalwinss);
        form.AddField("entry.526091228", totalloose);
        form.AddField("entry.569300200", mmrr);
        form.AddField("entry.382311103", pretestScoree);
        form.AddField("entry.647715252", posttestScoree);
        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;

    }


    public void SendData()
    {
        InputName = namet.text;
        InputCourse = course.text;
        InputGender = gender.text;

        if (StringExtension.IsNullOrWhiteSpace(InputName) || StringExtension.IsNullOrWhiteSpace(InputCourse) || StringExtension.IsNullOrWhiteSpace(InputGender))
        {
            alert.SetActive(true);
        }
        else
        {

            Namet = namet.GetComponent<Text>().text;
            Course = course.GetComponent<Text>().text;
            Gender = gender.GetComponent<Text>().text;
            TotalgGames = totalgGames.GetComponent<TMP_Text>().text;
            TotalWins = totalWins.GetComponent<TMP_Text>().text;
            Totalloose = totalloose.GetComponent<TMP_Text>().text;
            Mmr = mmr.GetComponent<TMP_Text>().text;
            PretestScore = pretestScore.GetComponent<TMP_Text>().text;
            PosttestScore = posttestScore.GetComponent<TMP_Text>().text;


            StartCoroutine(Post(Namet, Course, Gender, TotalgGames, TotalWins, Totalloose, Mmr, PretestScore, PosttestScore));

           
            FeedBackPanel.SetActive(true);
            SurveyPanel.SetActive(false);

        }
    }
   

}