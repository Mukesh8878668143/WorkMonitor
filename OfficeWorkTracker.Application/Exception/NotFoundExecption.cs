using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Exception
{
    public class NotFoundExecption: System.Exception
    {
        public NotFoundExecption(string message) : base(message) { }
    }
}
