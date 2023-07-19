using Otoge.Domain;
using Otoge.Presentation;
using VContainer;
using VContainer.Unity;

namespace Otoge.Application
{
    public class InGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            //
            // Domain.
            //
            builder.Register<LifeCycle>(Lifetime.Singleton);
            builder.Register<InGamePlayer>(Lifetime.Singleton);
            builder.Register<ProgressTimer>(Lifetime.Singleton);
            builder.Register<BarLine>(Lifetime.Singleton);
            builder.Register<InGameMainLoop>(Lifetime.Singleton);
            
            builder.Register<InputCommand>(Lifetime.Singleton);
            
            //
            // Presentation.
            //
            builder.Register<IInputEvent, InputEventPlayerPC>(Lifetime.Singleton);

            builder.RegisterEntryPoint<InGameMainLoop>();
        }
    }
}