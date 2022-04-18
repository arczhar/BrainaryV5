using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCameraShake : MonoBehaviour
{
    public static AttackCameraShake instance;

    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private float _fakeDelta;

    public RectTransform recTransform;

    public float duration, amount;

    private float originDuration;

    void Awake()
    {
        instance = this;
        originDuration = duration;
        //recTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        _timeAtCurrentFrame = Time.realtimeSinceStartup;
        _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
        _timeAtLastFrame = _timeAtCurrentFrame;
    }

    public static void Attack()
    {
        instance._originalPos = instance.recTransform.localPosition;
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.cShake());
    }

    public IEnumerator cShake()
    {
        float endTime = Time.time + duration;

        while (duration > 0)
        {
            recTransform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= _fakeDelta;

            yield return null;
        }

        recTransform.localPosition = _originalPos;
        duration = originDuration;
    }
}
