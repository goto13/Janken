namespace CSharpJanken.Game2
{
    /// <summary>
    /// 手を表現するクラス
    /// </summary>
    public sealed class Hands
    {
        /// <summary>
        /// グー
        /// </summary>
        public static readonly Hands Gu = new Hands("グー");

        /// <summary>
        /// チョキ
        /// </summary>
        public static readonly Hands Choki = new Hands("チョキ");

        /// <summary>
        /// パー
        /// </summary>
        public static readonly Hands Pa = new Hands("パー");

        /// <summary>
        /// 手の文字列表現
        /// </summary>
        private readonly string str;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="str">手の文字列表現</param>
        private Hands(string str)
        {
            this.str = str;
        }

        /// <summary>
        /// 文字列表現を返す
        /// </summary>
        /// <returns>文字列表現</returns>
        public override string ToString()
        {
            return str;
        }
    }
}
