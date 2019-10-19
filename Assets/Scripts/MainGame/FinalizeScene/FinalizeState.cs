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
        // キャッシュ
        private CupFinalData data;

        // 初期化
        public override void Init(CommonData common_data)
        {
            data = common_data.cup_final_data;
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("Start");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            for(int i = common_data.cups.Count - 1; i >= 0 ; --i)
            {
                Destroy(common_data.cups[i].gameObject);
            }
            common_data.cups.Clear();
            Destroy(common_data.item);
            common_data.dificulity = Mathf.Min(7, common_data.dificulity + 1);
        }
    }
}