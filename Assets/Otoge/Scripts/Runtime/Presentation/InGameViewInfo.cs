using System.Collections.Generic;
using Otoge.Domain;
using UnityEngine;
using VContainer;

namespace Otoge.Presentation
{
    /// <summary>
    /// 主にノーツ表示に必要な情報.
    /// </summary>
    public class InGameViewInfo
    {
        public IList<float> LanePositionsX { get; } = new List<float>();
        public float LaneWidth { get; private set; }
        
        [Inject]
        public InGameViewInfo(InGameConfiguration inGameConfiguration)
        {
            // レーン幅計算.
            LaneWidth = 1f;
                
            // レーン位置計算.
            var startX = -(inGameConfiguration.LaneNum / 2.0f);
            for (int i = 0; i < inGameConfiguration.LaneNum; ++i)
            {
                // 真ん中のレーンが０に来るようにするために LaneNum/2 したものを引いてる.
                var x = startX + i * LaneWidth;
                LanePositionsX.Add(x);
                Debug.Log($"[InGameView] lane:{i}, posX:{x}");   
            }
        }
    }
}