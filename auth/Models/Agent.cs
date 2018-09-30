using System;
using System.Collections.Generic;
using System.Text;

namespace auth.Models
{
    public class Agent
    {
        public int agentId;
        public string name;
        public string email;
        public string Phone_no;
        public string profileImageUrl;
        public DateTime CreatedOn;
        public long CreatedBy;
        public DateTime UpdatedOn;
        public long UpdatedBy;
    }
}
