using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using TadaLib;

// コップが移動する場面

namespace MainGame
{
    // Inspectorに表示
    [System.Serializable]
    public class CupMoveData
    {
        public float move_time = 0.5f;

        public float delay_time = 1.0f;

        public int move_num = 10;
    }

    public class CupMoveState : BaseState
    {
        public bool IsAtari { private set; get; }

        // コップのキャッシュ
        private List<CupController> cups;
        // データのキャッシュ
        private CupMoveData data;

        // 時間計測
        private Timer timer;

        private int move_cnt = -1;

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
                ++move_cnt;
                if(move_cnt == data.move_num)
                {
                    common_data.state_queue.Enqueue("CupSelect");
                    return;
                }
                timer.TimeReset();
                DoCupMove(common_data);
            }

            foreach(CupController cup in cups)
            {
                if (cup.IsMoving) cup.Moving();
            }

            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("CupSelect");
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
            int size = cups.Count;
            if (size < 2) return;

            // コップのインデックスをコピーしてシャッフルする
            List<int> index_list = new List<int>();
            for (int i = 0; i < size; ++i) {
                index_list.Add(i);
            }
            // シャッフル
            for(int i = size - 1; i >= 0; --i)
            {
                int target = Random.Range(0, i + 1);
                int tmp = index_list[i];
                index_list[i] = index_list[target];
                index_list[target] = tmp;
            }

            // 先頭から2つずつ選ぶ
            for(int i = 0; i < size / 2; ++i)
            {
                int first = index_list[i * 2];
                int second = index_list[i * 2 + 1];

                int round_num = Random.Range(1, 5);

                cups[first].MoveInit(cups[second].transform.position, data.move_time, round_num);
                cups[second].MoveInit(cups[first].transform.position, data.move_time, round_num);
            }
        }
    }
}