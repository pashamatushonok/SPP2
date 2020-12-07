using System;

namespace Faker.generator.impl
{
    public class CharGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            return Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        }

        public Type GetGenType()
        {
            return typeof(char);
        }
    }
}