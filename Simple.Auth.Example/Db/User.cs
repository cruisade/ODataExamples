using System.ComponentModel.DataAnnotations.Schema;

namespace Simple.Auth.Example.Db
{
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        // [IgnoreDataMember] // тот самый аттрибут который запрещает OData обращаться к полю   
        [Column("password")]
        public string Password { get; set; }
    }
}