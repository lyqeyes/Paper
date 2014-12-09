using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Contexts
{
    public class QuestionnaireCookieContext:CookieContext
    {
        public static QuestionnaireCookieContext Current 
        {
            get {
                return new QuestionnaireCookieContext();
            }
        }

        public override string KeyPrefix
        {
            get
            {
                return "Questionnaire" + base.KeyPrefix;
            }
        }
    }
}
