using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phd.Models
{
    public class BRStudent
    {
        // собственные данные
        public int Id { get; set; }
        public string Iin { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string ThesisTopicRus { get; set; }
        public string ThesisTopicKaz { get; set; }
        public string ThesisTopicEng { get; set; }

        // реквизиты научного руководителя ResearchSupervisor 
        public string ResearchSupervisorFname { get; set; }
        public string ResearchSupervisorMname { get; set; }
        public string ResearchSupervisorLname { get; set; }
        public string ResearchSupervisorWorkPlace { get; set; }
        public string ResearchSupervisorPosition { get; set; }
        // ***

        // реквизиты рецензента Reviewer
        public string ReviewerFname { get; set; }
        public string ReviewerMname { get; set; }
        public string ReviewerLname { get; set; }
        public string ReviewerWorkPlace { get; set; }
        public string ReviewerPosition { get; set; }
        public int ReviewerGrade { get; set; }
        // ***

        // реквизиты консультанта Consultant
        public string ConsultantFname { get; set; }
        public string ConsultantMname { get; set; }
        public string ConsultantLname { get; set; }
        public string ConsultantWorkPlace { get; set; }
        public string ConsultantPosition { get; set; }
        // ***

        /// Привязанность к одной группе
        public int BRStudentGroupId { get; set; }
        public BRStudentGroup BRStudentGroup { get; }
        // ***

        // Все связи один ко многим
        public ICollection<BRStudentGrade> BRStudentGrade { get; set; }
        public ICollection<BRStudentDoc> BRStudentDoc { get; set; }
        // ***
    }
}
