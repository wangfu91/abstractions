﻿using System;
using Unity.Build;
using Unity.Delegates;
using Unity.Policy;

namespace Unity.Injection
{
    /// <summary>
    /// A class that holds on to the given value and provides
    /// the required <see cref="IResolverPolicy"/>
    /// when the container is configured.
    /// </summary>
    public class InjectionParameter : TypedInjectionValue, IResolverPolicy
    {
        /// <summary>
        /// Create an instance of <see cref="InjectionParameter"/> that stores
        /// the given value, using the runtime type of that value as the
        /// type of the parameter.
        /// </summary>
        /// <param name="parameterValue">InjectionParameterValue to be injected for this parameter.</param>
        public InjectionParameter(object parameterValue)
            : this(GetParameterType(parameterValue), parameterValue)
        {
        }

        /// <summary>
        /// Create an instance of <see cref="InjectionParameter"/> that stores
        /// the given value, associated with the given type.
        /// </summary>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="parameterValue">InjectionParameterValue of the parameter</param>
        public InjectionParameter(Type parameterType, object parameterValue)
            : base(parameterType, parameterValue)
        {
        }


        private static Type GetParameterType(object parameterValue)
        {
            return (parameterValue ?? throw new ArgumentNullException(nameof(parameterValue))).GetType();
        }

        /// <summary>
        /// Return a <see cref="IResolverPolicy"/> instance that will
        /// return this types value for the parameter.
        /// </summary>
        /// <param name="typeToBuild">Type that contains the member that needs this parameter. Used
        /// to resolve open generic parameters.</param>
        /// <returns>The <see cref="IResolverPolicy"/>.</returns>
        public override IResolverPolicy GetResolverPolicy(Type typeToBuild)
        {
            return this;
        }

        public object Resolve<TContext>(ref TContext context) 
            where TContext : IBuildContext
        {
            return Value;
        }
    }

    /// <summary>
    /// A generic version of <see cref="InjectionParameter"/> that makes it a
    /// little easier to specify the type of the parameter.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    public class InjectionParameter<TParameter> : InjectionParameter
    {
        /// <summary>
        /// Create a new <see cref="InjectionParameter{TParameter}"/>.
        /// </summary>
        /// <param name="parameterValue">InjectionParameterValue for the parameter.</param>
        public InjectionParameter(TParameter parameterValue)
            : base(typeof(TParameter), parameterValue)
        {
        }
    }
}