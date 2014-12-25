using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Exercise.Common;
using Modules.Enums;
using Modules.Database;
using Modules.Helpers;
namespace Web.Exercise.Areas.UserOp.Controllers
{
    /// <summary>
    /// 此控制器主要负责问卷发布后,  对问卷的在线回答功能
    /// </summary>
    
    public class UAnswerController : UserBaseController
    {

        // GET: UserOp/UAnswer
        [AuthorizeIgnore]
        public ActionResult Paper(int id)
        {
            var paper = db.TestPapers.FirstOrDefault(a => a.Id == id);
            //1. 判断问卷是否存在以及是否发布
            if (paper == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            else if (paper.IsEnd == (int)EnumTestState.IsEnd)
            {
                return Content("问卷暂时已停止");
            }
            else
            {
                return View(paper);
            }
        }
         
        [AuthorizeIgnore]
        /// <summary>
        /// 接收问卷回答
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceivePaper()
        {
            string ip = IPHelper.GetIP();
            var paperid = Int32.Parse(Request.Form["paperid"]);
            var paper = db.TestPapers.FirstOrDefault(a => a.Id == paperid);

            //回答数+1
            paper.DoCount += 1;
            db.Entry(paper).State = System.Data.Entity.EntityState.Modified;

            //记录每题答案
            foreach (var item in paper.Questions)
            {
                //表单数据的name值用id标志
                var name_id = item.Id.ToString();
                //获取该题的答案
                var answer = Request.Form[name_id];
                //不同类型的题目不同处理
                if (item.QuestionType == (int)EnumQuestionType.Radioselect)
                {
                    db.Choices.Add(new Choice { 
                        SelectItemId = Int32.Parse(answer)
                    });
                }
                else if (item.QuestionType == (int)EnumQuestionType.Multiselect)
                {
                    var selectlist = answer.Split(',');
                    foreach (var selectitem in selectlist)
                    {
                        db.Choices.Add(new Choice
                        {
                            SelectItemId = Int32.Parse(selectitem)
                        });
                    }
                }
                else {
                    if (answer == "")
                        answer = "(空)";
                    //string address = IPHelper.GetAddress(IPHelper.GetIP());
                    db.Answers.Add(new Answer { 
                        QuestionId = Int32.Parse(name_id),
                        AnswerContent = answer,
                        Address = "无"
                    });
                }
            }
            db.SaveChanges();
            return View();
        }
    }
}