using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type.InputType
{
    public class QuestionCategorieInputType: InputObjectGraphType<QuestionCategorie>
    {
        public QuestionCategorieInputType()
        {
            Field(x => x.Id);
            Field(x => x.Libelle);
        }
    }
    
    
}