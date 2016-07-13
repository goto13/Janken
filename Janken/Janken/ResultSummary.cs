namespace CSharpJanken.Game2.Results
{
    /// <summary>
    /// 1回のじゃんけんの結果情報を保持するクラス
    /// </summary>
    public class ResultSummary
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="playerName">name of player</param>
        /// <param name="wins">counts of win</param>
        /// <param name="loses">counts of lose</param>
        public ResultSummary(string playerName, long wins, long loses)
        {
            PlayerName = playerName;
            Wins = wins;
            Loses = loses;
        }

        /// <summary>
        /// プレイヤー名
        /// </summary>
        public string PlayerName { get; private set; }

        /// <summary>
        /// 勝利数
        /// </summary>
        public long Wins { get; private set; }

        /// <summary>
        /// 敗北数
        /// </summary>
        public long Loses { get; private set; }

        /// <summary>
        /// 勝利数に追加。
        /// </summary>
        /// <param name="wins">勝利数。デフォルトは1</param>
        public void AddWins(long wins = 1)
        {
            Wins += wins;
        }

        /// <summary>
        /// 敗北数に追加。
        /// </summary>
        /// <param name="loses">敗北数。デフォルトは1</param>
        public void AddLoses(long loses = 1)
        {
            Loses += loses;
        }
    }
}
