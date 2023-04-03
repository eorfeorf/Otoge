using Otoge.Scripts2.Domain;
using Otoge.Scripts2.Domain.Input;
using UniRx;

namespace Otoge.Scripts2.Controllers
{
    public class DevicePC : IDevice 
    {
        public IReadOnlyReactiveProperty<InputEventData> Push { get; }
        public IReadOnlyReactiveProperty<InputEventData> Release { get; }

        private ReactiveProperty<InputEventData> _push = new();
        private ReactiveProperty<InputEventData> _release = new();
        
        
    }
}