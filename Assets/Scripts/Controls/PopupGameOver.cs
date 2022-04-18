using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGameOver : Popup
{
    public TextBox winText;
    public Image Throphy;
    public Sprite spriteWin;
    public Sprite spriteLose;
    public TextBox textScore;

    public GameObject panelReason;

    public override void CustomCreated()
    {
        base.CustomCreated();

        winText.text = (bool)Params[0] ? "You Win!" : "You Lose!";
        Throphy.sprite = (bool)Params[0] ? spriteWin : spriteLose;
        panelReason.SetActive((bool)Params[1]);
        textScore.text = Params[2].ToString();
    }

    public override void OnClickYes()
    {
        if (callback != null)
            callback.Invoke(true);
    }
}
