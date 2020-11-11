using GraphQL.Types;
using Skeleton.Domain.Models;

namespace Skeleton.Api.GraphQL.Type
{
    public class QuestionCategorieType: ObjectGraphType<QuestionCategorie>
    {
        public QuestionCategorieType()
        {
            Name = "QuestionCategorie";
            Field(x => x.Id);
            Field(x => x.Libelle);
        }
    }
    
    
}