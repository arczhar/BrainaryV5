
using PlayerIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class NetworkIO : Singleton<NetworkIO>
{


    private Connection playerIoCon;
    private Client client;
    private static Action Callback;





    public static Client Client
    {
        get { return instance.client; }
    }

    public static bool Connected
    {

        get
        {
            if (instance.playerIoCon == null)
                return false;

            return instance.playerIoCon.Connected;
        }
    }

    public ServerOptions ServerOptions;

    private bool joinRandomRoom;
    private List<Message> msgList = new List<Message>();


    void Start()
    {
        ServerOptions.Quiz = null;
    }

    public static void StartMatchmaking()
    {
        instance.JoinOrCreateRoom();
    }

    public static void StartMatchmaking(Action _callback)
    {
        Callback = _callback;
        instance.JoinOrCreateRoom();
    }

    public static void Auth()
    {
        if (!Connected)
            instance.Authentication();

    }

    void Authentication()
    {
        PlayerIO.Authenticate(ServerOptions.GameId, "public",
                new Dictionary<string, string> {
                    { "userId", GlobalVariable.UserID }
                },
                null,
                delegate (Client _client)
                {
                    client = _client;
                    
#if BUILD_RELEASE

                    Client.Multiplayer.GameServerEndpointFilter = GameServerEndpointFilterDelegate;
#else
                    Client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);
#endif

                },
                delegate (PlayerIOError error)
                {
                    Debug.LogError(error.Message);
                    Disconnect();
                }

        );
    }

    void JoinOrCreateRoom()
    {
        Client.Multiplayer.ListRooms(
                ServerOptions.RoomClass,
                null,
                0,
                0,
                delegate (RoomInfo[] roomInfo)
                {
                    foreach (RoomInfo rm in roomInfo)
                    {
                        if (rm.RoomData["game:state"].Equals("WAITING") && rm.RoomData["game:topic"].Equals(ServerOptions.Quiz.Topic))
                        {
                            JoinRoom(client, rm);
                            joinRandomRoom = true;
                            break;
                        }
                    }

                    if (!joinRandomRoom)
                        CreateRoom(client, true);


                }
            );
    }

    void JoinRoom(Client client, RoomInfo _roomInfo)
    {
        client.Multiplayer.JoinRoom(
            _roomInfo.Id,
            new Dictionary<string, string>
            {
                //JOIN DATA
                { "avatar:name", GlobalVariable.AvatarName},
                { "avatar:icon", GlobalVariable.AvatarID.ToString()},
                { "avatar:mmr",  GlobalVariable.TotalScore.ToString() } //<-- You forget to add this
            },
            delegate (Connection connection) {
                if (Callback != null)
                    Callback.Invoke();
                Popup.Show("UI", "PopupMatchmaking", PopupButton.Yes, OnPopupMatchmakingCallback);
                playerIoCon = connection;
                playerIoCon.OnMessage += HandleMessage;
                joinRandomRoom = true;
            });
    }

    void CreateRoom(Client client, bool _visible)
    {
        Debug.Log(GlobalVariable.TotalScore.ToString());
        string UniqueRoom = string.Format("Brainary#{0}", Utils.GenerateKey(8));
        client.Multiplayer.CreateJoinRoom(
            UniqueRoom,
            ServerOptions.RoomClass,
            _visible,
            new Dictionary<string, string>
            {
                { "game:version", ServerOptions.Version },
                { "game:state", "WAITING" },
                { "game:topic", ServerOptions.Quiz.Topic },
                { "game:answer_time", ServerOptions.Quiz.AnswerTime.ToString() },
                { "game:wait_next_question", ServerOptions.Quiz.WaitNextQuestion.ToString() },
                { "game:question", ServerOptions.Quiz.Questions.Count.ToString() }
            },
            new Dictionary<string, string>
            {
                //JOIN DATA
                { "avatar:name", GlobalVariable.AvatarName},
                { "avatar:icon", GlobalVariable.AvatarID.ToString()},
                { "avatar:mmr",  GlobalVariable.TotalScore.ToString() }
            },
            delegate (Connection connection) {
                if (Callback != null)
                    Callback.Invoke();

                Popup.Show("UI", "PopupMatchmaking", PopupButton.Yes, OnPopupMatchmakingCallback);
                playerIoCon = connection;
                playerIoCon.OnMessage += HandleMessage;
                joinRandomRoom = true;
            },
            delegate (PlayerIOError error) {
                joinRandomRoom = false;
                Disconnect();
            }
        );
    }

    void HandleMessage(object sender, Message msg)
    {
        msgList.Add(msg);
    }

    void Update()
    {
        ProcessMessage();
    }

    private void ProcessMessage()
    {
        foreach (Message m in msgList)
        {
            switch (m.Type)
            {
                case "MSG:LOAD_GAME":
                    GlobalVariable.LoadScene("GamePlay");
                    GlobalVariable.TotalWar += 1;
                    break;
                case "MSG:VERSION_ERROR":

                    break;
                case "MSG:NOT_RESPONSE_MATCHMAKING":
                    Debug.LogError("NO ENEMY");
                    break;
                case "MSG:SUFFLE":
                    int[] numbers = Utils.ConvertToIntArray(m.GetByteArray(0));
                    foreach (int n in numbers)
                    {
                        GameManager.Instance.Add(ServerOptions.Quiz.Questions[n]);
                    }

                    //Your code wrong index
                    //m.GetString(1).Equals(GlobalVariable.UserID) ? m.GetString(4) : m.GetString(2),
                    //m.GetString(1).Equals(GlobalVariable.UserID) ? m.GetString(5) : m.GetString(3),
                    //m.GetString(1).Equals(GlobalVariable.UserID) ? m.GetString(7) : m.GetString(4)


                    //Fix
                    UIGame.Instance.SetOpponentData(
                            m.GetString(1).Equals(GlobalVariable.UserID) ? m.GetString(5) : m.GetString(2),
                            m.GetString(1).Equals(GlobalVariable.UserID) ? m.GetString(6) : m.GetString(3),
                            m.GetString(1).Equals(GlobalVariable.UserID) ? m.GetString(7) : m.GetString(4)
                        );
                    break;
                case "MSG:COUNTDOWN":
                    UIGame.Instance.countdownText.text = m.GetInt(0) > 0 ? m.GetInt(0).ToString() : "Go!";
                    break;
                case "MSG:STATE":
                    GameManager.Instance.GameStateChange(m.GetInt(0));
                    UIGame.Instance.countdownText.transform.parent.gameObject.SetActive(false);
                    break;
                case "MSG:SET_QUESTION":
                    GlobalVariable.QuestionEnable = true;
                    GlobalVariable.CurrentIndexQuestion = m.GetInt(1);
                    GameManager.Instance.SetQuestion(m.GetString(0), m.GetInt(1));
                    break;
                case "MSG:TIME_ANSWER":
                    UIGame.Instance.SetTimeAnswer(m.GetFloat(0), m.GetFloat(1));
                    break;
                case "MSG:NOTIFICATION":
                    GlobalVariable.QuestionEnable = false;
                    if (!GlobalVariable.UserID.Equals(m.GetString(0)))
                    {
                        UIGame.Instance.ShowAlert("Openent has already answered!", AlertType.Danger);
                    }

                    UIGame.Instance.UpdateScore(m.GetString(0), new int[] { m.GetInt(1), m.GetInt(2) });
                    break;
                case "MSG:EMOTICON":
                    UIGame.Instance.ReceiveEmoticon(m.GetString(0), m.GetInt(1));
                    break;
                case "MSG:ATTACK":
                    if (!GlobalVariable.UserID.Equals(m.GetString(0)))
                    {
                        UIGame.Instance.ShowAlert("Attack Incoming!", AlertType.Danger);
                    }

                    UIGame.Instance.ReceiveAttack(m.GetString(0), m.GetInt(1));
                    break;
                case "MSG:GAMEOVER":
                    bool IamWinner = GlobalVariable.UserID == m.GetString(0);

                    if (IamWinner)
                    {
                        GlobalVariable.TotalWin += 1;
                        GlobalVariable.TotalScore += +25;
                    }
                    else
                    {
                        GlobalVariable.TotalLose += 1;
                    }

                    int score = IamWinner ? m.GetInt(1) : GlobalVariable.TotalScore += -25;

                    Popup.Show("UI", "PopupGameOver", PopupButton.Yes, OnPopupContinue, IamWinner, m.GetBoolean(2), score);
                    Disconnect();
                    break;
            }
        }

        msgList.Clear();
    }

    private List<ServerEndpoint> GameServerEndpointFilterDelegate(List<ServerEndpoint> _endpoints)
    {
        List<int> portRemoves = new List<int>() { 80, 443 };

        foreach (int port in portRemoves)
        {
            _endpoints.Remove(_endpoints.Where(x => x.Port == port).FirstOrDefault());
        }
        return _endpoints;
    }


    public static void Disconnect()
    {
        if (instance.playerIoCon != null)
        {
            instance.joinRandomRoom = false;
            instance.playerIoCon.Disconnect();
        }

    }

    public static void Send(string _key, params object[] _params)
    {
        instance.playerIoCon.Send(_key, _params);
    }

    //CALLBACK POPUP
    void OnPopupMatchmakingCallback(bool _confirm)
    {
        Disconnect();
    }

    void OnPopupContinue(bool _confirm)
    {
        GlobalVariable.LoadScene("MainMenu");
    }

    void OnApplicationQuit()
    {
        Disconnect();
    }
}
