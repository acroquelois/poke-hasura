using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type.InputType
{
    public class QuestionInputType: InputObjectGraphType<Question>
    {
        public QuestionInputType()
        {
            Field(x => x.Id,type:typeof(IdGraphType));
            Field(x => x.Libelle);
            Field<QuestionCategorieInputType>(nameof(Question.QuestionCategorie));
            Field<QuestionAnswerInputType>(nameof(Question.QuestionAnswer));
            Field<ListGraphType<QuestionPropositionInputType>>(nameof(Question.ListQuestionProposition));
        }
    }
}