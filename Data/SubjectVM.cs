using System.ComponentModel.DataAnnotations;

namespace fullstack_project_1.Data
{
    public class SubjectVM
    {
        // a custom view model

        public string subject_code { get; set; }

        public string subject_name { get; set; }

        public string? description_subjects { get; set; }

        public int? credit { get; set; }

        public string? department { get; set; }

        public bool? is_archived { get; set; }

    }
}
