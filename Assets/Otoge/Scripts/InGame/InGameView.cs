using System.Collections.Generic;
using UnityEngine;

public class InGameView : MonoBehaviour
{
    [SerializeField] private RectTransform line;
    [SerializeField] private RectTransform noteParent;
    [SerializeField] private GameObject notePrefab;
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

    private Vector2 linePos;
    private readonly Dictionary<int, NoteView> noteViews = new();

    private void Start()
    {
        // ラインの中央にbottom調整.
        linePos = line.anchoredPosition;
        var noteParentRectTransform = noteParent.GetComponent<RectTransform>();
        noteParentRectTransform.anchoredPosition = linePos;
        noteParentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, line.rect.height);
    }

    /// <summary>
    /// 初期化.
    /// </summary>
    /// <param name="notes"></param>
    public void Initialize(ICollection<Note> notes)
    {
        int screenCenterW = Screen.width / GameDefine.LaneNum;
        float laneHalfW = screenCenterW / 2f;
        foreach (var note in notes)
        {
            // 必要な情報はNoteViewに詰め込む.
            var view = new NoteView();
            view.Time = note.Time;
            view.GameObject = Instantiate(notePrefab, noteParent);
            view.RectTransform = view.GameObject.GetComponent<RectTransform>();
            // ノーツのX位置.
            var posX = screenCenterW * note.Lane + laneHalfW;
            posX -= screenCenterW; // 画面中央が基準だったのでhalf分引く
            posX *= 0.4f; // 調整
            view.RectTransform.anchoredPosition = new Vector2(posX, 0f);
            Debug.Log($"[InGameView] posX:{posX}");
            noteViews.Add(note.UId, view);
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
            var sub = view.Value.Time - progressTime;
            var posY = sub * 50f;
            var pos = new Vector2(view.Value.RectTransform.anchoredPosition.x, posY);
            view.Value.RectTransform.anchoredPosition = pos;
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