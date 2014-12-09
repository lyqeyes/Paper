using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Contexts
{
    public class QuestionnaireUserContext:UserContext
    {
        public QuestionnaireUserContext() 
            : base(QuestionnaireCookieContext.Current) 
        { }

        public QuestionnaireUserContext(IAuthCookie authCookie)
            : base(authCookie)
        { }

        public static QuestionnaireUserContext Current
        {
            get 
            {
                return new QuestionnaireUserContext();
            }
        }
    }
}
