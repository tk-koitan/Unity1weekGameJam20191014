using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// カップを選択するシーン

namespace MainGame
{
    public class CupSelectScene : BaseScene
    {
        // 初期化
        public override void Init(CommonData common_data)
        {
            Debug.Log("カップ選択　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            if (Input.GetKeyDown(KeyCode.A)) common_data.scene_queue.Enqueue("End");
            Debug.Log("カップ選択　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("カップ選択　終了");
        }
    }
}