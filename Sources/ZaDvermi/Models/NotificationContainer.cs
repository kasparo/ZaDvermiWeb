

using System.Collections.Generic;

namespace ZaDvermi.Models
{
    public class NotificationContainer
    {
        public List<User> Celebrators { get; set; }
        public bool IsNewBookPost { get; set; }
    }
}