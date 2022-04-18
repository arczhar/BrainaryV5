using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WAITING, READY, PLAYING
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameState;

   
    [HideInInspector]
    private List<Question> currentQuestions = new List<Question>();

    public int maxQuestion
    {
        get { return currentQuestions.Count;  }
    }

    void Awake()
    {
        Instance = this;

    }


    public void SetQuestion(string _valueOf, int _index)
    {
        UIGame.Instance.Reset();
        UIGame.Instance.SetQuestion(_valueOf, currentQuestions[_index]);
    }

    public void Add(Question _question)
    {
        currentQuestions.Add(_question);
    }

    public void GameStateChange(int _index)
    {
        GameState = (GameState)Enum.ToObject(typeof(GameState), _index);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    Popup.Show("Selamat Datatng", "UI");
        //    AttackBlink.Attack();
    }

    
}
