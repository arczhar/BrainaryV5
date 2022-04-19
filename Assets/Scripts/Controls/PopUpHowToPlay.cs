using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpHowToPlay : Popup
{
    public GameObject paneHowToPlay;
    public GameObject backBtn;

    public GameObject firstTut;
    public GameObject first_btnNext;
    


    public GameObject SecondTut;
    public GameObject second_btnNext;
    public GameObject second_btnPrev;

    public GameObject thirdTut;
    public GameObject third_btnNext;
    public GameObject third_btnPrev;


    public GameObject Forthtut;
    public GameObject forth_btnNext;
    public GameObject forth_btnPrev;



    public GameObject FifthTut;
    public GameObject fifth_btnPrev;
    public GameObject fifth_btnNext;

    public GameObject SixthTut;
    public GameObject sixth_btnPrev;



    public void FirsttutNext(int choice)
    {
        
        firstTut.SetActive(false);
        first_btnNext.SetActive(false);
        
        SecondTut.SetActive(true);
        second_btnNext.SetActive(true);
        second_btnPrev.SetActive(true);
    }

    public void SeconddTutPrev()
    {
        SecondTut.SetActive(false); 
        second_btnNext.SetActive(false);
        second_btnPrev.SetActive(false);

        firstTut.SetActive(true);
        first_btnNext.SetActive(true);
    }

    public void SeconddTutNext()
    {
        SecondTut.SetActive(false);
        second_btnNext.SetActive(false);
        second_btnPrev.SetActive(false);
        
        thirdTut.SetActive(true);
        third_btnNext.SetActive(true);
        third_btnPrev.SetActive(true);
    }

    public void thirddTuPrev()
    {
        thirdTut.SetActive(false);
        third_btnNext.SetActive(false);
        third_btnPrev.SetActive(false);
       
        SecondTut.SetActive(true);
        second_btnNext.SetActive(true);
        second_btnPrev.SetActive(true);


    }

    public void thirdTutNext()
    {
        thirdTut.SetActive(false);
        third_btnNext.SetActive(false);
        third_btnPrev.SetActive(false);

        Forthtut.SetActive(true);
        forth_btnNext.SetActive(true);
        forth_btnPrev.SetActive(true);

    }

    public void forthhTutPrev()
    {
        Forthtut.SetActive(false);
        forth_btnNext.SetActive(false);
        forth_btnPrev.SetActive(false);


        thirdTut.SetActive(true);
        third_btnNext.SetActive(true);
        third_btnPrev.SetActive(true);

    }

    public void forthhTutNext()
    {
       
        Forthtut.SetActive(false);
        forth_btnNext.SetActive(false);
        forth_btnPrev.SetActive(false);


        FifthTut.SetActive(true);
        fifth_btnPrev.SetActive(true);
        fifth_btnNext.SetActive(true);
    }

    public void fifthhTutPrev()
    {
        FifthTut.SetActive(false);
        fifth_btnPrev.SetActive(false);
        fifth_btnNext.SetActive(false);


        Forthtut.SetActive(true);
        forth_btnNext.SetActive(true);
        forth_btnPrev.SetActive(true);
    }
    public void fifthTutNext()
    {
        FifthTut.SetActive(false);
        fifth_btnPrev.SetActive(false);
        fifth_btnNext.SetActive(false);

        SixthTut.SetActive(true);
        sixth_btnPrev.SetActive(true);
    }

    public void SixPrev()
    {
        SixthTut.SetActive(false);
        sixth_btnPrev.SetActive(false);

        FifthTut.SetActive(true);
        fifth_btnPrev.SetActive(true);
        fifth_btnNext.SetActive(true);


    }



    public void OnClickBack(int choice)
    {

        paneHowToPlay.SetActive(false);

    }
}
