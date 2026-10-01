using System;
using fullstack_project_1.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullstack_project_1.Services
{
    public class SubjectsService
    {
        private AppDbContext _context;

        public SubjectsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Subject> GetAllSubjects() => _context.Subjects.ToList();

        public SubjectVM GetSubjectById(int subjectId)       // public SubjectVM GetSubjectById(int subjectId) 
        {
            var _subject = _context.Subjects.Where(n => n.id_subjects == subjectId).Select(subject => new SubjectVM()
            {
                subject_code = subject.subject_code,
                subject_name = subject.subject_name,
                description_subjects = subject.description_subjects,
                credit = subject.credit,
                department = subject.department,
                is_archived = subject.is_archived
            }).FirstOrDefault();

            return _subject;


            //var _subject = _context.Subjects.FirstOrDefault(n => n.id_subjects == subjectId);

            //var _subject = _context.Subjects.Select(subject => new SubjectVM()
            //{
            /*if(_subject != null)
            {
                subject_code = subject.subject_code,
                subject_name = subject.subject_name,
                description_subjects = subject.description_subjects,
                credit = subject.credit,
                department = subject.department,
                is_archived = subject.is_archived

                _context.SaveChanges();
            }*/
            //}).FirstOrDefault();

            //return _subject;
        }

    }

}
