using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// カップが移動する場面

namespace MainGame
{
    public class CupMoveScene : BaseScene
    {
        // 初期化
        public override void Init(CommonData common_data)
        {
            Debug.Log("カップ移動　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            if (Input.GetKeyDown(KeyCode.A)) common_data.scene_queue.Enqueue("CupSelect");
            Debug.Log("カップ移動　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("カップ移動　終了");
        }
    }
}