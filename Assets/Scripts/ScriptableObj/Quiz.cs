using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "HeadBoxGames/Quiz", order = 1)]
public class Quiz : ScriptableObject
{
    public string Topic;
    public Sprite Icon;

    public int AnswerTime = 10;
    public int WaitNextQuestion = 1;

    [SerializeField]
    public List<Question> Questions = new List<Question>();
}
