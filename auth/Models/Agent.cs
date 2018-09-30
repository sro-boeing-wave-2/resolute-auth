using System;
using System.Collections.Generic;
using System.Text;

namespace auth.Models
{
    public class Agent
    {
        public int Id;
        public string Name;
        public string Email;
        public string Phone_no;
        public string Profile_img_url;
        public DateTime CreatedOn;
        public long CreatedBy;
        public DateTime UpdatedOn;
        public long UpdatedBy;
    }
}
