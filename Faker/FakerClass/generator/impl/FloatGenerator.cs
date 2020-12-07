using System;

namespace Faker.generator.impl
{
    public class FloatGenerator :IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            var buffer = new byte[4];
            float result;
            do
            {
                random.NextBytes(buffer);
                result = BitConverter.ToSingle(buffer, 0);
            } while (result == 0);

            return result;
        }

        public Type GetGenType()
        {
            return typeof(float);
        }
    }
}