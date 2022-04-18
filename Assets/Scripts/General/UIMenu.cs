using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class UIMenu : MonoBehaviour
{
    public Text AvatarName;
    public Image AvatarImage;

    public TextBox textTotalWar;
    public TextBox textTotalWin;
    public TextBox textTotalLose;
    public TextBox textScore;

    void Start()
    {
        if (string.IsNullOrEmpty(GlobalVariable.AvatarName))
        {
            Popup.Show("UI", "PopupAvatar", PopupButton.Yes, OnPopupAvatarCallback);
        }


        OnPopupAvatarCallback(true);
        NetworkIO.Auth();

        FetchStats();

        

    }

    void FetchStats()
    {
        textTotalWar.text = string.Format("{0}x", GlobalVariable.TotalWar);
        textTotalWin.text = string.Format("{0}x", GlobalVariable.TotalWin);
        textTotalLose.text = string.Format("{0}x", GlobalVariable.TotalLose);
        textScore.text = string.Format("{0}", GlobalVariable.TotalScore);
    }

    public void OnClickAvatar()
    {
        Popup.Show("UI", "PopupAvatar", PopupButton.Yes, OnPopupAvatarCallback);
    }

    
    public void OnLoadScene(string _sceneName)
    {
        
    }

    public void ChooseCategory()
    {
        Popup.Show("UI", "PopupCategory", PopupButton.YesNo, OnPopupCategoryCallback);
    }

    //CALLBACK POPUP
    void OnPopupAvatarCallback(bool _confirm)
    {
        AvatarName.text = GlobalVariable.AvatarName;
        AvatarImage.sprite = GlobalVariable.Avatar.AvatarImage;
    }

    void OnPopupCategoryCallback(bool _confirm)
    {

    }
}
