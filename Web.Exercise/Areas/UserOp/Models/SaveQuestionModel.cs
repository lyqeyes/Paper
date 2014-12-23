using Modules.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Exercise.Areas.UserOp.Models
{
    public class SaveQuestionModel
    {
        public string Title { get; set; }
        public EnumQuestionType Questiontype { get; set; }
        public int PaperId { get; set; }
        /// <summary>
        /// 该问题的所有选项(问答题也会有该参数)
        /// </summary>
        public string[] Items { get; set; }
        public string[] MustAnswers { get; set; }

        public string WhetherContinue { get; set; }
    }
}