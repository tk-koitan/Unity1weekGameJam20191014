using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// 初期化するシーン

namespace MainGame
{
    [System.Serializable]
    public class CupInitData
    {
        [SerializeField]
        private float stage_width = 15.0f;
        public float StageWidth { private set { stage_width = value; } get { return stage_width; } }
        [SerializeField]
        private float stage_height = 15.0f;
        public float StageHeight { private set { stage_height = value; } get { return stage_height; } }

        [SerializeField]
        private Vector3 default_cup_pos;
        public Vector3 DefaultCupPos { private set { default_cup_pos = value; } get { return default_cup_pos; } }
    }

    public class InitializeState : BaseState
    {
        // キャッシュ
        private CupInitData data;

        // コップのキャッシュ
        private List<CupController> cups;

        // 初期化
        public override void Init(CommonData common_data)
        {
            cups = common_data.cups;
            data = common_data.cup_init_data;

            // コップを追加する
            // コップを読み込む
            //CupController cup = Resources.Load("paper_cup") as CupController;
            int cup_num = common_data.stage_datas[common_data.dificulity].cups_num;

            // コップの間隔
            float each_distance = 0;
            if (cup_num != 1) each_distance = data.StageWidth / (cup_num - 1);
            float dis = each_distance / 2;
            if(cup_num % 2 == 1)
            {
                CupController new_cup = Instantiate(common_data.cup_data, data.DefaultCupPos, Quaternion.identity);
                cups.Add(new_cup);
                dis = each_distance;
            }
            for (int i = 0; i < cup_num / 2; ++i)
            {
                CupController new_cup = Instantiate(common_data.cup_data, data.DefaultCupPos + new Vector3(0f, 0f, dis), Quaternion.identity);
                CupController new_cup2 = Instantiate(common_data.cup_data, data.DefaultCupPos - new Vector3(0f, 0f, dis), Quaternion.identity);
                cups.Add(new_cup);
                cups.Add(new_cup2);
                dis += each_distance;
            }

            foreach(CupController cup in cups)
            {
                cup.transform.localEulerAngles = new Vector3(-90.0f, 0f, 0f);
            }


            // コップを上から降らせる
            //StartCoroutine(Flow());

        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("CupMove");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
        }

        private IEnumerator Flow()
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}