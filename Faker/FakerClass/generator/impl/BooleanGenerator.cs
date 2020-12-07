using System;

namespace Faker.generator.impl
{
    public class BooleanGenerator : IGenerator
    {
        public object Generate()
        {
            return true;
        }

        public Type GetGenType()
        {
            return typeof(bool);
        }
    }
}