using System;
using System.Text;

namespace Faker.generator.impl
{
    public class StringGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            var builder = new StringBuilder();
            var size = random.Next(byte.MaxValue + 1);
            for (var i = 0; i < size; i++)
            {
                var symbol = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(symbol);
            }

            return builder.ToString();
        }

        public Type GetGenType()
        {
            return typeof(string);
        }
    }
}