using GraphQL;
using GraphQL.Types;
using Skeleton.Api.GraphQL.Type;
using Skeleton.Domain.Models;
using Skeleton.Domain.Services.Core;

namespace Skeleton.Api.GraphQL.Query
{
    public class QuestionQuery: ObjectGraphType
    {
        private ICrudService<Question, int> _service;
        public QuestionQuery(ICrudService<Question, int> service)
        {
            _service = service;
            int id = 0;
            Field<ListGraphType<QuestionType>>(
                name: "questions", resolve: (context) =>
                    _service.ListAsync()
            );
            Field<QuestionType>(
                name: "question",
                arguments: new QueryArguments(new 
                    QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    id = context.GetArgument<int>("id");
                    return _service.GetAsync(id);
                }
            );
        }
    }
}