﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Contexts
{
    public class UserLoginInfo
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public int Role { get; set; }
    }
}
