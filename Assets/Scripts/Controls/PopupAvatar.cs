using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupAvatar : Popup
{
    public TMP_InputField InputField;
    public GameObject panelGroupAvatar;


    public override void CustomCreated()
    {
        base.CustomCreated();

        InputField.text = GlobalVariable.AvatarName;
        foreach (var ava in GlobalVariable.instance.Avatars)
        {
            GameObject newObject = Instantiate(Resources.Load<GameObject>("Prefabs/Avatar"));
            newObject.transform.SetParent(panelGroupAvatar.transform);
            newObject.transform.localScale = Vector3.one;

            RectTransform rectTransform = newObject.GetComponent<RectTransform>();
            rectTransform.SetLeft(0);
            rectTransform.SetTop(0);
            rectTransform.SetRight(0);
            rectTransform.SetBottom(0);

            newObject.GetComponent<AvatarItem>().SetAvatar(ava);
        }
    }

    public override void OnClickYes()
    {
        if (string.IsNullOrEmpty(InputField.text))
            return;

        GlobalVariable.AvatarName = InputField.text;

        base.OnClickYes();
    }
}
