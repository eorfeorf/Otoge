using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private RectTransform line;
    [SerializeField] private RectTransform noteParent;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI comboText;

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
    public void Initialize(IList<Note> notes)
    {
        foreach (var note in notes)
        {
            var view = new NoteView();
            view.Time = note.Time;
            view.GameObject = Instantiate(notePrefab, noteParent);
            view.RectTransform = view.GameObject.GetComponent<RectTransform>();
            noteViews.Add(note.Id, view);
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
    /// ノーツが通り過ぎた.
    /// </summary>
    /// <param name="note"></param>
    public void PassNote(Note note)
    {
        ApplyRank(note, GameDefine.JudgeRank.Miss);
        return;
        var view = noteViews[note.Id];
        view.GameObject.SetActive(false);
    }

    /// <summary>
    /// ランク反映.
    /// </summary>
    /// <param name="rank"></param>
    public void ApplyRank(Note note, GameDefine.JudgeRank rank)
    {
        noteViews[note.Id].GameObject.SetActive(false);
        switch (rank)
        {
            case GameDefine.JudgeRank.Perfect:
                rankText.text = GameText.RANK_PERFECT;
                break;
            case GameDefine.JudgeRank.Great:
                rankText.text = GameText.RANK_GREAT;
                break;
            case GameDefine.JudgeRank.Good:
                rankText.text = GameText.RANK_GOOD;
                break;
            case GameDefine.JudgeRank.Miss:
                rankText.text = GameText.RANK_MISS;
                break;
        }
    }

    /// <summary>
    /// コンボ数変化.
    /// </summary>
    /// <param name="count"></param>
    public void ChangedCombo(int count)
    {
        comboText.text = count.ToString();
        comboText.gameObject.SetActive(count != 0);
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