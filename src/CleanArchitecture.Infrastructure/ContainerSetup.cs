using Autofac;
using Autofac.Extensions.DependencyInjection;
using CleanArchitecture.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CleanArchitecture.Infrastructure
{
	public static class ContainerSetup
	{
		public static IServiceProvider InitializeWeb(Assembly webAssembly, IServiceCollection services) =>
			new AutofacServiceProvider(BaseAutofacInitialization(setupAction =>
			{
				setupAction.Populate(services);
				setupAction.RegisterAssemblyTypes(webAssembly).AsImplementedInterfaces();
			}));

		public static IContainer BaseAutofacInitialization(Action<ContainerBuilder> setupAction = null)
		{
			var builder = new ContainerBuilder();

			var coreAssembly = Assembly.GetAssembly(typeof(BaseEntity));
			builder.RegisterAssemblyTypes(coreAssembly).AsImplementedInterfaces();

			setupAction?.Invoke(builder);
			return builder.Build();
		}
	}
}
