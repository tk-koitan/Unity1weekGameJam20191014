using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// 終了処理

namespace MainGame
{
    [System.Serializable]
    public class CupFinalData
    {
        [SerializeField]
        private int hoge;
    }

    public class FinalizeState : BaseState
    {
        // 初期化
        public override void Init(CommonData common_data)
        {
            Debug.Log("メインゲーム終了　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            if (Input.GetKeyDown(KeyCode.A)) common_data.state_queue.Enqueue("Start");
            Debug.Log("メインゲーム終了　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("メインゲーム終了　終了");
        }
    }
}