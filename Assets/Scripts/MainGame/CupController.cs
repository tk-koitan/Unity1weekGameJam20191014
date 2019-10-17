using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using DG.Tweening;

// コップの移動処理を各

namespace MainGame
{
    public class CupController : MonoBehaviour
    {
        // 移動しているかどうか
        public bool IsMoving { private set; get; }

        // 初期化
        public void Init()
        {
            
        }

        // 移動 目的地 移動時間 いーじんぐ
        public void Move(Vector3 target_pos, float duration, Ease ease)
        {
            IsMoving = true;
            transform.DOMove(
                target_pos, duration).SetEase(ease).OnComplete(() => IsMoving = false);
        }

        // オープン
        public void Open(Vector3 rotation, float duration, Ease ease)
        {
            transform.DORotate(
                rotation,
                duration).SetEase(ease);
        }
    }
}