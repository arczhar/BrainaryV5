using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryItem : MonoBehaviour
{
    public Color colorDefault;
    public Color colorSelected;
    public GameObject Img;

    public Image Icon;
    //public TextBox Topic;
    private Image selector;

    private Quiz Quiz;

    void Awake()
    {
        selector = GetComponent<Image>();

    }

    public void SetCategoryItem(Quiz _quiz)
    {
        Quiz = _quiz;

        Icon.sprite = Quiz.Icon;
        //Topic.text = Quiz.Topic;
    }

    public void OnClickSelect()
    {

        foreach (Transform t in transform.parent)
        {
            t.GetComponent<CategoryItem>().Reset();
        }

        Img.SetActive(true);
        NetworkIO.instance.ServerOptions.Quiz = Quiz;
    }

    public void Reset()
    {
        Img.SetActive(false);
        //selector.color = colorDefault;
    }
}
