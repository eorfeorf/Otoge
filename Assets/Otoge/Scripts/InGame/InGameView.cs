using System.Collections.Generic;
using UnityEngine;

public class InGameView : MonoBehaviour
{
    [SerializeField] private Transform line;
    [SerializeField] private Transform noteParent;
    [SerializeField] private Transform notePrefab;
    [SerializeField] private RankView rankView;
    [SerializeField] private ComboView comboView;

    /// <summary>
    /// コンボ.
    /// </summary>
    public ComboView ComboView => comboView;
    /// <summary>
    /// 判定文字.
    /// </summary>
    public RankView RankView => rankView;

    private readonly Dictionary<int, NoteView> noteViews = new();

    private void Start()
    {
    }

    /// <summary>
    /// 初期化.
    /// </summary>
    /// <param name="notes"></param>
    public void Initialize(ICollection<Note> notes)
    {
        foreach (var note in notes)
        {
            // 必要な情報はNoteViewに詰め込む.
            var viewTransform = Instantiate(notePrefab, noteParent);
            var view = new NoteView(viewTransform, note.Time);
            
            // ノーツのX位置.
            var posX = note.Lane - GameDefine.LANE_NUM / 2.0f;
            posX *= 0.05f;
            view.Transform.position = new Vector3(posX, 0f, 0f);
            noteViews.Add(note.UId, view);
            Debug.Log($"[InGameView] posX:{posX}");
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