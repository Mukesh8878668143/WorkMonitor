using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Enum
{
    public enum WorkTaskStatus
    {
        [EnumMember(Value ="Pending")]
        Pending =1,

        [EnumMember(Value = "In Progress")]
        InProgress =2,

        [EnumMember(Value = "Completed")]
        Completed =3,

        [EnumMember(Value = "On Hold")]
        Hold =4,

        [EnumMember(Value = "Blocked")]
        Blocked =5
    }
}
