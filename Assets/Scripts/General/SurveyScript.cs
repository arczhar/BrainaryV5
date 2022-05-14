using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class SurveyScript : MonoBehaviour
{

    public TMP_Text namet;
    public TMP_Text course;
    public TMP_Text gender;
    public TMP_Text totalgGames;
    public TMP_Text totalWins;
    public TMP_Text totalloose;
    public TMP_Text mmr;
    public TMP_Text pretestScore;
    public TMP_Text posttestScore;

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfsorzK8fcwJ2hG1mpgwlnVnYjxPbsRZnCHW4dTD_2UlR2ROA/formResponse";


    public void Send()
    {
        StartCoroutine(Post(namet.text));
        //StartCoroutine(Post(course.text));
        //StartCoroutine(Post(gender.text));
        //StartCoroutine(Post(totalgGames.text));
        //StartCoroutine(Post(totalWins.text));
        //StartCoroutine(Post(totalloose.text));
        //StartCoroutine(Post(mmr.text));
        //StartCoroutine(Post(pretestScore.text));
        //StartCoroutine(Post(posttestScore.text));
    }

    IEnumerator Post(string s1)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.363315195", s1); //Name
        /*
        form.AddField("entry.393028410", s1); //Course
        form.AddField("entry.330611923", s1); //gender
        form.AddField("entry.1100045297", s1); //totalgames
        form.AddField("entry.1036641033", s1); //totalwins
        form.AddField("entry.526091228", s1); //totalloose
        form.AddField("entry.569300200", s1); //mmr
        form.AddField("entry.382311103", s1); //pretestscore
        form.AddField("entry.647715252", s1); //posttestscore


        */

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

    }

   

}