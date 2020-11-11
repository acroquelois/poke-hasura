using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Skeleton.Api.GraphQL.Type;
using Skeleton.Api.GraphQL.Type.InputType;
using Skeleton.Domain.Models;
using Skeleton.Domain.Services.Core;

namespace Skeleton.Api.GraphQL.Mutation
{
    public class QuestionMutation : ObjectGraphType
    {
        public QuestionMutation()
        {
            Field<QuestionType>(
                name: "createQuestion",
                arguments: new QueryArguments(new
                    QueryArgument<NonNullGraphType<QuestionInputType>> {Name = "question"}),
                resolve: context =>
                {
                    var question = context.GetArgument<Question>("question");
                    var service = context.RequestServices.GetRequiredService<ICrudService<Question, int>>();
                    return service.InsertAsync(question);
                }
            );
        }
    }
}