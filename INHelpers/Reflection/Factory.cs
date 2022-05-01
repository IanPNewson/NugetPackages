using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.Reflection
{
    public static class Factory
    {

        /// <summary>
        /// Instantiates a type given a type and a list of possible parameters for the constructor.
        /// 
        /// The most specific constructor (i.e. with the most parameters) are preferred.
        /// </summary>
        /// <param name="type">The type to instantiate</param>
        /// <param name="parameters">A list of parameters to potentially pass to the constructor. May not be null or contain null values.</param>
        /// <typeparam name="T">The type to instantiate</typeparam>
        public static T InstantiateBestMatch<T>(params object[] parameters)
            => (T)typeof(T).InstantiateBestMatch(parameters);

        /// <summary>
        /// Instantiates a type given a type and a list of possible parameters for the constructor.
        /// 
        /// The most specific constructor (i.e. with the most parameters) are preferred.
        /// </summary>
        /// <param name="type">The type to instantiate</param>
        /// <param name="parameters">A list of parameters to potentially pass to the constructor. May not be null or contain null values.</param>
        public static object InstantiateBestMatch(this Type type, params object[] parameters)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsAbstract)
                throw new ArgumentException($"Can't instantiate abstract type {type.FullName}");
            if (type.IsInterface)
                throw new ArgumentException($"Can't instantiate interface type {type.FullName}");
            if (type.IsGenericTypeDefinition)
                throw new ArgumentException($"Can't instantiate generic type definition type {type.FullName}");

            if (parameters.Any(x => x == null))
                throw new ArgumentException("parameters may not contain null entries", nameof(parameters));

            var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (!ctors.Any())
                    throw new ArgumentException($"Type {type.FullName} has no constructors and therefore cannot be instantiated");

            foreach (var ctor in ctors
                .OrderByDescending(x => x.GetParameters().Length)
            )
            {
                var parameterInstances = new object[ctor.GetParameters().Length];

                for (int i = 0; i < parameterInstances.Length; i++)
                {
                    var parameterInfo = ctor.GetParameters()[i];
                    var candidates = parameters
                        .Where(x => !parameterInstances.Take(i).Contains(x));

                    foreach (var parameter in candidates)
                    {
                        if (parameter.GetType().IsAssignableTo(parameterInfo.ParameterType))
                        {
                            parameterInstances[i] = parameter;
                            break;
                        }
                    }
                }

                if (!parameterInstances.Any(x => x == null))
                    return ctor.Invoke(parameterInstances);
            }

            throw new ArgumentException($"No candidate constructor was found for type {type.FullName} from parameter types {string.Join(", ", parameters.Select(x => x.GetType().Name))}");
        }

    }
}
