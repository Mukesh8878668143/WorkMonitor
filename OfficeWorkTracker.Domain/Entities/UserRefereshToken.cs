using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Domain.Entities
{
    public class UserRefereshToken
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public string RefereshToken { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokeDate { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public User user { get; set; } = null;

        public ICollection<UserRefereshToken> RefereshTokens { get; set; } = new List<UserRefereshToken>();

    }
}
