using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type.InputType
{
    public class QuestionPropositionInputType: InputObjectGraphType<QuestionProposition>
    {
        public QuestionPropositionInputType()
        {
            Name = "QuestionPropositionInput";
            Field(x => x.Id);
            Field(x => x.Libelle);
        }
    }
}