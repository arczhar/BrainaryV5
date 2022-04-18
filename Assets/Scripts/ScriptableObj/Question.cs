using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{

    [TextArea(0, 30)]
    public string Text;
    [HideInInspector]
    [SerializeField]
    public Choice[] Choices = new Choice[4];
    [HideInInspector]
    public bool Expanding = true;

}

[System.Serializable]
public class Choice
{
    public string Text;
    public bool Answer;
}
