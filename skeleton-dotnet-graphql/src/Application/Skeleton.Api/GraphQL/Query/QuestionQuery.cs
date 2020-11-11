using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Skeleton.Api.GraphQL.Type;
using Skeleton.Domain.Models;
using Skeleton.Domain.Services.Core;

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
                    var service = context.RequestServices.GetRequiredService<ICrudService<Question, int>>();
                    return service.ListAsync();
                }
            );
            Field<QuestionType>(
                name: "question",
                arguments: new QueryArguments(new 
                    QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    id = context.GetArgument<int>("id");
                    var service = context.RequestServices.GetRequiredService<ICrudService<Question, int>>();
                    return service.GetAsync(id);
                }
            );
        }
    }
}