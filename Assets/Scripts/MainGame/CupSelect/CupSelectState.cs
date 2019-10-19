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
        // キャッシュ
        private CupSelectData data;
        // コップのキャッシュ
        private List<CupController> cups;

        // 初期化
        public override void Init(CommonData common_data)
        {
            data = common_data.cup_select_data;
            cups = common_data.cups;
        }

        // 更新
        public override void Proc(CommonData common_data)
        {

            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("End");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
        }
    }
}