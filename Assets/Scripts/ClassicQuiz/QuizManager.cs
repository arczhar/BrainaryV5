using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private List<QuestionD> questionDs;

    private QuestionD selectedQuestion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

}
[SerializeField]
public class QuestionD
{
    public string questionInfo;
    public QuestionType quesType;
    public Sprite questionImg;
    public AudioClip questionClip;
    public UnityEngine.Video.VideoClip questionVideo;
    public List<string> options;
    public string correctAns;
}
[SerializeField]
public enum QuestionType
{
    TEXT,
    IMAGE,
    VIDEO,
    AUDIO
}
