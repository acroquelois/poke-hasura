using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type
{
    public class QuestionAnswerType: ObjectGraphType<QuestionAnswer>
    {
        public QuestionAnswerType()
        {
            Field(x => x.Id);
            Field(x => x.Libelle);
        }
    }
}