using System;

namespace CSharpJanken.Game2.Players
{
    /// <summary>
    /// ユーザプレイヤー
    /// </summary>
    public class UserPlayer : Player
    {
        /// <summary>
        /// <see cref="UserPlayer"/>のコンストラクタ
        /// </summary>
        /// <param name="name">ユーザ名</param>
        public UserPlayer(string name) : base(name, PlayerType.User)
        {
        }

        /// <summary>
        /// <see cref="Player.Hand"/>
        /// </summary>
        /// <returns>手</returns>
        public override Hands Hand()
        {
            ShowHandInputMessage(Name);
            var inputVal = Console.ReadLine();
            var hand = JudgeHand(inputVal);

            if (hand == null)
            {
                Console.WriteLine("1～3の整数以外が入力されました。");
                hand = Hand();
            }

            return hand;
        }

        private void ShowHandInputMessage(string name)
        {
            System.Console.WriteLine($"{name}さん、手を選んでください。手は1～3の整数を入力してください。");
            System.Console.WriteLine("1:グー 2:チョキ 3:パー");
        }
    }
}
