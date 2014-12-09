using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Contexts
{
    public interface IAuthCookie
    {
         int UserExpiresHours { get; set; }
         int UserId { get; set; }
         string UserName { get; set; }
         string Email { get; set; }
         int Role { get; set; }
    }
}
