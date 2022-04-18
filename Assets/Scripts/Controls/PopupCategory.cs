using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCategory : Popup
{
    public GameObject panelGroupQuiz;

    public override void CustomCreated()
    {
        base.CustomCreated();
        foreach (var qz in GlobalVariable.instance.Quizzes)
        {
            GameObject newObject = Instantiate(Resources.Load<GameObject>("Prefabs/CategoryItem"));
            newObject.transform.SetParent(panelGroupQuiz.transform);
            newObject.transform.localScale = Vector3.one;

            RectTransform rectTransform = newObject.GetComponent<RectTransform>();
            rectTransform.SetLeft(0);
            rectTransform.SetTop(0);
            rectTransform.SetRight(0);
            rectTransform.SetBottom(0);

            newObject.GetComponent<CategoryItem>().SetCategoryItem(qz);

            if(NetworkIO.instance.ServerOptions.Quiz != null)
                if (qz.Topic.Equals(NetworkIO.instance.ServerOptions.Quiz.Topic))
                {
                    newObject.GetComponent<CategoryItem>().OnClickSelect();
                }
        }

    }

    public override void OnClickYes()
    {
        if (NetworkIO.instance.ServerOptions.Quiz == null)
            return;

        NetworkIO.StartMatchmaking(base.OnClickYes);
    }

    public override void OnClickNo()
    {
        Destroy(gameObject);
        base.OnClickNo();
    }

}
