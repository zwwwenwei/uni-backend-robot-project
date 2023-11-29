using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using System.Xml.Linq;

namespace sit331_4._2
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string? Description { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public UserModel(int id, string email, string firstname, string lastname, string passwordhash, DateTime createddate, DateTime modifieddate, string? description = null, string? role = null) 
        {
            Id = id;
            Email = email;
            FirstName = firstname;
            LastName = lastname;
            PasswordHash = passwordhash;
            Description = description;
            Role = role;
            CreatedDate = createddate;
            ModifiedDate = modifieddate;
        }
    }
}
