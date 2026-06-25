using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Enum
{
    public enum ActivityType
    {
        TaskCreated =1,
        StatusChanged = 2,
        CommentAdded = 3,
        TaskAssigned = 4
    }
}
