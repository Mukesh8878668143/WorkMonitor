using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Enum
{
    public enum TaskStatus
    {
        pending =1,
        InProgress =2,
        Completed =3,
        Hold =4,
        Blocked=5
    }
}
