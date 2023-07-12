using System.Collections.Generic;
using UnityEngine;

public class InGameView : MonoBehaviour
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
    /// レーン位置.
    /// </summary>
    private List<Vector3> lanePositions = new();
    
    
    /// <summary>
    /// 初期化.
    /// </summary>
    /// <param name="notes"></param>
    public void Initialize(ViewInitializeData data)
    {
        
        // エフェクト.
        effectView.Initialize(data.MaxLaneNum, lanePositions);
    }
    
    /// <summary>
    /// ノーツ適用.
    /// </summary>
    /// <param name="note"></param>
    public void ApplyNote(Note note)
    {
        effectView.Play(note.Lane);
    }
}