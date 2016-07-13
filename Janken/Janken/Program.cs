namespace CSharpJanken.Game2
{
    /// <summary>
    /// プログラムのエントリーポイント
    /// </summary>
    public class Program
    {
        /// <summary>
        /// エントリーポイント
        /// </summary>
        /// <param name="args">プログラム引数</param>
        public static void Main(string[] args)
        {
            var game = new JankenGame();
            game.Janken();
        }
    }
}
