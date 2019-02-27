using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumniBook.BLL.SensitiveService
{
    interface ISensitiveServiceAplication
    {
        bool Check(string Msg);
    }
}
