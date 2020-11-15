using System;
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
                name: "questions", 
                arguments: new QueryArguments(new 
                    QueryArgument<IntGraphType> { Name = "limit" }),
                resolve: (context) =>
                {
                    try
                    {
                        var limit = context.GetArgument<int>("limit");
                        var service = context.RequestServices.GetRequiredService<IQuestionService>();
                        var result = service.ListAsync(limit).Result;
                        return result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return null;
                    }
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