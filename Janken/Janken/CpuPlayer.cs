namespace CSharpJanken.Game2.Players
{
    /// <summary>
    /// CPUプレイヤー
    /// </summary>
    public class CpuPlayer : Player
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">CPU名</param>
        public CpuPlayer(string name) : base(name, PlayerType.Cpu)
        {
        }

        /// <summary>
        /// <see cref="Player.Hand"/>
        /// </summary>
        /// <returns>手</returns>
        public override Hands Hand()
        {
            int hand = Utils.Random.Next(1, 4);
            return JudgeHand(hand);
        }
    }
}
