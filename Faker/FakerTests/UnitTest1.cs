using System;
using Faker.generator.impl;
using NUnit.Framework;
namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Random random = new Random();
            Console.WriteLine((short) random.Next(short.MinValue, short.MaxValue + 1));
            Assert.True(true);
        }
    }
}