using Otoge.Domain;
using Otoge.Presentation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Otoge.Application
{
    public class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField] private NoteViewFactory noteViewFactory;
            
        protected override void Configure(IContainerBuilder builder)
        {
            //
            // Domain.
            //
            builder.Register<LifeCycle>(Lifetime.Singleton);
            builder.Register<InGamePlayer>(Lifetime.Singleton);
            builder.Register<ProgressTimer>(Lifetime.Singleton);
            builder.Register<BarLine>(Lifetime.Singleton);
            builder.Register<InputCommand>(Lifetime.Singleton);
            builder.Register<NoteContainer>(Lifetime.Singleton);
            //builder.Register<InGameMainLoop>(Lifetime.Singleton);
            builder.Register<InGameConfiguration>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<InGameMainLoop>();
            
            //
            // Presentation.
            //
            builder.Register<IInputEvent, InputEventPlayerPC>(Lifetime.Singleton);
            builder.RegisterComponent(noteViewFactory);
            builder.Register<NoteViewRepository>(Lifetime.Singleton);
            //builder.Register<NotePresenter>(Lifetime.Singleton);
            builder.Register<InGameViewInfo>(Lifetime.Singleton);

            builder.RegisterEntryPoint<NotePresenter>();
        }
    }
}