using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pretest : MonoBehaviour
{

    public GameObject intructionF;
    public GameObject instructionS;
    public GameObject testPanel;




    public void Next()
    {
        intructionF.SetActive(false);
        instructionS.SetActive(true);
        
    }

    public void showTest()
    {
        intructionF.SetActive(false);
        instructionS.SetActive(false);
        testPanel.SetActive(true);

    }
    
}
