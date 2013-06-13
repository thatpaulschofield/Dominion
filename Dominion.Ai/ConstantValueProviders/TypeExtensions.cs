using System;

namespace Dominion.Ai.ConstantValueProviders
{
    public static class TypeExtensions
    {
        public static bool IsDescendedFrom(this Type type, Type ancestorType)
        {
            do
            {
                if (type == ancestorType)
                    return true;

                type = type.BaseType;
            } while (type != null);

            return false;
        }

        public static bool HasGenericAncestor(this Type type)
        {
            return type.ClosestGenericAncestor() != null;
        }

        public static Type ClosestGenericAncestor(this Type type)
        {
            do
            {
                if (type.IsGenericType)
                    return type;

                type = type.BaseType;
            } while (type != null);

            return null;
        }
    }
}