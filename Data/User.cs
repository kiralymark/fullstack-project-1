using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fullstack_project_1.Data
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("user_name")]
        //public string? Email { get; set; }
        public string? UserName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("description_users")]
        public string? DescriptionUsers { get; set; }

        [Column("created_at_users")]
        public string? CreatedAtUsers { get; set; }

    }
}