using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Exercise.Areas.UserOp.Models
{
    public class MyJsonResult
    {
        public string Msg { get; set; }
        public int IsOk { get; set; }
        public string Result { get; set; }
    }

    public class SerializeResult
    {
        public static string SerializeStringResult(int isOk, string msg)
        {
            string res= JsonConvert.SerializeObject(new MyJsonResult()
            {
                IsOk = isOk,
                Msg = msg
            });
            return res;
        }
        public static string SerializeStringResult(int isOk, string msg,string result)
        {
            string res = JsonConvert.SerializeObject(new MyJsonResult()
            {
                IsOk = isOk,
                Msg = msg,
                Result = result
            });
            return res;
        }
    }
}