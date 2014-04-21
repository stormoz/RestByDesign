using System;
using Autofac;

namespace RestByDesign
{
    public static class IocHelper
    {
        public static IContainer CreateContainer(Action<ContainerBuilder> mapping = null)
        {
            var builder = new ContainerBuilder();

            if (mapping != null)
                mapping(builder);

            return builder.Build(); 
        }
    }
}