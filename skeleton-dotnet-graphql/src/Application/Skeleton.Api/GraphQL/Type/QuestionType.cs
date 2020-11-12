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
            Field(x => x.QuestionCategorie, type: typeof(QuestionCategorieType));
            Field(x => x.QuestionAnswer, type: typeof(QuestionAnswerType));
            Field(x => x.ListQuestionProposition, type: typeof(ListGraphType<QuestionPropositionType>));
        }
    }
}