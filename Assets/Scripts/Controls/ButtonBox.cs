using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBox : MonoBehaviour
{
    private TextBox TextBox;
    private Button button;

    public string Text
    {
        get { return TextBox.text; }
        set { TextBox.text = value; }
    }

    void Awake()
    {
        TextBox = GetComponentInChildren<TextBox>();
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }


    public virtual void OnClick()
    {

    }
}
