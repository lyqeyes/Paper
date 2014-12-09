using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Database
{
    public class DataSourceContext
    {
        /// <summary>
        /// 当前使用的数据上下文
        /// </summary>
        public static QuestionnaireDBEntities Current
        {
            get {
                return new QuestionnaireDBEntities();
            }
        }
    }
}
