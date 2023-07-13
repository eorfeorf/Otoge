using Otoge.Domain;
using VContainer;
using VContainer.Unity;

namespace Otoge.Application
{
    public class InGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InGamePlayer>(Lifetime.Singleton);
        }
    }
}