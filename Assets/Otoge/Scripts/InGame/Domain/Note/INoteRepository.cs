using System.Collections.Generic;

namespace Otoge.Scripts.InGame.Application.Interface
{
    public interface INoteRepository
    {
        public IList<global::Note> Notes { get; }
    }
}