using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using UnityEngine.Assertions;
using TMPro;

// すべてを呼び出すクラス

/* メインゲーム内のシーンを変更するときは
common_data.scene_queue に

"Start"
"CupMove"
"CupSelect"
"End" 

を代入してください

*/

namespace MainGame
{
    // すべてのシーンに共通するデータ
    [System.Serializable]
    public class CommonData
    {
        // 難易度
        public int dificulity = 1;
        public List<Dificulity> stage_datas;

        // シーン
        public Queue<string> state_queue;
        // オブジェクト
        public CupController cup_data;
        public GameObject item_data;
        public List<CupController> cups;
        public GameObject item;
        public List<GameObject> stages;
        public GameObject camera;

        // 各ステートのデータ
        public CupMoveData cup_move_data;
        public CupSelectData cup_select_data;
        public CupInitData cup_init_data;
        public CupFinalData cup_final_data;

        // あたったか
        public bool is_atari;

        public CommonData()
        {
            state_queue = new Queue<string>();
            cups = new List<CupController>();
            stages = new List<GameObject>();
            stage_datas = new List<Dificulity>();
        }
    }

    // 難易度
    public class Dificulity
    {
        public int cups_num { private set; get; } // コップの数
        public float move_duration { private set; get; } // 一回の移動時間
        public int move_num_min { private set; get; } // 一回で回る回数の最小
        public int move_num_max { private set; get; } // 一回で回る回数の最大

        public Dificulity(int _cups_num, float _move_duration, int _move_num_min, int _move_num_max)
        {
            cups_num = _cups_num;
            move_duration = _move_duration;
            move_num_min = _move_num_min;
            move_num_max = _move_num_max;
        }
    }

    public class Director : MonoBehaviour
    {
        // 各場面
        BaseState state = null;
        // すべてのシーンで共通するデータ
        public CommonData common_data;
        // シーン名とそれに対応するシーンクラスの辞書
        private Dictionary<string, BaseState> factory;

        public static int dificulity = 1;

        [SerializeField]
        private TextMeshProUGUI text;

        // Start is called before the first frame update
        private void Start()
        {
            common_data.dificulity = dificulity;

            factory = new Dictionary<string, BaseState>();

            // シーンを登録
            RegisterState();

            // csvファイルからステージのデータを持ってくる
            LoadStageData();

            // sceneが何故かずっとnullなのでしょうがなく
            state = factory["Start"];
            state.Init(common_data);
            text.text = "Start";
        }

        // Update is called once per frame
        private void Update()
        {
            // シーンを変更するかどうか
            CheckSequence();

            // 更新
            state.Proc(common_data);
        }

        // シーンを登録
        private void RegisterState()
        {
            factory.Add("Start", new InitializeState());
            factory.Add("CupMove", new CupMoveState());
            factory.Add("CupSelect", new CupSelectState());
            factory.Add("End", new FinalizeState());

        }

        // シーケンス変更
        private void CheckSequence()
        {
            // シーン変更 unityの機能で別のシーン(タイトルなど)に移動する時は呼ばれない (Unityなので別に大丈夫)
            while (common_data.state_queue.Count != 0)
            {
                Assert.IsTrue(factory.ContainsKey(common_data.state_queue.Peek()), "登録されていないシーンです");
                state.Final(common_data); // ここ呼ばれない
                text.text = common_data.state_queue.Peek();
                state = factory[common_data.state_queue.Dequeue()];
                state.Init(common_data);
            }
        }

        // csvファイルからステージのデータを持ってくる
        private void LoadStageData()
        {
            // csvファイルから難易度データを持ってくる
            List<string[]> data = TadaLib.CSVReader.LoadCSVFile("StageData");

            // 2行目以降をint型に置き換える
            for (int i = 1; i < data.Count; ++i)
            {
                int cup_num = int.Parse(data[i][0]);
                float move_duration = float.Parse(data[i][1]);
                int move_num_min = int.Parse(data[i][2]);
                int move_num_max = int.Parse(data[i][3]);

                common_data.stage_datas.Add(new Dificulity(cup_num, 
                    move_duration, move_num_min, move_num_max));
            }
        }
    }
}