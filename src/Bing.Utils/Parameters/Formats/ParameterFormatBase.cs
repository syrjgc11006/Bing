﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Parameters.Formats
{
    /// <summary>
    /// 参数格式化器
    /// </summary>
    public abstract class ParameterFormatBase:IParameterFormat
    {
        /// <summary>
        /// 格式化分割符
        /// </summary>
        protected abstract string FormatSeparator { get; }

        /// <summary>
        /// 连接符
        /// </summary>
        protected abstract string JoinSeparator { get; }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public string Format(string key, string value)
        {
            return $"{key}{FormatSeparator}{value}";
        }

        /// <summary>
        /// 连接参数
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <returns></returns>
        public string Join(string left, string right)
        {
            if (string.IsNullOrWhiteSpace(left))
            {
                return right;
            }

            if (string.IsNullOrWhiteSpace(right))
            {
                return left;
            }

            return $"{left}{JoinSeparator}{right}";
        }
    }
}
