using System;
using System.Collections.Generic;
using System.Text;

namespace auth.Models
{
    public class Agent
    {
        int id;
        string name;
        string email;
        string phone_no;
        string profile_img_url;
        DateTime createdOn;
        long createdBy;
        DateTime updatedOn;
        long updatedBy;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Phone_no { get => phone_no; set => phone_no = value; }
        public string Profile_img_url { get => profile_img_url; set => profile_img_url = value; }
        public DateTime CreatedOn { get => createdOn; set => createdOn = value; }
        public long CreatedBy { get => createdBy; set => createdBy = value; }
        public DateTime UpdatedOn { get => updatedOn; set => updatedOn = value; }
        public long UpdatedBy { get => updatedBy; set => updatedBy = value; }
    }
}
