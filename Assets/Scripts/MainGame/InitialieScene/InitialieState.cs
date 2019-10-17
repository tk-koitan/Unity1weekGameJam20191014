using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// 初期化するシーン

namespace MainGame
{
    [System.Serializable]
    public class CupInitData
    {
        [SerializeField]
        private int hoge;
    }

    public class InitializeState : BaseState
    {
        // キャッシュ
        private CupInitData data;

        // コップのキャッシュ
        private List<CupController> cups;

        // 初期化
        public override void Init(CommonData common_data)
        {
            cups = common_data.cups;
            data = common_data.cup_init_data;

            Debug.Log("メインゲーム開始　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("CupMove");
            Debug.Log("メインゲーム開始　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("メインゲーム開始　終了");
        }
    }
}