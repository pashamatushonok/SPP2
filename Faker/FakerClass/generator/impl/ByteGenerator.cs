using System;

namespace Faker.generator.impl
{
    public class ByteGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            var result = new byte[1];
            random.NextBytes(result);
            return result[0];
        }

        public Type GetGenType()
        {
            return typeof(byte);
        }
    }
}