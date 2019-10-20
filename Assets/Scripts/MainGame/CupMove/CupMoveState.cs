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
        public int move_num = 10;
        public float delay_time = 1.0f;
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
        private int move_num_min;
        private int move_num_max;
        private float move_duration;

        // 初期化
        public override void Init(CommonData common_data)
        {
            move_cnt = 0;
            cups = common_data.cups;
            data = common_data.cup_move_data;
            if (common_data.is_stage_select)
            {
                move_num_min = 1;
                move_num_max = 2;
                move_duration = 2.0f;
            }
            else
            {
                move_num_min = common_data.stage_datas[common_data.dificulity - 1].move_num_min;
                move_num_max = common_data.stage_datas[common_data.dificulity - 1].move_num_max;
                move_duration = common_data.stage_datas[common_data.dificulity - 1].move_duration;
            }
            timer = new Timer(move_duration + data.delay_time);

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

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("CupSelect");
#endif
        }

        // 終了
        public override void Final(CommonData common_data)
        {
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

                int round_num = Random.Range(move_num_min, move_num_max + 1);

                int dir = Random.Range(0, 2);
                bool inverse = (dir == 0);
                cups[first].MoveInit(cups[second].transform.position, move_duration, round_num, inverse);
                cups[second].MoveInit(cups[first].transform.position, move_duration, round_num, inverse);
            }
        }
    }
}