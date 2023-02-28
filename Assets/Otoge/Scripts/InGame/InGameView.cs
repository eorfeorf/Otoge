using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class InGameView : MonoBehaviour
{
    [SerializeField]
    private Transform line;
    [SerializeField]
    private Transform noteParent;
    [SerializeField]
    private Transform notePrefab;
    [SerializeField]
    private RankView rankView;
    [SerializeField]
    private ComboView comboView;
    
    [Header("エフェクト")]
    [SerializeField]
    private VisualEffect tapEffectPrefab;

    /// <summary>
    /// コンボ.
    /// </summary>
    public ComboView ComboView => comboView;
    /// <summary>
    /// 判定文字.
    /// </summary>
    public RankView RankView => rankView;

    /// <summary>
    /// UID,noteView
    /// </summary>
    private readonly Dictionary<int, NoteView> noteViews = new();

    /// <summary>
    /// レーン位置.
    /// </summary>
    private List<Vector3> lanePositions = new();

    private List<VisualEffect> vfxs = new();

    private void Start()
    {
    }

    /// <summary>
    /// 初期化.
    /// </summary>
    /// <param name="notes"></param>
    public void Initialize(ViewInitializeData data)
    {
        // レーン位置計算.
        for (int i = 0; i < data.MaxLaneNum; ++i)
        {
            var posX = i - data.MaxLaneNum / 2.0f + (notePrefab.transform.lossyScale.x/2f);
            //var posX = i;
            lanePositions.Add(new Vector3(posX, 0f, 0f));
            Debug.Log($"[InGameView] lane:{i}, posX:{posX}");
        }
        
        // レーン位置決定
        foreach (var note in data.Notes)
        {
            // 必要な情報はNoteViewに詰め込む.
            var viewTransform = Instantiate(notePrefab, noteParent);
            var view = new NoteView(viewTransform, note.Time, note.Lane);
            
            // ノーツのX位置.
            view.Transform.position = lanePositions[view.LaneIndex];
            noteViews.Add(note.UId, view);
        }
        
        // エフェクト
        for (int i = 0; i < data.MaxLaneNum; ++i)
        {
            var vfx = Instantiate(tapEffectPrefab, lanePositions[i], Quaternion.identity, transform);
            vfxs.Add(vfx);
        }
    }

    /// <summary>
    /// 経過時間更新.
    /// </summary>
    /// <param name="progressTime"></param>
    public void UpdateProgressTime(float progressTime)
    {
        foreach (var view in noteViews)
        {
            // ノーツ時間と経過時間を比較してノーツの位置を計算.
            var noteView = view.Value;
            var pos = noteView.Transform.position;
            var sub = view.Value.Time - progressTime;
            var posY = sub * GameDefine.NOTE_BASE_SPEED;
            pos = new Vector3(pos.x, posY, pos.z);
            view.Value.Transform.position = pos;
        }
    }

    /// <summary>
    /// ノーツ適用.
    /// </summary>
    /// <param name="note"></param>
    public void ApplyNote(Note note)
    {
        noteViews[note.UId].GameObject.SetActive(false);
        vfxs[note.Lane].Play();
    }

    /// <summary>
    /// ノーツが通り過ぎた.
    /// 判定範囲外になったタイミングで呼び出される.
    /// </summary>
    /// <param name="note"></param>
    public void PassNote(Note note)
    {
        noteViews[note.UId].GameObject.SetActive(false);
    }

    /// <summary>
    /// リセット.
    /// </summary>
    public void Reset()
    {
        foreach (var view in noteViews)
        {
            view.Value.GameObject.SetActive(true);
        }
    }
}