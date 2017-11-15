using System;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        private int _player1Score;
        private int _player2Score;

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
            {
                _player1Score += 1;
            }
            else
            {
                _player2Score += 1;
            }
        }

        public string GetScore()
        {
            var scoreState = GetScoreState();
            switch (scoreState)
            {
                case ScoreState.InGame:
                    return ResolveInGameScore();
                case ScoreState.Drawing:
                    return ResolveDrawingScore();
                case ScoreState.Advantage:
                    return ResolveAdvantage();
                default:
                    throw new Exception($"The score is in an invalid state: player 1 = '{_player1Score}'; player 2 = '{_player2Score}'");
            }
        }

        private ScoreState GetScoreState()
        {
            if (_player1Score == _player2Score)
            {
                return ScoreState.Drawing;
            }
            if (_player1Score >= 4 || _player2Score >= 4)
            {
                return ScoreState.Advantage;
            }
            return ScoreState.InGame;
        }

        private string ResolveInGameScore()
        {
            var score = string.Empty;
            for (var i = 1; i < 3; i++)
            {
                int tempScore;
                if (i == 1)
                {
                    tempScore = _player1Score;
                }
                else
                {
                    score += "-";
                    tempScore = _player2Score;
                }
                switch (tempScore)
                {
                    case 0:
                        score += "Love";
                        break;
                    case 1:
                        score += "Fifteen";
                        break;
                    case 2:
                        score += "Thirty";
                        break;
                    case 3:
                        score += "Forty";
                        break;
                }
            }
            return score;
        }

        private string ResolveAdvantage()
        {
            var minusResult = _player1Score - _player2Score;
            if (minusResult == 1)
            {
                return "Advantage player1";
            }
            if (minusResult == -1)
            {
                return "Advantage player2";
            }
            if (minusResult >= 2)
            {
                return "Win for player1";
            }
            return "Win for player2";
        }

        private string ResolveDrawingScore()
        {
            var score = string.Empty;
            switch (_player1Score)
            {
                case 0:
                    score = "Love-All";
                    break;
                case 1:
                    score = "Fifteen-All";
                    break;
                case 2:
                    score = "Thirty-All";
                    break;
                default:
                    score = "Deuce";
                    break;
            }
            return score;
        }
    }
}

