using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Skeleton.Api.GraphQL.Type;
using Skeleton.Domain.Services;

namespace Skeleton.Api.GraphQL.Query
{
    public class QuestionQuery: ObjectGraphType
    {
        public QuestionQuery()
        {
            int id = 0;
            Field<ListGraphType<QuestionType>>(
                name: "questions", resolve: (context) =>
                {
                    var service = context.RequestServices.GetRequiredService<IQuestionService>();
                    var result = service.ListAsync().Result;
                    return result;
                }
            );
            
            Field<QuestionType>(
                name: "question",
                arguments: new QueryArguments(new 
                    QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    id = context.GetArgument<int>("id");
                    var service = context.RequestServices.GetRequiredService<IQuestionService>();
                    return service.GetAsync(id);
                }
            );
        }
    }
}