using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIMenu : MonoBehaviour
{
    public Text AvatarName;
    public Image AvatarImage;
    public Text AvatarNameT;
    public Image AvatarImageT;

    public TextBox textTotalWar;
    public TextBox textTotalWin;
    public TextBox textTotalLose;
    public TextBox textScore;
    public TMP_Text preTestScore;
    public TMP_Text postTestScore;

    public GameObject btnChangeAvatar;

    public GameObject pvpPanel;
    public GameObject mainMenuPanel;
    public GameObject profilePanel;
    public GameObject settingsPanel;
    public GameObject panelHowToPlay;
    public GameObject panelQuitPopUp;
    public GameObject panelCreditPopUp;
    public GameObject panelPreTest;
    public GameObject panelPostTest;
    public GameObject panelAvatar;
    

    public GameObject soundOffIcon;
    public GameObject soundOnIcon;

   


    private bool muted = false;


    void Awake()
    {
            //MMR                              //TotalWar
        if (GlobalVariable.TotalScore == 1500 || GlobalVariable.TotalWar == 1)
        {
            panelPostTest.SetActive(true);
        }
        else
        {
            panelPostTest.SetActive(false);
        }


    }

    void Start()
    {
        showPanelPreTest();
        if (string.IsNullOrEmpty(GlobalVariable.AvatarName))
        {
            Popup.Show("UI", "PopupAvatar", PopupButton.Yes, OnPopupAvatarCallback);
            
        } 
        OnPopupAvatarCallback(true);
        NetworkIO.Auth();
        FetchStats();
        FetchScore();

    }

    void showPanelPreTest()
    {
        if (GlobalVariable.PreTestScore == 0)
        {
            panelPreTest.SetActive(true);
        }
        else
        {
            panelPreTest.SetActive(false);
        }
    }

    public void onClickStart()
    {
        panelPreTest.SetActive(false);
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

    void FetchScore()
    {       
        preTestScore.text = string.Format("{0} / 30", GlobalVariable.PreTestScore);
        postTestScore.text = string.Format("{0} / 30", GlobalVariable.PostTestScore);
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
        AvatarNameT.text = GlobalVariable.AvatarName;
        AvatarImageT.sprite = GlobalVariable.Avatar.AvatarImage;

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
        panelAvatar.SetActive(false); 
        profilePanel.SetActive(true);
        FetchScore();
    }

    public void onClickMainMenu()
    {
        panelAvatar.SetActive(true);
        btnChangeAvatar.SetActive(false);
        profilePanel.SetActive(false);
        settingsPanel.SetActive(false);
        pvpPanel.SetActive(false);
        panelQuitPopUp.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void onClickSettings()
    {
        panelAvatar.SetActive(true);
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
