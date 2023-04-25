using System.Collections.Generic;
using Otoge.Scripts2.Domain.Entities;
using Otoge.Scripts2.Presentation.Note;
using Otoge.Scripts2.Presentation.Views;
using UnityEngine;

namespace Otoge.Scripts2.Presentation
{
    public class View : MonoBehaviour
    {
        /// <summary>
        /// 判定ライン.
        /// </summary>
        [SerializeField]
        private Transform line;
        /// <summary>
        /// ノーツの親オブジェクト.
        /// </summary>
        [SerializeField]
        private Transform noteParent;
        /// <summary>
        /// 小節線の親オブジェクト.
        /// </summary>
        [SerializeField]
        private Transform barParent;
    
        [Header("Prefab")]
        /// <summary>
        /// ノーツPrefab.
        /// </summary>
        [SerializeField]
        private Transform notePrefab;
        /// <summary>
        /// 小節線Prefab
        /// </summary>
        [SerializeField]
        private Transform barPrefab;
    
    
        [Header("View")]
        /// <summary>
        /// 判定View.
        /// </summary>
        [SerializeField]
        private RankView rankView;
        /// <summary>
        /// コンボView.
        /// </summary>
        [SerializeField]
        private ComboView comboView;
        /// <summary>
        /// エフェクトView.
        /// </summary>
        [SerializeField]
        private EffectView effectView;
        /// <summary>
        /// スコアView.
        /// </summary>
        [SerializeField]
        private ScoreView scoreView;

        /// <summary>
        /// ノーツの親.
        /// </summary>
        public Transform NotesParent => noteParent;
        /// <summary>
        /// コンボ.
        /// </summary>
        public ComboView ComboView => comboView;
        /// <summary>
        /// 判定文字.
        /// </summary>
        public RankView RankView => rankView;
        /// <summary>
        /// スコア.
        /// </summary>
        public ScoreView ScoreView => scoreView;
    
        /// <summary>
        /// 特定のViewを生成する.
        /// </summary>
        /// <param name="parent"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>(Transform parent) where T : Component
        {
            if (typeof(T) == typeof(NoteView))
            {
                return Instantiate(notePrefab, parent) as T;
            }

            if (typeof(T) == typeof(BarView))
            {
                return Instantiate(barPrefab, parent) as T;
            }

            Debug.LogError("Create invalid view.");
            return default;
        }
    }
}