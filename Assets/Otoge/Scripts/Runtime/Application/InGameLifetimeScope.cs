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
        [SerializeField] private ComboView comboView;
            
        protected override void Configure(IContainerBuilder builder)
        {
            ////////////////////////////
            // Domain.
            ////////////////////////////
            builder.Register<LifeCycle>(Lifetime.Singleton);
            builder.Register<InGamePlayer>(Lifetime.Singleton);
            builder.Register<ProgressTimer>(Lifetime.Singleton);
            builder.Register<BarLine>(Lifetime.Singleton);
            builder.Register<InputCommand>(Lifetime.Singleton);
            builder.Register<NoteContainer>(Lifetime.Singleton);
            builder.Register<InGameConfiguration>(Lifetime.Singleton);
            builder.Register<Combo>(Lifetime.Singleton);

            builder.Register<IInitializable, InGameMainLoop>(Lifetime.Singleton);
            
            
            ////////////////////////////
            // Presentation.
            ////////////////////////////
            // 入力.
            builder.Register<IInputEvent, InputEventPlayerPC>(Lifetime.Singleton);
            
            // 表示.
            builder.Register<InGameViewInfo>(Lifetime.Singleton);

            builder.RegisterComponent(noteViewFactory);
            builder.Register<NoteViewRepository>(Lifetime.Singleton);
            builder.Register<IInitializable, NotePresenter>(Lifetime.Singleton);
            
            builder.RegisterComponent(comboView);
            builder.Register<IInitializable, ComboPresenter>(Lifetime.Singleton);
        }
    }
}