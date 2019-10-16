using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// 初期化するシーン

namespace MainGame
{
    public class InitializeScene : BaseScene
    {
        // 初期化
        public override void Init(CommonData common_data)
        {
            Debug.Log("メインゲーム開始　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            if (Input.GetKeyDown(KeyCode.A)) common_data.scene_queue.Enqueue("CupMove");
            Debug.Log("メインゲーム開始　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("メインゲーム開始　終了");
        }
    }
}