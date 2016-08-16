using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CSharpJanken.Game2.Players;

namespace CSharpJanken.Game2.Results
{
    /// <summary>
    /// じゃんけんの結果を処理するクラス
    /// </summary>
    public class ResultHistory
    {
        /// <summary>
        /// CSVファイルを保存する相対ディレクトリパス
        /// </summary>
        private static readonly string CsvDirectoryPath = "/Csv/";

        /// <summary>
        /// 保存するCSVファイル名
        /// </summary>
        private static readonly string CsvFileName = "Result.csv";

        /// <summary>
        /// 1連の結果情報を保持するリスト
        /// </summary>
        private List<Result> resultList;

        /// <summary>
        /// 通算の結果情報を保持するディクショナリ。
        /// key:プレイヤー名, value:勝敗サマリ
        /// </summary>
        private Dictionary<string, ResultSummary> resultSummaries;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ResultHistory()
        {
            resultList = new List<Result>();
            resultSummaries = new Dictionary<string, ResultSummary>();
            ReadFromCsv();
        }

        /// <summary>
        /// 結果を追加する
        /// </summary>
        /// <param name="winnerHand">勝利した手</param>
        /// <param name="playerHands">プレイヤーと手のDictionary。key:プレイヤー, value:手</param>
        public void AddResult(Hands winnerHand, Dictionary<Player, Hands> playerHands)
        {
            resultList.Add(new Result(winnerHand, playerHands));
        }

        /// <summary>
        /// CSVファイルに最終の履歴を出力する。
        /// </summary>
        public void WriteToCsv()
        {
            MergeResultToSummary(resultSummaries);
            string currentDir = Directory.GetCurrentDirectory();

            if (!Directory.Exists($"{currentDir}{CsvDirectoryPath}"))
            {
                Directory.CreateDirectory($"{currentDir}{CsvDirectoryPath}");
            }

            if (File.Exists($"{currentDir}{CsvDirectoryPath}{CsvFileName}"))
            {
                File.Delete($"{currentDir}{CsvDirectoryPath}{CsvFileName}");
                File.Create($"{currentDir}{CsvDirectoryPath}{CsvFileName}").Close();
            }

            using (var writer = new StreamWriter($"{currentDir}{CsvDirectoryPath}{CsvFileName}", true, Encoding.GetEncoding("UTF-8")))
            {
                try
                {
                    foreach (ResultSummary summary in resultSummaries.Values)
                    {
                        writer.WriteLine($"{summary.PlayerName},{summary.Wins},{summary.Loses}");
                    }
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw;
                }
            }
        }

        /// <summary>
        /// コンソールに1連の結果と通算結果を出力する。
        /// </summary>
        public void ConsoleOut()
        {
            var thisSummaries = new Dictionary<string, ResultSummary>();
            MergeResultToSummary(thisSummaries);

            foreach (var pair in thisSummaries)
            {
                var wins = (decimal)pair.Value.Wins;
                var loses = (decimal)pair.Value.Loses;
                var rate = decimal.Round(decimal.Divide(decimal.Parse("0.00") + wins, wins + loses), 3, MidpointRounding.AwayFromZero);

                ResultSummary totalSummary = null;
                if (!resultSummaries.TryGetValue(pair.Key, out totalSummary))
                {
                    totalSummary = new ResultSummary(pair.Key, 0L, 0L);
                }

                var totalWins = wins + totalSummary.Wins;
                var totalLoses = loses + totalSummary.Loses;
                var totalRate = decimal.Round(decimal.Divide(decimal.Parse("0.00") + totalWins, totalWins + totalLoses), 3, MidpointRounding.AwayFromZero);

                Console.WriteLine($"{pair.Key}さん: (今回) {wins}勝 {loses}敗 勝率{rate} " +
                    $"(通算) {totalWins}勝 {totalLoses}敗 勝率{totalRate} ");
            }
        }

        /// <summary>
        /// 一連の結果をディクショナリにマージする。
        /// </summary>
        /// <param name="target">マージ先のディクショナリ。key:プレイヤー名, value:勝敗サマリ</param>
        private void MergeResultToSummary(Dictionary<string, ResultSummary> target)
        {
            resultList.ForEach(result =>
            {
                foreach (var pair in result.PlayerHands)
                {
                    ResultSummary summary = null;
                    if (!target.TryGetValue(pair.Key.Name, out summary))
                    {
                        summary = new ResultSummary(pair.Key.Name, 0L, 0L);
                        target[pair.Key.Name] = summary;
                    }

                    if (result.WinnerHand == pair.Value)
                        summary.AddWins();
                    else
                        summary.AddLoses();
                }
            });
        }

        /// <summary>
        /// CSVファイルから最新の履歴を読み込む。
        /// </summary>
        public void ReadFromCsv()
        {
            resultSummaries.Clear();
            string currentDir = System.IO.Directory.GetCurrentDirectory();

            if (!Directory.Exists($"{currentDir}{CsvDirectoryPath}") || !File.Exists($"{currentDir}{CsvDirectoryPath}{CsvFileName}"))
                return;

            using (StreamReader reader = new StreamReader($"{currentDir}{CsvDirectoryPath}{CsvFileName}", Encoding.GetEncoding("UTF-8")))
            {
                while (true)
                {
                    string line = null;
                    try
                    {
                        line = reader.ReadLine();
                    }
                    catch (OutOfMemoryException e)
                    {
                        Console.WriteLine(e.StackTrace);
                        throw;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.StackTrace);
                        throw;
                    }

                    if (line == null)
                        break;

                    var values = line.Split(',');
                    if (values.Length != 3)
                        continue;

                    long winsL = 0L;
                    long losesL = 0L;

                    // If parse error, the user data is broken.
                    if (!long.TryParse(values[1], out winsL) || !long.TryParse(values[2], out losesL))
                        continue;

                    resultSummaries.Add(values[0], new ResultSummary(values[0], winsL, losesL));
                }
            }
        }

        /// <summary>
        /// 1回のじゃんけん結果を結果のリストに追加する
        /// </summary>
        /// <param name="result">結果</param>
        public void AddResult(Result result)
        {
            resultList.Add(result);
        }
    }
}
