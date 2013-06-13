using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.Infrastructure;
using StructureMap.Configuration.DSL;
using System;
using StructureMap.Graph;

namespace Dominion.Configuration
{
    public class DominionRegistry : Registry
    {
        public DominionRegistry()
        {
            For<GameBuilder>().Use<GameBuilder>();
            For<IEventAggregator>().Use<EventAggregator>();
            For<IBus>().Use<Bus>();
            For<ISagaRepository>().Use<SagaRepository>();
            For<EndGameCondition>().Use<ThreeSupplyPilesDepletedEndGameCondition>();
            For<IEnumerable<EndGameCondition>>().Use(cfg => 
                cfg.GetAllInstances<EndGameCondition>());
            For<Player.PlayerId>().Use<Player.PlayerId>();
            For<Guid>().Use(Guid.NewGuid);
        }
    }

    public class DefaultCtorParameterConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.IsAbstract || type.IsEnum || type.IsGenericTypeDefinition)
                return;

            var ctor = type.GetGreediestCtor();

            if (!ctor.HasOptionalParameters())
                return;

            var inst = registry.For(type).Use(type);

            foreach (var param in ctor.GetOptionalParameters())
            {
                object defaultValue = param.DefaultValue;
                if (defaultValue == null && param.ParameterType == typeof(string))
                    inst.Child(param.Name).Is(String.Empty);

                if (defaultValue == null && param.ParameterType == typeof(bool))
                    inst.Child(param.Name).Is(false);

                if (defaultValue == null && param.ParameterType == typeof(Int32))
                    inst.Child(param.Name).Is(0);

                if (defaultValue != null)
                    inst.Child(param.Name).Is(defaultValue);
            }
        }
    }

    public static class StructureMapExtensions
    {
        public static bool HasOptionalParameters(
            this ConstructorInfo ctor)
        {
            return ctor.GetOptionalParameters().Any();
        }

        public static IEnumerable<ParameterInfo> GetOptionalParameters(this ConstructorInfo ctor)
        {
            return ctor.GetParameters().Where(
                param => param.Attributes
                              .HasFlag(ParameterAttributes.Optional));
        }

        public static ConstructorInfo GetGreediestCtor(
            this Type target)
        {
            return target.GetConstructors()
                         .WithMax(ctor => ctor.GetParameters().Length);
        }

        public static T WithMax<T>(
            this IEnumerable<T> target, Func<T, int> selector)
        {
            int max = -1;
            T currentMax = default(T);

            foreach (var item in target)
            {
                var current = selector(item);
                if (current <= max)
                    continue;

                max = current;
                currentMax = item;
            }

            return currentMax;
        }
    }
}