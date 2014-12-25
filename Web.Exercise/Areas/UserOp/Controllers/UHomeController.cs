using Modules.Database;
using Modules.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Exercise.Areas.UserOp.Models;
using Web.Exercise.Common;
using Webdiyer.WebControls.Mvc;
namespace Web.Exercise.Areas.UserOp.Controllers
{
    public class UHomeController : UserBaseController
    {
        // GET: UserOp/Home


        #region 首页
        /// <summary>
        /// 登录后的用户主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            //读取所有的问卷
            var paperlist = db.TestPapers.Where(a => a.UserId == UserContext.UserInfo.Id).OrderByDescending(a => a.CreateTime);
            var list = paperlist.ToPagedList(id ?? 0, PageSize);
            return View(list);
        } 
        #endregion

        #region 新建问卷
        /// <summary>
        /// 创建新的问卷
        /// </summary>
        /// <returns></returns>
        public ActionResult CreatePaper()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CratePaper(TestPaper paper)
        {
            if (paper.Description == "")
                paper.Description = "无";
            paper.IsEnd = (int)EnumTestState.IsEnd;
            paper.UserId = UserContext.UserInfo.Id;
            paper.CreateTime = DateTime.Now;
            paper.DoCount = 0;
            db.TestPapers.Add(paper);
            db.SaveChanges();
            return RedirectToAction("AddQuestions", new { paperid = paper.Id });
        } 
        #endregion

        #region 添加&删除问题
        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="paperid"></param>
        /// <returns></returns>
        public ActionResult AddQuestions(int paperid)
        {
            var questionlist = db.Questions.Where(a => a.PaperId == paperid);
            ViewBag.Paperid = paperid;
            return View(questionlist);
        }
        [HttpPost]
        public ActionResult SaveQuestion(SaveQuestionModel question)
        {
            question.MustAnswers = Request.Form["mustanswer"].Split(',');
            question.Items = Request.Form["selectitems"].Split(',');
            var newquestion = new Question();
            newquestion.Title = question.Title;
            newquestion.QuestionType = (int)question.Questiontype;
            newquestion.MustAnswer = question.MustAnswers.Length > 1 ? (int)EnumMustAnswer.MustAnswer : (int)EnumMustAnswer.Selectale;
            newquestion.PaperId = question.PaperId;
            db.Questions.Add(newquestion);
            db.SaveChanges();
            //关于选项的处理
            if (question.Questiontype != EnumQuestionType.Question)
            {
                foreach (var item in question.Items)
                {
                    db.SelectItems.Add(new SelectItem
                    {
                        ItemContent = item,
                        QuestionId = newquestion.Id
                    });
                }
                db.SaveChanges();
            }
            if (question.WhetherContinue == "over")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddQuestions", new { paperid = question.PaperId });
            }
        }
        public string DelQuestion(int questionid)
        {
            var question = db.Questions.FirstOrDefault(a => a.Id == questionid);
            if (question != null)
            {
                if (question.QuestionType == (int)EnumQuestionType.Question)
                {
                    db.Answers.RemoveRange(question.Answers);
                    db.Questions.Remove(question);
                    db.SaveChanges();
                }
                else
                {
                    foreach (var item in question.SelectItems)
                    {
                        db.Choices.RemoveRange(item.Choices);
                    }
                    db.SelectItems.RemoveRange(question.SelectItems);
                    db.Questions.Remove(question);
                    db.SaveChanges();
                }
                return SerializeResult.SerializeStringResult(1, "删除成功");
            }
            else
            {
                return SerializeResult.SerializeStringResult(0, "删除失败, 找不到对应问题");
            }
        }

        #endregion

        #region 对问卷的系列操作 -- 启动/停止  删除
        public string ChangeState(int paperid)
        {
            var paper = db.TestPapers.FirstOrDefault(a => a.Id == paperid);
            if (paper.IsEnd == (int)EnumTestState.IsEnd)
            {
                paper.IsEnd = (int)EnumTestState.IsNotEnd;
            }
            else {
                paper.IsEnd = (int)EnumTestState.IsEnd;
            }
            try
            {
                db.Entry(paper).State = System.Data.Entity.EntityState.Modified;
                db.SaveChangesAsync();
                return SerializeResult.SerializeStringResult(1, "操作成功");
            }
            catch (Exception ex)
            {
                return SerializeResult.SerializeStringResult(0, "操作失败, 请刷新重试");
            }
        }
        public string DeletePaper(int paperid)
        {
            var paper = db.TestPapers.FirstOrDefault(a => a.Id == paperid);
            if (paper != null)
            {
                foreach (var item in paper.Questions)
                {
                    if (item.QuestionType == (int)EnumQuestionType.Question)
                    {
                        db.Answers.RemoveRange(item.Answers);
                    }
                    else
                    {
                        foreach (var item_2 in item.SelectItems)
                        {
                            db.Choices.RemoveRange(item_2.Choices);
                        }
                        db.SelectItems.RemoveRange(item.SelectItems);
                    }
                }
                db.Questions.RemoveRange(paper.Questions);
                db.TestPapers.Remove(paper);
                db.SaveChanges();
                return SerializeResult.SerializeStringResult(1,"删除成功");
            }
            else {
                return SerializeResult.SerializeStringResult(0, "删除失败, 找不到对应问卷");
            }
            
        } 
        #endregion
    }
}