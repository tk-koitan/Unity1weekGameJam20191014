using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using TadaLib;

// コップが移動する場面

namespace MainGame
{
    public class CupMoveScene : BaseScene
    {
        // コップのキャッシュ
        private List<CupController> cups;

        // 時間計測
        private Timer timer;

        private float move_time = 0.5f;

        private float delay_time = 1.0f;

        // 初期化
        public override void Init(CommonData common_data)
        {
            cups = common_data.cups;

            timer = new Timer(move_time + delay_time);

            Debug.Log("コップ移動　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            timer.TimeUpdate(Time.deltaTime);

            if (timer.IsTimeout())
            {
                timer.TimeReset();
                DoCupMove();
            }

            if (Input.GetKeyDown(KeyCode.A)) common_data.scene_queue.Enqueue("CupSelect");
            Debug.Log("コップ移動　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("コップ移動　終了");
        }

        // コップを移動させる
        private void DoCupMove()
        {
            // コップを2つランダムに選ぶ
            int size = cups.Count;
            if (size < 2) return;

            int first = Random.Range(0, size);
            int second = Random.Range(0, size - 1);
            if (first == second) second = size - 1;

            cups[first].Move(cups[second].transform.position, move_time, DG.Tweening.Ease.Linear);
            cups[second].Move(cups[first].transform.position, move_time, DG.Tweening.Ease.Linear);
        }
    }
}