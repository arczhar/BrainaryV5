using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFreeze : MonoBehaviour
{
    public static AttackFreeze instance;
    public RectTransform recTransform;

    public float Duration = 5;
    private float originValue;
    [HideInInspector]
    public bool isAttack;

    void Awake()
    {
        instance = this;
        recTransform.gameObject.SetActive(false);

        originValue = Duration;
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
            Duration -= Time.deltaTime;
            if (Duration < 0)
            {
                isAttack = false;
                recTransform.gameObject.SetActive(false);
                Duration = originValue;
            }
        }
    }
}
