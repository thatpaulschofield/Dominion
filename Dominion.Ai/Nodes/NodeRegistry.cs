using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.AI;
using Dominion.Ai.ConstantValueProviders;
using StructureMap;

namespace Dominion.Ai.Nodes
{
    public class NodeRegistry
    {
        private readonly IContainer _container;
        private readonly IValueProviderRegistry _valueProviderRegistry;
        private readonly List<Type> _argumentTypes = new List<Type>();

        private readonly List<Type> _nodeTypes = new List<Type>();

        private readonly List<Type> _functionTypes = new List<Type>();

        private readonly List<Type> _terminalTypes = new List<Type>();

        public NodeRegistry(IContainer container, IValueProviderRegistry valueProviderRegistry)
        {
            _container = container;
            _valueProviderRegistry = valueProviderRegistry;
            this.GetType().Assembly.GetTypes().Where(t => !t.IsAbstract 
                //&& t.
                &&  !t.IsGenericType
                && !(t == typeof(NullNode) 
                || t == typeof(NullNode<>))).ForEach(ScanType);
        }

        private void ScanType(Type type)
        {
            if (typeof(INode).IsAssignableFrom(type))
                RegisterNodeType(type);
        }

        private void RegisterNodeType(Type type)
        {
            if (type.IsAbstract)
                return;

            if (typeof (Function).IsAssignableFrom(type))
                _functionTypes.Add(type);

            if (typeof (Terminal).IsAssignableFrom(type))
                _terminalTypes.Add(type);

            _nodeTypes.Add(type);


            if (type.IsGenericType)
                RegisterGenericArguments(type);
            if (type.BaseType != null && type.BaseType.IsGenericType)
                RegisterGenericArguments(type.BaseType);
        }

        public void RegisterType(Type type)
        {
            RegisterNodeType(type);
        }

        private void RegisterGenericArguments(Type type)
        {
            var args = type.GetGenericArguments();
            args
                //.Where(t => !t.IsGenericParameter)
                .ForEach(RegisterArgumentType);
        }

        private void RegisterArgumentType(Type type)
        {
            if (!_argumentTypes.Contains(type))
                _argumentTypes.Add(type);
        }

        public IEnumerable<INode> FunctionSet
        {
            get { return _functionTypes.SelectMany(t => CloseGenericsAgainstArgumentTypes(t)
                .Select(CreateInstance));
            }
        }

        public IEnumerable<INode> TerminalSet
        {
            get
            {
                return _terminalTypes
                    .SelectMany(CloseGenericsAgainstArgumentTypes)
                    .Where(InitializerIsAvailableIfNeeded)
                    .Select(CreateInstance);
            }
        }

        private bool InitializerIsAvailableIfNeeded(Type type)
        {
            if (typeof (Terminal).IsAssignableFrom(type))
                return true;

            var terminalValueType = type.BaseType.GetGenericArguments()[0];
            return _valueProviderRegistry.HasProviderFor(terminalValueType);

            //var initializerType =
            //    typeof(IInitialValueProvider<>).GetGenericTypeDefinition().MakeGenericType(new Type[] { terminalValueType });

            //var valueProviders = _container.GetAllInstances(initializerType);
            //return valueProviders.Count > 0;
        }

        private INode CreateInstance(Type type)
        {
            return Activator.CreateInstance(type) as INode;
        }

        private IEnumerable<Type> CloseGenericsAgainstArgumentTypes(Type t)
        {
            if (!t.IsGenericTypeDefinition)
            {
                yield return t;
                yield break;
            }

            var genericType = t.GetGenericTypeDefinition();
            var argsCount = genericType.GetGenericArguments().Count();
            foreach (var argType in _argumentTypes)
            {
                if (!ConstraintViolated(t, argType))
                {
                    var typeArgs = (from m in Enumerable.Range(1, argsCount) select argType).ToArray();
                    var closedType = genericType.MakeGenericType(typeArgs);
                    yield return closedType;
                }
            }
        }

        private bool ConstraintViolated(Type type, Type argType)
        {
            return
                !type.GetGenericArguments()
                    .All(arg => arg.GetGenericParameterConstraints().All(t => argType.IsAssignableFrom(arg)));
        }
    }
}