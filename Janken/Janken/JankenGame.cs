using System;
using System.Collections.Generic;
using System.Linq;
using CSharpJanken.Game2.Players;
using CSharpJanken.Game2.Results;

namespace CSharpJanken.Game2
{
    /// <summary>
    /// じゃんけんゲームを行うクラス
    /// </summary>
    public class JankenGame
    {
        /// <summary>
        /// じゃんけんゲームをする。
        /// </summary>
        public void Janken()
        {
            ShowStartMessage();
            var players = InitPlayers();
            do
            {
                Game(players);
            } while (NextGame());
            ShowEndMessage();
        }

        /// <summary>
        /// じゃんけんゲームの参加プレイヤー初期化
        /// </summary>
        /// <returns>じゃんけんゲームの参加プレイヤーのリスト</returns>
        private List<Player> InitPlayers()
        {
            var playerNums = InitPlayerNums();
            var userNums = InitUserNums(playerNums);

            return Enumerable.Union(
                Enumerable.Range(0, userNums)
                    .Select(i => (Player)new UserPlayer(InitUserName())),
                Enumerable.Range(0, playerNums - userNums)
                    .Select(i => (Player)new CpuPlayer($"CPU{i + 1}")))
                .ToList();
        }

        /// <summary>
        /// ユーザプレイヤーの名前を初期化
        /// </summary>
        /// <returns>ユーザプレイヤーの名前</returns>
        private string InitUserName()
        {
            return ConfirmInput("ユーザ名を入力してください。", "ユーザ名は{0}でよろしいですか？");
        }

        /// <summary>
        /// ユーザプレイヤーの人数を初期化
        /// </summary>
        /// <param name="playerNums">プレイヤー全体の人数</param>
        /// <returns>ユーザプレイヤーの人数</returns>
        private int InitUserNums(int playerNums)
        {
            var input = ValidateInput(
                $"手入力するユーザは何人ですか？ 0～{playerNums}の整数を入力してください。",
                @"\d",
                $"0～{playerNums}以外の整数が入力されました。");
            var userNums = int.Parse(input);
            if (userNums < 0 || playerNums < userNums)
            {
                Console.WriteLine($"0~{playerNums}の整数が入力されました。再度入力してください。");
                return InitUserNums(playerNums);
            }

            if (!ConfirmInput($"手入力するユーザは{input}人でよろしいですか？"))
            {
                Console.WriteLine("再度入力してください。");
                return InitUserNums(playerNums);
            }

            return userNums;
        }

        /// <summary>
        /// 確認を行う処理
        /// </summary>
        /// <param name="confirmMessage">確認メッセージ</param>
        /// <returns>はいの場合、true。それ以外の場合、false</returns>
        private bool ConfirmInput(string confirmMessage)
        {
            Console.WriteLine(string.Format(confirmMessage));
            Console.WriteLine("y / Y または n / N を入力してください。");

            var inputVal = Console.ReadLine();
            switch (inputVal)
            {
                case "y":
                case "ｙ":
                case "Y":
                case "Ｙ":
                    return true;
                case "n":
                case "ｎ":
                case "N":
                case "Ｎ":
                    return false;
                default:
                    Console.WriteLine("y/Y、n/N以外の文字列が入力されました。");
                    return ConfirmInput(confirmMessage);
            }
        }

        /// <summary>
        /// ユーザにコンソールに入力をさせ、確認メッセージを表示する処理。
        /// 確認メッセージでNoとなった場合には、再度入力を促す。
        /// </summary>
        /// <param name="firstMessage">何を入力してもらうか、その内容についてのメッセージ。</param>
        /// <param name="confirmMessage">確認メッセージ</param>
        /// <returns>ユーザによる入力と確認が済んだ文字列</returns>
        private string ConfirmInput(string firstMessage, string confirmMessage)
        {
            Console.WriteLine(firstMessage);
            var input = Console.ReadLine();

            if (!ConfirmInput(string.Format(confirmMessage, input)))
            {
                input = ConfirmInput(firstMessage, confirmMessage);
            }

            return input;
        }

        /// <summary>
        /// ユーザにコンソールに入力をさせる処理。
        /// 入力された値がregexに正規表現マッチする値でなければ、エラーメッセージを表示させ、再入力させる。
        /// </summary>
        /// <param name="firstMessage">>何を入力してもらうか、その内容についてのメッセージ。</param>
        /// <param name="regex">入力された値が想定された値かどうか判定する正規表現</param>
        /// <param name="errorMessage">入力された値が想定された値でない場合に表示するエラーメッセージ</param>
        /// <returns>ユーザによって入力された値</returns>
        private string ValidateInput(string firstMessage, string regex, string errorMessage)
        {
            Console.WriteLine(firstMessage);
            var input = Console.ReadLine();

            if (!System.Text.RegularExpressions.Regex.IsMatch(input, regex))
            {
                Console.WriteLine(errorMessage);
                return ValidateInput(firstMessage, regex, errorMessage);
            }

            return input;
        }

        /// <summary>
        /// じゃんけんゲームの参加プレイヤーの全体数を入力してもらう処理
        /// </summary>
        /// <returns>じゃんけんゲームの参加プレイヤーの全体数</returns>
        private int InitPlayerNums()
        {
            var input = ValidateInput(
                "何人でプレイしますか？ 2~5の整数を入力してください。",
                @"\d",
                "2~5の整数以外が入力されました。");
            var playerNums = int.Parse(input);

            if (playerNums < 2 || 5 < playerNums)
            {
                Console.WriteLine("2~5の整数以外が入力されました。");
                return InitPlayerNums();
            }

            return playerNums;
        }

        /// <summary>
        /// 終了時のメッセージ
        /// </summary>
        private void ShowEndMessage()
        {
            Console.WriteLine("じゃんけんゲームを終了しました。");
        }

        /// <summary>
        /// じゃんけんゲームを行う処理
        /// </summary>
        /// <param name="players">じゃんけんゲームの参加プレイヤーのリスト</param>
        private void Game(List<Player> players)
        {
            var resultHistory = new ResultHistory();
            Enumerable.Range(1, InitGameCounts()).ToList().ForEach(_ =>
            {
                resultHistory.AddResult(Play(players));
            });

            resultHistory.ConsoleOut();
            resultHistory.WriteToCsv();
        }

        /// <summary>
        /// 連続してじゃんけんする回数を入力してもらう処理
        /// </summary>
        /// <returns>連続してじゃんけんする回数</returns>
        private int InitGameCounts()
        {
            var input = ValidateInput(
                "何回プレイしますか？ 1-99の範囲の整数を入力してください。",
                @"\d{1,2}",
                "整数以外が入力されました。");
            var counts = int.Parse(input);

            if (counts < 1 || 99 < counts)
            {
                Console.WriteLine("1-99の範囲外の値が入力されました。");
                return InitGameCounts();
            }

            return counts;
        }

        /// <summary>
        /// じゃんけん一回を行う処理
        /// </summary>
        /// <param name="players">じゃんけんゲームの参加プレイヤーのリスト</param>
        /// <returns>じゃんけん一回の結果</returns>
        private Result Play(List<Player> players)
        {
            var playerHands = players.ToDictionary(player => player, player => player.Hand());

            foreach (var pair in playerHands)
            {
                Console.WriteLine($"{pair.Key.Name}: {pair.Value.ToString()}");
            }

            var winnerHand = WinnerHand(playerHands);

            if (winnerHand == null)
            {
                Console.WriteLine("あいこです。");
                return Play(players);
            }

            var result = new Result(winnerHand, playerHands);
            result.ConsoleOut();

            return result;
        }

        /// <summary>
        /// 連続したじゃんけんを行った後、再度じゃんけんゲームを行うかどうかの確認処理
        /// </summary>
        /// <returns>再度じゃんけんゲームを行う場合、true。それ以外の場合、false。</returns>
        private bool NextGame()
        {
            return ConfirmInput("続けてじゃんけんしますか？");
        }

        /// <summary>
        /// 勝利した手を判定する処理。
        /// 勝利した手を返す。あいこの場合、nullを返す。
        /// </summary>
        /// <param name="playerHandDictionary">プレイヤーと手のマップ。key:プレイヤー、value:手</param>
        /// <returns>勝利した手。あいこの場合、null。</returns>
        private Hands WinnerHand(Dictionary<Player, Hands> playerHandDictionary)
        {
            bool guExist = playerHandDictionary.ContainsValue(Hands.Gu);
            bool chokiExist = playerHandDictionary.ContainsValue(Hands.Choki);
            bool paExist = playerHandDictionary.ContainsValue(Hands.Pa);

            if (GuWin(guExist, chokiExist, paExist))
                return Hands.Gu;
            else if (ChokiWin(guExist, chokiExist, paExist))
                return Hands.Choki;
            else if (PaWin(guExist, chokiExist, paExist))
                return Hands.Pa;
            else
                return null;
        }

        private bool PaWin(bool guExist, bool chokiExist, bool paExist)
        {
            return guExist && !chokiExist && paExist;
        }

        private bool ChokiWin(bool guExist, bool chokiExist, bool paExist)
        {
            return !guExist && chokiExist && paExist;
        }

        private bool GuWin(bool guExist, bool chokiExist, bool paExist)
        {
            return guExist && chokiExist && !paExist;
        }

        private void ShowStartMessage()
        {
            Console.WriteLine("じゃんけんゲームを開始します。");
        }
    }
}
