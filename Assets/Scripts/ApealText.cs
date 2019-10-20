using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Assertions;

public enum AppealType // 今はこれだけ
{
    LargeScale = 1, // 大きくなる
    Rotate = 2, // 回転する
    Shake = 3, // 揺れる
    Jump = 4, // 跳ねる
    Punch = 5, // びよよーん
    Blink = 6, // 点滅する
}

[System.Serializable]
public class AppealData
{
    public TextMeshProUGUI target; // 変化させる対称
    public AppealType type = AppealType.LargeScale;
    public Ease ease = Ease.Linear;
    public float duration = 1.0f; // 一回の継続時間
    // 変化の大きさ
    public float power = 1.0f;
    // ループするかどうか
    public bool is_loop = true;
}

public class ApealText : MonoBehaviour
{
    [SerializeField]
    private List<AppealData> texts;

    public void ChangeText(string new_text, int index)
    {
        texts[index].target.text = new_text;
    }
    public void DOAppeal(int index)
    {
        Assert.IsFalse(texts.Count <= index || index < 0, "領域外アクセス");

        AppealData target = texts[index];
        target.target.gameObject.SetActive(true);

        switch (target.type)
        {
            case AppealType.Blink:
                target.target.DOFade(0f, target.duration).SetEase(target.ease).SetLoops((target.is_loop) ? -1 : 1, LoopType.Yoyo);
                break;
            case AppealType.Jump:
                target.target.rectTransform.DOJump(target.target.rectTransform.position, 0f, 5, target.duration).SetEase(target.ease).SetLoops((target.is_loop) ? -1 : 1, LoopType.Yoyo);
                break;
            case AppealType.LargeScale:
                target.target.rectTransform.DOScale(target.target.rectTransform.localScale * (1.0f + target.power), target.duration).SetEase(target.ease).SetLoops((target.is_loop) ? -1 : 1, LoopType.Yoyo);
                break;
            case AppealType.Punch:
                target.target.rectTransform.DOPunchScale(target.target.rectTransform.localScale * (1.0f + target.power), target.duration).SetEase(target.ease).SetLoops((target.is_loop) ? -1 : 1, LoopType.Yoyo);
                break;
            case AppealType.Rotate:
                target.target.rectTransform.DORotate(target.target.rectTransform.position, target.duration).SetEase(target.ease).SetLoops((target.is_loop) ? -1 : 1, LoopType.Yoyo);
                break;
            case AppealType.Shake:
                target.target.rectTransform.DOShakeScale(target.duration, target.power).SetEase(target.ease).SetLoops((target.is_loop) ? -1 : 1, LoopType.Yoyo);
                break;
            default:
                Assert.IsTrue(true, "無効なタイプ");
                break;
        }
    }
}
