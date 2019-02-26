using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniBook.BLL
{
    public class ResultBaseOutput
    {
        public bool Status { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }
    }
}