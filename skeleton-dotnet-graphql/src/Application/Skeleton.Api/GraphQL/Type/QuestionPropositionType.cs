using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type
{
    public class QuestionPropositionType: ObjectGraphType<QuestionProposition>
    {
        public QuestionPropositionType()
        {
            Name = "QuestionProposition";
            Field(x => x.Id);
            Field(x => x.Libelle);
        }
    }
}