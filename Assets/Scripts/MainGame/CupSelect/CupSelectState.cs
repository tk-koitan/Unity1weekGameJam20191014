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
        public float open_time = 2.0f;
        public float next_scene_time = 4.0f;
    }

    public class CupSelectState : BaseState
    {
        // キャッシュ
        private CupSelectData data;
        // コップのキャッシュ
        private List<CupController> cups;

        Camera camera;

        private float time = 0.0f;
        private bool end = false;
        private bool is_clear = false;
        private bool is_opened = false;

        // 初期化
        public override void Init(CommonData common_data)
        {
            data = common_data.cup_select_data;
            cups = common_data.cups;
            camera = Camera.main;
            time = 0.0f;
            end = false;
            is_opened = false;
        }

        // 更新
        public override void Proc(CommonData common_data)
        {

#if UNITY_ENGINE
            if (Input.GetKeyDown(KeyCode.Space)) common_data.state_queue.Enqueue("End");
#endif
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit_info = new RaycastHit();
                float max_distance = 100f;

                bool is_hit = Physics.Raycast(ray, out hit_info, max_distance);

                if (is_hit && !end)
                {
                    if (hit_info.transform.tag == "Cup")
                    {
                        GameObject obj = hit_info.collider.gameObject;
                        //TODO: ヒットした時の処理;
                        obj.GetComponent<CupController>().Open(Camera.main.transform, !common_data.is_stage_select);
                        is_clear = obj.GetComponent<CupController>().HasItem;
                        GameEnd.Cleared = is_clear;
                        end = true;
                        if (common_data.is_stage_select)
                        {
                            Director.dificulity = obj.GetComponent<CupController>().dificlity;
                        }
                    }
                }
            }

            if (end)
            {
                time += Time.deltaTime;
                if(!is_opened && time >= data.open_time)
                {
                    Open(common_data);
                }
                if(time >= data.next_scene_time)
                {
                    common_data.state_queue.Enqueue("End");
                }
            }
        }

        // 終了
        public override void Final(CommonData common_data)
        {
        }

        private void Open(CommonData common_data)
        {
            Transform cam_transform = Camera.main.transform;
            foreach (CupController cup in cups)
            {
                if (cup.IsOpened) continue;
                cup.Open(cam_transform, !common_data.is_stage_select);
            }
        }
    }
}