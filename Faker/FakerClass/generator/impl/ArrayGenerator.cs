using System;

namespace Faker.generator.impl
{
    public class ArrayGenerator<T>: IGenerator
    {
        public object Generate()
        {
            Random random = new Random();
            var elementType = typeof(T).GetElementType();
            if (elementType == null) return null;
            var length = random.Next(0, 9);
            var result = Array.CreateInstance(elementType, length);
            var faker = new Faker();
            var type = typeof(Faker);
            var methodInfo = type.GetMethod("Create");
            if (methodInfo == null) return null;
            methodInfo = methodInfo.MakeGenericMethod(elementType);
            for (var i = 0; i < length; i++)
            {
                var item = methodInfo.Invoke(faker, null);
                result.SetValue(item, i);
            }

            return result;
        }

        public Type GetGenType()
        {
            return typeof(Array);
        }
    }
}