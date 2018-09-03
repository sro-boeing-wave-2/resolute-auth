using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Entities
{
    class UserCredentials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string passwordHash { get; set; }

        public UserCredentials()
        {
            email = "";
            passwordHash = "";
        }

        public UserCredentials(string email, string passwordHash)
        {
            this.email = email;
            this.passwordHash = passwordHash;
        }
    }
}
