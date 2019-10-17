using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using UnityEngine.Assertions;

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
        // シーン
        public Queue<string> state_queue;
        // オブジェクト
        public List<CupController> cups;
        public List<GameObject> stages;
        public GameObject camera;

        // 各ステートのデータ
        public CupMoveData cup_move_data;

        public CommonData()
        {
            state_queue = new Queue<string>();
            cups = new List<CupController>();
            stages = new List<GameObject>();
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

        // Start is called before the first frame update
        void Start()
        {
            factory = new Dictionary<string, BaseState>();

            // シーンを登録
            RegisterState();

            // sceneが何故かずっとnullなのでしょうがなく
            state = factory["Start"];
            state.Init(common_data);
        }

        // Update is called once per frame
        void Update()
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
                state = factory[common_data.state_queue.Dequeue()];
                state.Init(common_data);
            }
        }
    }
}