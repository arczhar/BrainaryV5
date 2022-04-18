using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : ButtonBox
{
    public Sprite spriteTrue;
    public Sprite spriteFalse;
    private Image btnImage;

    private Choice Choice;
    public Choice SetOption
    {
        get { return Choice;  }
        set {
            Choice = value;
            Text = Choice.Text;
        }
    }

    public override void OnClick()
    {
        base.OnClick();
        if (!GlobalVariable.QuestionEnable)
            return;

        btnImage = GetComponent<Image>();
        btnImage.sprite = Choice.Answer ? spriteTrue : spriteFalse;
        NetworkIO.Send("MSG:ANSWER", GlobalVariable.CurrentIndexQuestion, Choice.Answer);
    }
}
