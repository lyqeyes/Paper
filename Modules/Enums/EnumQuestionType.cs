using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Enums
{
    /// <summary>
    /// 问卷题目类型枚举
    /// </summary>
    public enum EnumQuestionType
    {
        /// <summary>
        /// 单选题
        /// </summary>
        Radioselect = 0,
        /// <summary>
        /// 多选题
        /// </summary>
        Multiselect = 1,
        /// <summary>
        /// 简答题
        /// </summary>
        Question = 2
    }
}
