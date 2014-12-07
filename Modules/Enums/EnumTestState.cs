using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Enums
{
    /// <summary>
    /// 问卷是否被终止的状态枚举
    /// </summary>
    public enum EnumTestState
    {
        /// <summary>
        /// 正在进行
        /// </summary>
        IsNotEnd = 0,

        /// <summary>
        /// 已停止
        /// </summary>
        IsEnd = 1
    }
}
