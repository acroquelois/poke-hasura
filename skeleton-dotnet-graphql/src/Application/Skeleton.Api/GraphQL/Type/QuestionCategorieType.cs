using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type
{
    public class QuestionCategorieType: ObjectGraphType<QuestionCategorie>
    {
        public QuestionCategorieType()
        {
            Field(x => x.Id);
            Field(x => x.Libelle);
        }
    }
    
    
}