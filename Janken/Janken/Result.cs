using System;
using System.Collections.Generic;
using System.Linq;
using CSharpJanken.Game2.Players;

namespace CSharpJanken.Game2.Results
{
    /// <summary>
    /// じゃんけんの決着のついた1回分の結果情報を保持する。
    /// </summary>
    public class Result
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="winnerHand">winned hand</param>
        /// <param name="playerHands">hands of each player (key:player, value:hands)</param>
        public Result(Hands winnerHand, Dictionary<Player, Hands> playerHands)
        {
            WinnerHand = winnerHand;
            PlayerHands = playerHands;
            DateTime = DateTime.Now;
        }

        /// <summary>
        /// 結果データを作成した日時情報
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// winned hand
        /// </summary>
        public Hands WinnerHand { get; private set; }

        /// <summary>
        /// hands of each player (key:player, value:hands)
        /// </summary>
        public Dictionary<Player, Hands> PlayerHands { get; private set; }

        /// <summary>
        /// コンソールに結果情報を出力する。
        /// </summary>
        public void ConsoleOut()
        {
            PlayerHands.Where(pair => pair.Key.IsUser()).ToList().ForEach(pair =>
            {
                string resultStr = pair.Value == WinnerHand ? "勝ち" : "負け";
                Console.WriteLine($"{pair.Key.Name}さんの{resultStr}です。");
            });
        }
    }
}
