using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

#if !NET5_0_OR_GREATER
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class IsExternalInit
    { }
}
#endif

namespace Otoge.Scripts2.Presentation.Note
{
    /// <summary>
    /// Viewを初期化するのに必要なデータ.
    /// </summary>
    public record InitializationViewData(
        IDictionary<int, Domain.Entities.Note> Notes,
        int MaxLaneNum,
        float Bpm,
        Vector3 NoteSize);
}