using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// コップを選択するシーン

namespace MainGame
{
    [System.Serializable]
    public class CupSelectData
    {
        [SerializeField]
        private int hoge;
    }

    public class CupSelectState : BaseState
    {
        // 初期化
        public override void Init(CommonData common_data)
        {
            Debug.Log("コップ選択　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {


            if (Input.GetKeyDown(KeyCode.A)) common_data.state_queue.Enqueue("End");
            Debug.Log("コップ選択　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("コップ選択　終了");
        }
    }
}