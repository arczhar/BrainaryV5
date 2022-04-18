using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarItem : MonoBehaviour
{
    private Image avatarImage;
    public Image avatarSelected;
    private Avatar Avatar;

    void Awake()
    {
        avatarImage = GetComponent<Image>();
    }

    public void SetAvatar(Avatar _avatar)
    {
        Avatar = _avatar;
        avatarImage.sprite = Avatar.AvatarImage;
        avatarSelected.gameObject.SetActive(GlobalVariable.AvatarID == Avatar.AvatarId);
    }

    public void OnClikAvatar()
    {
        foreach (Transform ava in transform.parent)
        {
            ava.GetComponent<AvatarItem>().avatarSelected.gameObject.SetActive(false);
        }

        avatarSelected.gameObject.SetActive(true);
        GlobalVariable.AvatarID = Avatar.AvatarId;
    }
}
