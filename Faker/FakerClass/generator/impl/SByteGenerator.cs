using System;

namespace Faker.generator.impl
{
    public class SByteGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            return   (sbyte) random.Next(sbyte.MinValue, sbyte.MaxValue + 1);
        }

        public Type GetGenType()
        {
            return typeof(sbyte);
        }
    }
}