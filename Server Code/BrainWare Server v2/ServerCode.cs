using PlayerIO.GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainWare_Server_v2
{
    public enum GameState
    {
        WAITING, READY, PLAYING, GAMEOVER
    }

    [RoomType("Brainary")]
    public class ServerCode : Game<Player>
    {
        private string Version = "v1.0.0";

        private int maxPlayer = 2;
        private GameState GameState;
        private int SizeQuestion;

        private Timer TimerMatchmaking;
        private float MaxTimeMatchmaking = 15;
        private Timer TimerCountdown;
        private int MaxTimeCountdown = 4;
        private Timer TimerTimeAnswer;
        private int MaxTimeAnswer = 0;
        private int factorTimeAnswer;
        private Timer TimerNextQuestion;
        private int MaxNextQuestion = 3;
        private int configMaxNextQuestion;

        private List<Question> Questions = new List<Question>();
        private int indexQuestion = 0;
        private string questionOf = "";

        private bool isShuffle;

        public const int QUESTIONLIMIT = 15;

        public override void GameStarted()
        {
            base.GameStarted();
            Console.WriteLine("Game Brain sssWars is started: " + RoomId);


            TimerMatchmaking = AddTimer(NotResponseMatchmaking, 1000);
            TimerCountdown = AddTimer(CountDown, 1000);
            TimerTimeAnswer = AddTimer(TimeAnswer, 1000);

        }

        public override void GameClosed()
        {
            base.GameClosed();
            Console.WriteLine("BrainWars RoomId: " + RoomId);
        }

        public override bool AllowUserJoin(Player player)
        {

            if (!RoomData["game:version"].Equals(Version))
            {
                player.Send("MSG:VERSION_ERROR");
                return false;
            }

            if (Players.Count() < maxPlayer && GameState == GameState.WAITING)
            {

                int.TryParse(RoomData["game:question"], out SizeQuestion);

                int.TryParse(RoomData["game:answer_time"], out MaxTimeAnswer);
                factorTimeAnswer = MaxTimeAnswer;

                int.TryParse(RoomData["game:wait_next_question"], out MaxNextQuestion);
                configMaxNextQuestion = MaxNextQuestion;


                return true;
            }

            return false;

        }

        public override void UserJoined(Player player)
        {
            if (Players.Count() >= maxPlayer)
            {
                Broadcast("MSG:LOAD_GAME");
            }
        }

        public override void UserLeft(Player player)
        {
            if (Players.Count() <= 1)
            {
                if (GameState == GameState.PLAYING)
                    GameOver(true, player);
            }


            Console.WriteLine("Player Left: " + player.ConnectUserId);
        }

        void NotResponseMatchmaking()
        {
            MaxTimeMatchmaking--;
            if (MaxTimeMatchmaking <= 0)
            {

                TimerMatchmaking.Stop();
                MaxTimeMatchmaking = 15;

                Broadcast("MSG:NOT_RESPONSE_MATCHMAKING");
            }
        }

        private void CountDown()
        {
            if (Players.Count() < maxPlayer)
                return;

            MaxTimeCountdown--;
            if (MaxTimeCountdown >= 0)
            {
                Broadcast("MSG:COUNTDOWN", MaxTimeCountdown);
            }
            else
            {
                TimerCountdown.Stop();
                GameState = GameState.PLAYING;
                Broadcast("MSG:STATE", (int)GameState);
                questionOf = string.Format("{0} of {1}", indexQuestion + 1, QUESTIONLIMIT);
                Broadcast("MSG:SET_QUESTION", questionOf, indexQuestion);
            }
        }

        private void TimeAnswer()
        {
            if (GameState == GameState.PLAYING)
            {

                if (MaxTimeAnswer < 0)
                {
                    if (indexQuestion < Questions.Count - 1)
                    {
                        indexQuestion++;
                        questionOf = string.Format("{0} of {1}", indexQuestion + 1, QUESTIONLIMIT);
                        Broadcast("MSG:SET_QUESTION", questionOf, indexQuestion);
                        MaxTimeAnswer = factorTimeAnswer;
                    }
                    else
                    {
                        GameOver();
                        TimerTimeAnswer.Stop();
                    }

                }
                else
                {
                    MaxTimeAnswer--;
                    Broadcast("MSG:TIME_ANSWER", MaxTimeAnswer, factorTimeAnswer);
                }

            }
        }

        public override void GotMessage(Player player, Message m)
        {
            base.GotMessage(player, m);
            switch (m.Type)
            {
                case "MSG:READY":
                    player.UserSuccessLoadScene = true;

                    if (Players.ToList().Count(x => x.UserSuccessLoadScene) >= maxPlayer)
                    {
                        RoomData["game:state"] = "PLAYING";
                        RoomData.Save();

                        TimerMatchmaking.Stop();
                        MaxTimeMatchmaking = 15;

                        Player playerEnemy = Players.SingleOrDefault(x => x.ConnectUserId != player.ConnectUserId);

                        if (!isShuffle)
                        {
                            isShuffle = true;
                            Broadcast("MSG:SUFFLE",
                                Utils.ConvertToByteArray(Utils.SuffleQuestionList(QUESTIONLIMIT, Questions).ToArray()),
                                player.ConnectUserId,
                                player.JoinData["avatar:name"],
                                player.JoinData["avatar:icon"],
                                player.JoinData["avatar:mmr"],
                                playerEnemy.JoinData["avatar:name"],
                                playerEnemy.JoinData["avatar:icon"],
                                playerEnemy.JoinData["avatar:mmr"]
                            );

                        }


                    }
                    break;
                case "MSG:ANSWER":
                    Player owner = Players.SingleOrDefault(p => p.ConnectUserId == player.ConnectUserId);
                    Player opponent = Players.SingleOrDefault(p => p.ConnectUserId != player.ConnectUserId);

                    Question questionAnswered = Questions[m.GetInt(0)];

                    if (m.GetInt(0) >= Questions.Count - 1)
                    {
                        GameOver();
                        return;
                    }
                    else
                    {
                        if (questionAnswered != null)
                        {
                            if (!questionAnswered.Answered)
                            {
                                questionAnswered.Answered = true;

                                bool userAnswer = m.GetBoolean(1);
                                if (userAnswer)
                                {
                                    owner.PlayerScore += 1;
                                }

                                //{0} = Who is first answered
                                //{1} = Owner Score
                                //{2} = Opponent Score

                                Broadcast("MSG:NOTIFICATION", owner.ConnectUserId, owner.PlayerScore, opponent.PlayerScore);

                                TimerTimeAnswer.Stop();
                                TimerNextQuestion = AddTimer(NextQuestion, 1000);
                            }

                        }
                    }


                    break;
                case "MSG:EMOTICON":
                    Broadcast("MSG:EMOTICON", player.ConnectUserId, m.GetInt(0));
                    break;
                case "MSG:ATTACK":
                    Broadcast("MSG:ATTACK", player.ConnectUserId, m.GetInt(0));
                    break;
            }
        }

        private void NextQuestion()
        {
            MaxNextQuestion--;
            if (MaxNextQuestion < 0)
            {
                TimerNextQuestion.Stop();
                MaxNextQuestion = configMaxNextQuestion;


                if (indexQuestion < Questions.Count - 1)
                {
                    indexQuestion++;
                    questionOf = string.Format("{0} of {1}", indexQuestion + 1, QUESTIONLIMIT);
                    Broadcast("MSG:SET_QUESTION", questionOf, indexQuestion);
                    MaxTimeAnswer = factorTimeAnswer;

                    TimerTimeAnswer = AddTimer(TimeAnswer, 1000);
                }


            }

        }

        void GameOver(bool diconnected = false, Player _player = null)
        {
            Player playerWin = null;

            if (_player != null)
            {
                playerWin = Players.FirstOrDefault(x => x.ConnectUserId != _player.ConnectUserId);
            }
            else
            {
                playerWin = Players.OrderByDescending(x => x.PlayerScore).Take(1).FirstOrDefault();
            }

            GameState = GameState.GAMEOVER;

            indexQuestion = Questions.Count;

            Broadcast("MSG:GAMEOVER", playerWin.ConnectUserId, playerWin.PlayerScore, diconnected);

            GameClosed();
        }
    }
}