using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;

// メインゲームのシーンのベースクラス
// これを継承する

namespace MainGame
{
    public abstract class BaseScene : MonoBehaviour
    {
        // 初期化
        public abstract void Init(CommonData common_data);
        // 更新
        public abstract void Proc(CommonData common_data);
        // 終了処理
        public abstract void Final(CommonData common_data);
    }
}