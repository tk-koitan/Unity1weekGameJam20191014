using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using TadaLib;
using TMPro;

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

        [SerializeField]
        private List<TextMeshPro> texts;
        public List<TextMeshPro> Texts { private set { texts = value; } get { return texts; } }

        public float close_time = 2.0f;
        public float apeal_time = 2.0f;
    }

    public class InitializeState : BaseState
    {
        // キャッシュ
        private CupInitData data;

        // コップのキャッシュ
        private List<CupController> cups;

        private float time = 0.0f;

        int state = 0;

        // 初期化
        public override void Init(CommonData common_data)
        {
            time = 0.0f;
            state = 0;

            cups = common_data.cups;
            data = common_data.cup_init_data;

            if (common_data.is_stage_select) MusicManager.Play(1);
            else MusicManager.Play(0);

            // コップを追加する
            // コップを読み込む
            //CupController cup = Resources.Load("paper_cup") as CupController;
            int cup_num;

            if (common_data.is_stage_select) {
                cup_num = 5;
            }
            else cup_num = common_data.stage_datas[common_data.dificulity - 1].cups_num;

            int cup_data = 0;
            if (common_data.cup_datas.Count == 2 && Random.Range(0, common_data.cup_datas.Count * 100) <= 20) cup_data = 1;
            float dis_y = 0.0f;
            if (cup_data == 1) dis_y = 1.5f;
            // コップの間隔
            float each_distance = 0;
            if (cup_num != 1) each_distance = data.StageWidth / (cup_num - 1);
            float dis_z = each_distance / 2;
            if (cup_num % 2 == 1)
            {
                float dis_x = Random.Range(0f, data.StageHeight / 2.0f);
                if (common_data.is_stage_select) dis_x = 0f;
                CupController new_cup = Instantiate(common_data.cup_datas[cup_data], data.DefaultCupPos + new Vector3(dis_x, dis_y, 0f), Quaternion.identity);
                cups.Add(new_cup);
                dis_z = each_distance;
            }
            for (int i = 0; i < cup_num / 2; ++i)
            {
                float dis_x = Random.Range(0f, data.StageHeight / 2.0f);
                if (common_data.is_stage_select) dis_x = 0f;
                CupController new_cup = Instantiate(common_data.cup_datas[cup_data], data.DefaultCupPos + new Vector3(dis_x, dis_y, dis_z), Quaternion.identity);
                CupController new_cup2 = Instantiate(common_data.cup_datas[cup_data], data.DefaultCupPos + new Vector3(dis_x, dis_y, -dis_z), Quaternion.identity);
                cups.Add(new_cup);
                cups.Add(new_cup2);
                dis_z += each_distance;
            }

            if (common_data.is_stage_select)
            {
                // csvファイルから各コップに入る数字を読み込む
                List<string[]> datas = CSVReader.LoadCSVFile("StageDificulity");
                int[] dificulities = new int[5];
                for (int i = 0; i < cup_num; ++i)
                {
                    dificulities[i] = int.Parse(datas[Mathf.Min(8, common_data.phase - 1)][i]);
                    data.Texts[i].text = dificulities[i].ToString();
                    // 一定の確率でジョーカーに
                    if (Random.Range(1, 101) <= 10 + common_data.phase)
                    {
                        dificulities[i] = 9;
                        data.Texts[i].text = "J";
                        data.Texts[i].color = Color.red;
                    }
                }

                // テキストを配置する
                for (int i = 0; i < cup_num; ++i) {
                    data.Texts[i].transform.parent = cups[i].transform;
                    data.Texts[i].transform.localPosition = cups[i].ItemPos;
                }

                {
                    int cnt = -1;
                    foreach (CupController cup in cups)
                    {
                        ++cnt;
                        //cup.gameObject.SetActive(false);
                        cup.Init(false, dificulities[cnt]);
                    }
                }
            }
            else
            {
                // 当たりのコップを設定する
                int has_item_index = Random.Range(0, cups.Count);

                // 当たりのコップのところに果物等を置く

                int item_index = Random.Range(0, common_data.item_datas.Count);
                common_data.item = Instantiate(common_data.item_datas[item_index], cups[has_item_index].transform.position, Quaternion.identity);
                common_data.item.transform.parent = cups[has_item_index].transform;
                common_data.item.transform.localPosition = cups[has_item_index].ItemPos;

                {
                    int cnt = -1;
                    foreach (CupController cup in cups)
                    {
                        ++cnt;
                        //cup.gameObject.SetActive(false);
                        cup.Init(cnt == has_item_index);
                    }
                }
            }

            // コップを上から降らせる
            //StartCoroutine(CupAppear())
        }

        // 更新
        public override void Proc(CommonData common_data)
        {
            time += Time.deltaTime;
            if(state == 0 && time >= data.close_time)
            {
                Transform cam_transform = Camera.main.transform;
                foreach (CupController cup in cups)
                {
                    if (!cup.HasItem && !common_data.is_stage_select) continue;
                    cup.Open(cam_transform, !common_data.is_stage_select);
                }
                ++state;
            }

            if(state == 1 && time >= data.close_time + data.apeal_time)
            {
                Transform cam_transform = Camera.main.transform;
                foreach (CupController cup in cups)
                {
                    if (!cup.HasItem && !common_data.is_stage_select) continue;
                    cup.Close(cam_transform);
                }
                ++state;
                common_data.state_queue.Enqueue("CupMove");
            }
            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("CupMove");
        }

        // 終了
        public override void Final(CommonData common_data)
        {
        }

        private IEnumerator CupAppear()
        {
            Transform cam_transform = Camera.main.transform;
            foreach (CupController cup in cups)
            {
                // カメラの方向を向くようにする
                cup.transform.LookAt(cam_transform);
                cup.transform.localEulerAngles = new Vector3(0f, cup.transform.eulerAngles.y + Mathf.Rad2Deg * Mathf.PI, 0f);

                cup.gameObject.SetActive(true);
                cup.Close(cam_transform);

                yield return new WaitForSeconds(1.0f / cups.Count);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}