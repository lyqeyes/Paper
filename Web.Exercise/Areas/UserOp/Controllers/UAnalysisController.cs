using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Exercise.Common;
using Webdiyer.WebControls.Mvc;

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

        public ActionResult AnswerDetail(int questionid,int? id)
        {
            var question = db.Questions.FirstOrDefault(a => a.Id == questionid);
            ViewBag.PaperId = question.PaperId;
            ViewBag.Title = question.Title;
            var answers = question.Answers.OrderByDescending(a => a.Id).ToPagedList(id ?? 0, PageSize);
            return View(answers);
        }
    }
}