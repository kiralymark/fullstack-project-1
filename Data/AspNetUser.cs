using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fullstack_project_1.Data
{
    [Table("AspNetUsers")]
    public class AspNetUser
    {

        [Key]
        [Column("Id", TypeName = "VARCHAR(450)")]
        public string Id { get; set; } = null!;

        [Column("AccessFailedCount")]
        public int AccessFailedCount { get; set; }

        [Column("ConcurrencyStamp")]
        public string? ConcurrencyStamp { get; set; }

        [Column("Email", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public string? Email { get; set; }

        [Column("EmailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [Column("LockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [Column("LockoutEnd")]
        public DateTimeOffset? LockoutEnd { get; set; }

        [Column("NormalizedEmail", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public string? NormalizedEmail { get; set; }

        [Column("NormalizedUserName", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public string? NormalizedUserName { get; set; }

        [Column("PasswordHash")]
        public string? PasswordHash { get; set; }

        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Column("PhoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [Column("SecurityStamp")]
        public string? SecurityStamp { get; set; }

        [Column("TwoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }

        [Column("UserName", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public string? UserName { get; set; }

    }
    
}
