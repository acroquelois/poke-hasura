using System;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Skeleton.Api.GraphQL.Mutation;
using Skeleton.Api.GraphQL.Query;

namespace Skeleton.Api.GraphQL.Schemas
{
    public class QuestionSchema: Schema
    {
        public QuestionSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<QuestionQuery>();
            Mutation = provider.GetRequiredService<QuestionMutation>();
        }
    }
}