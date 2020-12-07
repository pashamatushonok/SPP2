using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Faker.generator;
using Faker.generator.impl;

namespace Faker
{
    public class Faker : IFaker
    {
        private readonly Dictionary<Type, IGenerator> _generators;
        private readonly string _pathPlugin = Path.Combine(Directory.GetCurrentDirectory(), "Plugin");
        private List<Type> _listType;

        public Faker()
        {
            _listType = new List<Type>();
            _generators = new Dictionary<Type, IGenerator>();
        }

        public T create<T>()
        {
            var type = typeof(T);
            if (type.IsAbstract) return default;
            if (_generators.TryGetValue(type, out var _generator))
            {
                return (T) _generator.Generate();
            }

            if (type.IsEnum)
            {
                Array enumValues = type.GetEnumValues();
                Random random = new Random();
                return (T) enumValues.GetValue(random.Next(0, enumValues.Length));
            }
            if (type.IsArray)
            {
                return (T) new ArrayGenerator<T>().Generate();
            }
            if (type.IsClass || type.IsValueType)
            {
                if (_listType.Contains(type))
                {
                    return default;
                }

                _listType.Add(type);
                var inst = (T) CreateThroughConstructor(type);
                if (inst == null) return default;
                FillObject(inst);
                _listType.Remove(type);
                return inst;
            }

            return default;
        }
        
        private void LoadGeneratorsFromDirectory()
        {
            if (_generators == null) return;
            
            var pluginDirectory = new DirectoryInfo(_pathPlugin);
            if (!pluginDirectory.Exists)
            {
                pluginDirectory.Create();
                return;
            }

            var pluginFiles = Directory.GetFiles(pluginDirectory.FullName,"*.dll");

            foreach (var pluginFile in pluginFiles)
            {
                var assembly = Assembly.LoadFrom(pluginFile);
                LoadGeneratorsFromAssembly(assembly);
            }
        }

        private void LoadGeneratorsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => typeof(IGenerator).IsAssignableFrom(type));
            foreach (var type in types)
            {
                if (type.FullName == null) continue;
                if (!type.IsClass) continue;
                if (assembly.CreateInstance(type.FullName) is IGenerator generatorPlugin)
                {
                    _generators.Add(generatorPlugin.GetType(), generatorPlugin);
                }
            }
        }

        private void FillObject(object instance)
        {
            var type = instance.GetType();
            var fields = new List<FieldInfo>(type.GetFields());
            foreach (var field in fields)
            {
                if (field.IsLiteral) continue;
                var fieldType = field.FieldType;
                var value = Create(fieldType);
                field.SetValue(instance, value);
            }

            var properties = new List<PropertyInfo>(type.GetProperties());
            foreach (var property in properties)
            {
                if (!property.CanWrite) continue;
                var propertyType = property.PropertyType;
                var value = Create(propertyType);
                property.SetValue(instance, value);
            }
        }

        private object CreateThroughConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            var constructor = GetMaxParametersCountConstructor(constructors);
            if (constructor == null) return null;

            var parametersInfo = constructor.GetParameters();

            return constructor.Invoke(parametersInfo.Select(parameter => Create(parameter.ParameterType)).ToArray());
        }

        private object Create(Type type)
        {
            if (type.IsPointer) return IntPtr.Zero;
            var create = typeof(Faker).GetMethod("Create");
            return create == null ? null : create.MakeGenericMethod(type).Invoke(this, null);
        }

        private static ConstructorInfo GetMaxParametersCountConstructor(IReadOnlyList<ConstructorInfo> constructors)
        {
            if (constructors == null || constructors.Count <= 0) return null;

            var constructorInfo = constructors[0];
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length > constructorInfo.GetParameters().Length)
                {
                    constructorInfo = constructor;
                }
            }

            return constructorInfo;
        }
    }
}