using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public string text
    {
        get { return textMesh.text; }
        set { textMesh.text = value;  }
    }

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
}
