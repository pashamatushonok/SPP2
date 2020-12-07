using System;

namespace Faker.generator
{
    public interface IGenerator
    {
        object Generate();
        
        Type GetGenType();
    }
}