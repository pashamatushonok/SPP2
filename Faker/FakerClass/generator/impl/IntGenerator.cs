using System;

namespace Faker.generator.impl
{
    public class IntGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            if (random.Next() % 2 == 0)
            {
                return random.Next(int.MaxValue) + 1;
            }
            
            return random.Next(int.MinValue, 0);
        }

        public Type GetGenType()
        {
            return typeof(int);
        }
    }
}