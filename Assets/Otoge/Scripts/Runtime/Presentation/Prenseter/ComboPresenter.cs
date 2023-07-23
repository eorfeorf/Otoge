using Otoge.Domain;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace Otoge.Presentation
{
    public class ComboPresenter : IInitializable
    {
        [Inject]
        public ComboPresenter(Combo combo, ComboView comboView, LifeCycle lifeCycle)
        {
            combo.OnValueChanged.Subscribe(comboView.ChangedCombo).AddTo(lifeCycle.CompositeDisposable);
        }

        public void Initialize()
        {
        }
    }
}