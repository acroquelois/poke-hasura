using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Skeleton.Domain.Models.Core;

namespace Skeleton.Domain.Models
{
    public class Question: BaseEntity<int>
    {
        public string Libelle { get; set; }
        
        [ForeignKey("QuestionCategorieId")]
        public int QuestionCategorieId{ get; set; }
        
        public QuestionCategorie QuestionCategorie { get; set; }
        
        [ForeignKey("QuestionAnswerId")]
        public int QuestionAnswerId { get; set; }

        public QuestionAnswer QuestionAnswer { get; set; }
        
        public virtual List<QuestionProposition> ListQuestionProposition { get; set; }
    }
}