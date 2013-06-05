using System;

namespace Dominion.Ai.Nodes.Functions
{
    public static class TypeExtensions
    {
        public static Type CloseWith(this Type openType, params Type[] typeParams)
        {
            return openType.GetGenericTypeDefinition().MakeGenericType(typeParams);
        }

        public static Type GenericTypeArgument(this Type closedGeneric)
        {
            return closedGeneric
                .GetGenericArguments()[0];
        }
    }


}