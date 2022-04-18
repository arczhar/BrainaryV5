using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupButton
{
    Yes, YesNo
}

public class Popup : Singleton<Popup>
{
    public Button buttonConfirmYes;
    public Button buttonConfirmNo;

    internal Action<bool> callback;

    private object[] _params;

    public object[] Params
    {
        get { return instance._params; }
        set { instance._params = value;  }
    }


    public static void Show(string _parentGameObject, string _popupName, PopupButton _popupButton, Action<bool> _callback = null, params object[] _params)
    {
        GameObject _parent = GameObject.Find(_parentGameObject);
        if (_parent)
        {
            GameObject newObject = Instantiate(Resources.Load<GameObject>("Prefabs/Popup/" + _popupName));
            newObject.transform.SetParent(_parent.transform);
            newObject.transform.localScale = Vector3.one;

            RectTransform rectTransform = newObject.GetComponent<RectTransform>();
            rectTransform.SetLeft(0);
            rectTransform.SetTop(0);
            rectTransform.SetRight(0);
            rectTransform.SetBottom(0);


            if(instance.buttonConfirmYes != null)
                instance.buttonConfirmYes.onClick.AddListener(instance.OnClickYes);
            if (instance.buttonConfirmNo != null)
                instance.buttonConfirmNo.onClick.AddListener(instance.OnClickNo);

            if (_popupButton == PopupButton.Yes)
            {
                if (instance.buttonConfirmNo != null)
                    instance.buttonConfirmNo.gameObject.SetActive(false);
            }

            instance.callback = _callback;
            instance.Params = _params;

            instance.CustomCreated();
        }
        else
        {
            Debug.LogError("Cannot find parent object with name " + _parentGameObject);
            return;
        }
    }

    public virtual void CustomCreated()
    {

    }

    public virtual void OnClickYes()
    {
        if (callback != null)
            callback.Invoke(true);

        Destroy(gameObject);
    }

    public virtual void OnClickNo()
    {
        if (callback != null)
            callback.Invoke(false);
    }
}
