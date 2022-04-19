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

    public GameObject btnChangeAvatar;

    public GameObject pvpPanel;
    public GameObject mainMenuPanel;
    public GameObject profilePanel;
    public GameObject settingsPanel;
    public GameObject panelHowToPlay;
    public GameObject panelQuitPopUp;
    public GameObject panelCreditPopUp;

    public GameObject soundOffIcon;
    public GameObject soundOnIcon;



    private bool muted = false;

    void Start()
    {
        if (string.IsNullOrEmpty(GlobalVariable.AvatarName))
        {
            Popup.Show("UI", "PopupAvatar", PopupButton.Yes, OnPopupAvatarCallback);
        }


        OnPopupAvatarCallback(true);
        NetworkIO.Auth();

        //FetchStats();

        

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //panelMainMenu.SetActive(true);
            panelQuitPopUp.SetActive(false);
            panelCreditPopUp.SetActive(false);
            panelHowToPlay.SetActive(false);
        }
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

    public void onClickPvp()
    {
        
        pvpPanel.SetActive(true);
        FetchStats();
    }

    public void onClickProfile()
    {

        btnChangeAvatar.SetActive(true);
        mainMenuPanel.SetActive(false);
        pvpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        profilePanel.SetActive(true);

    }

    public void onClickMainMenu()
    {
        btnChangeAvatar.SetActive(false);
        profilePanel.SetActive(false);
        settingsPanel.SetActive(false);
        pvpPanel.SetActive(false);
        panelQuitPopUp.SetActive(false);
        mainMenuPanel.SetActive(true);
    }


    public void onClickSettings()
    {
        btnChangeAvatar.SetActive(false);
        mainMenuPanel.SetActive(false);
        pvpPanel.SetActive(false);
        profilePanel.SetActive(false);
        panelCreditPopUp.SetActive(false);
        settingsPanel.SetActive(true);
    }

    
    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;

        }

        else
        {
            muted = false;
            AudioListener.pause = false;
        }

        Save();
        UpdateIconBtn();
    }
    
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    private void UpdateIconBtn()
    {

        if (muted == false)
        {
            soundOnIcon.SetActive(true);
            soundOffIcon.SetActive(false);
        }

        else
        {
            soundOnIcon.SetActive(false);
            soundOffIcon.SetActive(true);
        }
    } 

    public void onClickClassic()
    {

    }

    public void showQuit()
    {
        panelQuitPopUp.SetActive(true);
    }

    public void onClickQuit()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void onClickShowCredits()
    {
        panelCreditPopUp.SetActive(true);
    }

    public void OnclickText()
    {
        panelHowToPlay.SetActive(true);

    }









}
