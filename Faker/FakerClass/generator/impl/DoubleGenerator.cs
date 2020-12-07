using System;

namespace Faker.generator.impl
{
    public class DoubleGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            var  buffer = new byte[8];
            double result;
            do
            {
                random.NextBytes(buffer);
                result = BitConverter.ToDouble(buffer, 0);
            } while (result == 0);

            return result;
        }

        public Type GetGenType()
        {
            return typeof(double);
        }
    }
}