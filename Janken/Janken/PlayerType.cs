using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpJanken.Game2.Players
{
    /// <summary>
    /// プレイヤー種別
    /// </summary>
    public sealed class PlayerType
    {
        /// <summary>
        /// ユーザプレイヤー種別
        /// </summary>
        public static readonly PlayerType User = new PlayerType("ユーザ");

        /// <summary>
        /// CPUプレイヤー種別
        /// </summary>
        public static readonly PlayerType Cpu = new PlayerType("CPU");

        /// <summary>
        /// プレイヤー種別の文字列表現
        /// </summary>
        private readonly string str;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="str">プレイヤー種別の文字列表現</param>
        private PlayerType(string str)
        {
            this.str = str;
        }

        /// <summary>
        /// 文字列表現を返す。
        /// </summary>
        /// <returns>文字列表現</returns>
        public override string ToString()
        {
            return str;
        }
    }
}
