using System;

namespace Faker.generator.impl
{
    public class ShortGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            return (short) random.Next(short.MinValue, short.MaxValue + 1);
        }

        public Type GetGenType()
        {
            return typeof(short);
        }
    }
}