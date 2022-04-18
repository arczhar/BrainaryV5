using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBlink : MonoBehaviour
{
    public static AttackBlink instance;
    public RectTransform recTransform;

    [HideInInspector]
    public bool isAttack;

    private Image imageBlink;
    public float MaxBlink = 5;
    private float originValue;
    private Color colorOrigin;

    void Awake()
    {
        instance = this;
        imageBlink = recTransform.GetComponent<Image>();
        originValue = MaxBlink;
        colorOrigin = imageBlink.color;
        recTransform.gameObject.SetActive(false);
    }

    public static void Attack()
    {
        instance.recTransform.gameObject.SetActive(true);
        instance.isAttack = true;
    }

    void Update()
    {
        if (isAttack)
        {
            MaxBlink -= Time.deltaTime;
            if(MaxBlink < 0)
            {
                isAttack = false;
                recTransform.gameObject.SetActive(false);
                MaxBlink = originValue;
                imageBlink.color = colorOrigin;
            }
            else
            {
                imageBlink.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, Mathf.Lerp(imageBlink.color.a, imageBlink.color.a == 1 ? 0 : 1, 50 * Time.deltaTime));
            }

            


        }
    }
}
