using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type.InputType
{
    public class QuestionInputType: InputObjectGraphType<Question>
    {
        public QuestionInputType()
        {
            Name = "QuestionInput";
            Field(x => x.Libelle);
            Field(x => x.QuestionCategorieId);
            Field<QuestionCategorieInputType>(nameof(Question.QuestionCategorie));
            Field<QuestionAnswerInputType>(nameof(Question.QuestionAnswer));
            Field<ListGraphType<QuestionPropositionInputType>>(nameof(Question.ListQuestionProposition));
        }
    }
}