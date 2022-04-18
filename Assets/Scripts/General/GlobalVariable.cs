using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GlobalVariable : Singleton<GlobalVariable>
{
    public static string UserID
    {
        get
        {
            if (string.IsNullOrEmpty(PlayerPrefs.GetString("UserID", "")))
                PlayerPrefs.SetString("UserID", Utils.GenerateKey(8));

            return PlayerPrefs.GetString("UserID", "");
        }

    }


    public static int AvatarID
    {
        get { return PlayerPrefs.GetInt("AvatarID", 0); }
        set { PlayerPrefs.SetInt("AvatarID", value); }
    }

    public static string AvatarName
    {
        get { return PlayerPrefs.GetString("AvatarName", ""); }
        set { PlayerPrefs.SetString("AvatarName", value);  }
    }


    public static int TotalWar
    {
        get { return PlayerPrefs.GetInt("TotalWar", 0); }
        set { PlayerPrefs.SetInt("TotalWar", value); }
    }

    public static int TotalWin
    {
        get { return PlayerPrefs.GetInt("TotalWin", 0); }
        set { PlayerPrefs.SetInt("TotalWin", value); }
    }

    public static int TotalLose
    {
        get { return PlayerPrefs.GetInt("TotalLose", 0); }
        set { PlayerPrefs.SetInt("TotalLose", value); }
    }

    public static int TotalScore
    {
        get { return PlayerPrefs.GetInt("TotalScore", 0); }
        set { PlayerPrefs.SetInt("TotalScore", value); }
    }

    private int currentIndexQuestion;
    public static int CurrentIndexQuestion
    {
        get { return instance.currentIndexQuestion; }
        set { instance.currentIndexQuestion = value; }
    }


    private bool questionEnable;
    public static bool QuestionEnable
    {
        get { return instance.questionEnable; }
        set { instance.questionEnable = value; }
    }

    public static void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public static Avatar Avatar
    {
        get {
            return instance.Avatars.FirstOrDefault(x => x.AvatarId == AvatarID);
        }
    }

    public static Avatar GetAvatarByID(int _id)
    {
        return instance.Avatars.FirstOrDefault(x => x.AvatarId == _id);
    }

    [HideInInspector]
    public bool ExpandingAvatar;
    public List<Avatar> Avatars;

    [HideInInspector]
    public bool ExpandingQuiz;
    public List<Quiz> Quizzes;
}
