using System;

namespace CSharpJanken.Game2.Players
{
    /// <summary>
    /// This class represents player.
    /// </summary>
    public abstract class Player
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">name; identify user</param>
        /// <param name="type">player type; cpu / user</param>
        public Player(string name, PlayerType type)
        {
            Name = name;
            PlayerType = type;
        }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// プレイヤー種別
        /// </summary>
        public PlayerType PlayerType { get; private set; }

        /// <summary>
        /// 手を出す処理
        /// </summary>
        /// <returns>手</returns>
        public abstract Hands Hand();

        /// <summary>
        /// 手の文字列表現を手に変換する。
        /// 変換後の手を返却する。変換できない場合は、nullを返却する。
        /// </summary>
        /// <param name="input">手の文字列表現</param>
        /// <returns>変換後の手。変換できない場合は、null。</returns>
        protected Hands JudgeHand(string input)
        {
            switch (input)
            {
                case "1":
                case "１":
                    return Hands.Gu;
                case "2":
                case "２":
                    return Hands.Choki;
                case "3":
                case "３":
                    return Hands.Pa;
                default:
                    return null;
            }
        }

        /// <summary>
        /// プレイヤー種別がユーザかどうかを返す
        /// </summary>
        /// <returns>プレイヤー種別がユーザの場合、true。それ以外の場合、false。</returns>
        public bool IsUser()
        {
            return PlayerType == PlayerType.User;
        }

        /// <summary>
        /// 手の数値表現を手に変換する。
        /// 変換後の手を返却する。変換できない場合は、nullを返却する。
        /// </summary>
        /// <param name="input">手の数値表現</param>
        /// <returns>変換後の手。変換できない場合は、null。</returns>
        protected Hands JudgeHand(int input)
        {
            switch (input)
            {
                case 1:
                    return Hands.Gu;
                case 2:
                    return Hands.Choki;
                case 3:
                    return Hands.Pa;
                default:
                    return null;
            }
        }
    }
}
