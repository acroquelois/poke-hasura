using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type.InputType
{
    public class QuestionAnswerInputType: InputObjectGraphType<QuestionAnswer>
    {
        public QuestionAnswerInputType()
        {
            Name = "QuestionAnswerInput";
            Field(x => x.Id);
            Field(x => x.Libelle);
        }
    }
}