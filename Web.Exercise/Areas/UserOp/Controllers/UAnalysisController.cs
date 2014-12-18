using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Exercise.Common;

namespace Web.Exercise.Areas.UserOp.Controllers
{
    /// <summary>
    /// 执行对问卷调查结果的分析操作
    /// </summary>
    public class UAnalysisController : UserBaseController
    {
        // GET: UserOp/UAnalysis
        public ActionResult Index(int paperid)
        {
            var paper = db.TestPapers.FirstOrDefault(a => a.Id == paperid);
            return View(paper);
        }

        public ActionResult AnswerDetail(int questionid)
        {
            var question = db.Questions.FirstOrDefault(a => a.Id == questionid);
            return View(question);
        }
    }
}