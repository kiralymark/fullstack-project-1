using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fullstack_project_1.Data
{
    [Table("Subjects")]
    public class Subject
    {

        [Key]
        [Column("id_subjects")]
        public int id_subjects { get; set; }

        [Column("subject_code", TypeName = "VARCHAR(30)")]
        [StringLength(30)]
        public string subject_code { get; set; }

        [Column("subject_name", TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string subject_name { get; set; }

        [Column("description_subjects")]
        // TypeName = text
        public string? description_subjects { get; set; }

        [Column("credit")]
        public int? credit { get; set; }

        [Column("department", TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string? department { get; set; }

        [Column("is_archived")]
        public bool? is_archived { get; set; }

    }
}
