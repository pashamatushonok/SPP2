using System;

namespace Faker.generator.impl
{
    public class DateTimeGenerator : IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            var buffer = new byte[8];
            long ticks;
            do
            { 
                random.NextBytes(buffer); 
                ticks = BitConverter.ToInt64(buffer, 0);
            } while (ticks <= DateTime.MinValue.Ticks || ticks >= DateTime.MaxValue.Ticks);

            return new DateTime(ticks);
        }

        public Type GetGenType()
        {
            return typeof(DateTime);
        }
    }
}