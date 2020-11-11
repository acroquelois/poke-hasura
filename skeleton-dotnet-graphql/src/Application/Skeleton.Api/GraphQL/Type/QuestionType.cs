using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type
{
    public class QuestionType: ObjectGraphType<Question>
    {
        public QuestionType()
        {
            Name = "Question";
            Field(x => x.Id,type:typeof(IdGraphType));
            Field(x => x.Libelle);
            Field<QuestionCategorieType>(nameof(Question.QuestionCategorie));
            Field<QuestionAnswerType>(nameof(Question.QuestionAnswer));
            Field<ListGraphType<QuestionPropositionType>>(nameof(Question.ListQuestionProposition));
        }
    }
}