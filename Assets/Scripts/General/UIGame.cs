using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum AlertType
{
    Info, Danger
}

public class UIGame : MonoBehaviour
{
    public static UIGame Instance;
    public TextBox questionText;
    public TextBox valueOfText;

    public GameObject LoadingScreen;
    public GameObject groupAnswer;
    public GameObject prefabAnswer;

    public TextBox countdownText;
    public Image TimeAnswer;

    [Header("Player Stats")]
    public Image ownerAvatar;
    public Image ownerAvatarT;
    public TextBox ownerName;
    private Sprite originOwnerSprite;
    public Image opponentAvatar;
    public Image oppnentAvatarT;
    private Sprite originOpponentSprite;
    public TextBox opponentName;
    public TextBox oppnentNameT;

    public TextBox ownerScore;
    public TextBox opponentScore;

    public TextMeshProUGUI textAlert;
    public Color colorInfo;
    public Color colorDanger;

    private Queue<IEnumerator> coroutineQueueEmoticon = new Queue<IEnumerator>();

    public Sprite[] emoticons;

    void Awake()
    {
        Instance = this;
        textAlert.text = string.Empty;

        if (NetworkIO.instance == null)
        {
            GlobalVariable.LoadScene("MainMenu");
            return;
        }

        StartCoroutine(CoroutineEmoticon());

        NetworkIO.Send("MSG:READY");
        StartCoroutine(showGameUI());
        FetchDaTA();
        
    }

    IEnumerator showGameUI()
    {
        yield return new WaitForSeconds(2);
        LoadingScreen.SetActive(false);

    }

    public void FetchDaTA()
    {
      


    }


    public void SetQuestion(string _valueOf, Question _question)
    {
        questionText.text = _question.Text;
        valueOfText.text = _valueOf;

        foreach (var choice in _question.Choices)
        {
            GameObject newAnswer = Instantiate(prefabAnswer);
            newAnswer.transform.SetParent(groupAnswer.transform);
            newAnswer.transform.localScale = Vector3.one;
            newAnswer.GetComponent<Option>().SetOption = choice;
        }
        
    }

    public void UpdateScore(string _userId, int[] _notification)
    {

        if (_userId.Equals(GlobalVariable.UserID))
        {
            ownerScore.text = _notification[0].ToString();
            opponentScore.text = _notification[1].ToString();
        }
        else
        {
            ownerScore.text = _notification[1].ToString();
            opponentScore.text = _notification[0].ToString();
        }
        
    }

    public void SetOpponentData(string _name, string _icon)
    {
        originOwnerSprite = GlobalVariable.Avatar.AvatarImage;
        originOpponentSprite = GlobalVariable.GetAvatarByID(int.Parse(_icon)).AvatarImage;
        ownerName.text = GlobalVariable.AvatarName;

        ownerAvatar.sprite = originOwnerSprite;
        ownerAvatarT.sprite = originOwnerSprite;
        opponentAvatar.sprite = originOpponentSprite;
        oppnentAvatarT.sprite = originOpponentSprite;

        opponentName.text = _name;
        oppnentNameT.text = _name;
    }

    public void SetTimeAnswer(float _value, float _factor)
    {
        if (_value > 0)
            TimeAnswer.fillAmount = _value / _factor;
        else
            TimeAnswer.fillAmount = 0;
    }

    public void ReceiveEmoticon(string _userId, int _id)
    {
        coroutineQueueEmoticon.Enqueue(ShowEmoticon(_userId, _id));
    }

    private IEnumerator ShowEmoticon(string _userId, int _id)
    {
        if (_userId.Equals(GlobalVariable.UserID))
        {
            ownerAvatar.sprite = emoticons[_id];
            opponentAvatar.sprite = originOpponentSprite;
        }
        else
        {
            ownerAvatar.sprite = originOwnerSprite;
            opponentAvatar.sprite = emoticons[_id];
        }

        yield return new WaitForSeconds(2);

        ownerAvatar.sprite = originOwnerSprite;
        opponentAvatar.sprite = originOpponentSprite;
    }

    private IEnumerator CoroutineEmoticon()
    {
        while (true)
        {
            while (coroutineQueueEmoticon.Count > 0)
                yield return StartCoroutine(coroutineQueueEmoticon.Dequeue());
            yield return null;
        }
    }

    public void ReceiveAttack(string _userId, int _id)
    {
        if (!_userId.Equals(GlobalVariable.UserID))
        {
            switch (_id)
            {
                case 0:
                    AttackFreeze.Attack();
                    break;
                case 1:
                    AttackCameraShake.Attack();
                    break;
                case 2:
                    AttackBlink.Attack();
                    break;
            }
        }
    }


    public void Reset()
    {
        foreach (Transform n in groupAnswer.transform)
        {
            Destroy(n.gameObject);
        }

        TimeAnswer.fillAmount = 1;
    }

    public void SendEmoticon(int _id)
    {
        NetworkIO.Send("MSG:EMOTICON", _id);
    }

    public void ShowAlert(string _text, AlertType _alertType)
    {
        textAlert.color = _alertType == AlertType.Info ? colorInfo : colorDanger;
        textAlert.text = _text;

        StartCoroutine(FadeAlert());
    }

    IEnumerator FadeAlert()
    {
        yield return new WaitForSeconds(2f);
        textAlert.text = string.Empty;
    }

}
