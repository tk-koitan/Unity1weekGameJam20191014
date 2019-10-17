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
        public Queue<string> scene_queue;
        // オブジェクト
        public List<CupController> cups;
        public List<GameObject> stages;
        public GameObject camera;

        public CommonData()
        {
            scene_queue = new Queue<string>();
            cups = new List<CupController>();
            stages = new List<GameObject>();
        }
    }

    public class Director : MonoBehaviour
    {
        // 各場面のシーン
        BaseScene scene = null;
        // すべてのシーンで共通するデータ
        [SerializeField]
        private CommonData common_data;
        // シーン名とそれに対応するシーンクラスの辞書
        private Dictionary<string, BaseScene> factory;

        // Start is called before the first frame update
        void Start()
        {
            factory = new Dictionary<string, BaseScene>();

            // シーンを登録
            RegisterScene();

            // sceneが何故かずっとnullなのでしょうがなく
            scene = factory["Start"];
            scene.Init(common_data);
        }

        // Update is called once per frame
        void Update()
        {
            // シーンを変更するかどうか
            CheckSequence();

            // 更新
            scene.Proc(common_data);
        }

        // シーンを登録
        private void RegisterScene()
        {
            factory.Add("Start", new InitializeScene());
            factory.Add("CupMove", new CupMoveScene());
            factory.Add("CupSelect", new CupSelectScene());
            factory.Add("End", new FinalizeScene());

        }

        // シーケンス変更
        private void CheckSequence()
        {
            // シーン変更 unityの機能で別のシーン(タイトルなど)に移動する時は呼ばれない (Unityなので別に大丈夫)
            while (common_data.scene_queue.Count != 0)
            {
                Assert.IsTrue(factory.ContainsKey(common_data.scene_queue.Peek()), "登録されていないシーンです");
                scene.Final(common_data); // ここ呼ばれない
                scene = factory[common_data.scene_queue.Dequeue()];
                scene.Init(common_data);
            }
        }
    }
}