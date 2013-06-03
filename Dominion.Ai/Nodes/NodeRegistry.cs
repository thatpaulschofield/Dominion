using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.AI;
using Dominion.Ai.Nodes;
using Dominion.Ai.TreeBuilding;
using StructureMap;

namespace Dominion.Ai
{
    public class NodeRegistry
    {
        private readonly IContainer _container;
        private readonly List<Type> _argumentTypes = new List<Type>();

        private readonly List<Type> _nodeTypes = new List<Type>();

        private readonly List<Type> _functionTypes = new List<Type>();

        private readonly List<Type> _terminalTypes = new List<Type>();

        public NodeRegistry(IContainer container)
        {
            _container = container;
            this.GetType().Assembly.GetTypes().Where(t => !(t == typeof(NullNode) || t == typeof(NullNode<>))).ForEach(ScanType);
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

        private void RegisterGenericArguments(Type type)
        {
            var args = type.GetGenericArguments();
            args.Where(t => !t.IsGenericParameter)
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
                return _terminalTypes.SelectMany(CloseGenericsAgainstArgumentTypes)
                    .Where(InitializerIsAvailable)
                    .Select(CreateInstance);
            }
        }

        private bool InitializerIsAvailable(Type type)
        {
            var terminalValueType = type.BaseType.GetGenericArguments()[0];
            var initializerType =
                typeof(IInitialValueProvider<>).GetGenericTypeDefinition().MakeGenericType(new Type[] { terminalValueType });
            var valueProviders = _container.GetAllInstances(initializerType);
            return valueProviders.Count > 0;
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