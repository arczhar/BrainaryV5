using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum AttackType
{
    Once, Repeat
}

public class ButtonCard : MonoBehaviour
{
    public AttackType attackType;
    private bool isUsed;

    public float waitingTime = 10;

    private Button button;
    public Image imageCooldown;

    private bool isCooldown;
    private float originTime;

    public Sprite imageEnable;
    public Sprite imageDisable;

    private Image thisImage;
    public int AttackId;

    void Awake()
    {
        button = GetComponent<Button>();
        thisImage = GetComponent<Image>();

        button.onClick.AddListener(OnClick);
        originTime = waitingTime;

        thisImage.sprite = imageEnable;

        imageCooldown.fillAmount = 0;
    }

    void Update()
    {
        if (attackType == AttackType.Repeat)
        {
            if (isCooldown)
            {
                waitingTime -= Time.deltaTime;
                imageCooldown.fillAmount = waitingTime / originTime;
                if (waitingTime <= 0)
                {
                    isCooldown = false;
                    waitingTime = originTime;
                    imageCooldown.fillAmount = 0;
                    thisImage.sprite = imageEnable;
                }
            }
        }
        
    }

    private void OnClick()
    {
        if (attackType == AttackType.Repeat)
        {
            if (!isCooldown)
            {
                NetworkIO.Send("MSG:ATTACK", AttackId);
                isCooldown = true;
                imageCooldown.fillAmount = 1;
                thisImage.sprite = imageDisable;
            }
        }
        else
        {
            if (!isUsed)
            {
                NetworkIO.Send("MSG:ATTACK", AttackId);
                thisImage.sprite = imageDisable;
                isUsed = true;
            }
        }
        
    }

}
