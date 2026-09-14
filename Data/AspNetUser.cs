using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fullstack_project_1.Data
{
    [Table("AspNetUsers")]
    public class AspNetUser : IdentityUser
    {

        [Key]
        [Column("Id", TypeName = "VARCHAR(450)")]
        public override string Id { get; set; } = null!;

        [Column("AccessFailedCount")]
        public override int AccessFailedCount { get; set; }

        [Column("ConcurrencyStamp")]
        public override string? ConcurrencyStamp { get; set; }

        [Column("Email", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public override string? Email { get; set; }

        [Column("EmailConfirmed")]
        public override bool EmailConfirmed { get; set; }

        [Column("LockoutEnabled")]
        public override bool LockoutEnabled { get; set; }

        [Column("LockoutEnd")]
        public override DateTimeOffset? LockoutEnd { get; set; }

        [Column("NormalizedEmail", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public override string? NormalizedEmail { get; set; }

        [Column("NormalizedUserName", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public override string? NormalizedUserName { get; set; }

        [Column("PasswordHash")]
        public override string? PasswordHash { get; set; }

        [Column("PhoneNumber")]
        public override string? PhoneNumber { get; set; }

        [Column("PhoneNumberConfirmed")]
        public override bool PhoneNumberConfirmed { get; set; }

        [Column("SecurityStamp")]
        public override string? SecurityStamp { get; set; }

        [Column("TwoFactorEnabled")]
        public override bool TwoFactorEnabled { get; set; }

        [Column("UserName", TypeName = "VARCHAR(256)")]
        [StringLength(256)]
        public override string? UserName { get; set; }

    }
    
}
