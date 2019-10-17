using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using TadaLib;

// コップが移動する場面

namespace MainGame
{
    [System.Serializable]
    public class CupMoveData
    {
        public float move_time = 0.5f;

        public float delay_time = 1.0f;
    }

    public class CupMoveState : BaseState
    {
        // コップのキャッシュ
        private List<CupController> cups;
        // データのキャッシュ
        private CupMoveData data;

        // 時間計測
        private Timer timer;

        // 初期化
        public override void Init(CommonData common_data)
        {
            cups = common_data.cups;
            data = common_data.cup_move_data;

            timer = new Timer(data.move_time + data.delay_time);

            Debug.Log("コップ移動　初期化");
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            timer.TimeUpdate(Time.deltaTime);

            if (timer.IsTimeout())
            {
                timer.TimeReset();
                DoCupMove(common_data);
            }

            foreach(CupController cup in cups)
            {
                if (cup.IsMoving) cup.Moving();
            }

            if (Input.GetKeyDown(KeyCode.A)) common_data.state_queue.Enqueue("CupSelect");
            Debug.Log("コップ移動　更新");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
            Debug.Log("コップ移動　終了");
        }

        // コップを移動させる
        private void DoCupMove(CommonData common_data)
        {
            // コップを2つランダムに選ぶ
            int size = cups.Count;
            if (size < 2) return;

            int first = Random.Range(0, size);
            int second = Random.Range(0, size - 1);
            if (first == second) second = size - 1;

            int round_num = Random.Range(1, 5);

            cups[first].MoveInit(cups[second].transform.position, data.move_time, round_num);
            cups[second].MoveInit(cups[first].transform.position, data.move_time, round_num);
        }
    }
}