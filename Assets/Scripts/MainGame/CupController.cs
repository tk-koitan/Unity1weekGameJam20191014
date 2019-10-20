using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame;
using DG.Tweening;
using TadaLib;

// コップの移動処理を各

namespace MainGame
{
    public class CupController : MonoBehaviour
    {

        // 移動しているかどうか
        public bool IsMoving { private set; get; }
        public bool IsOpened { private set; get; }
        public bool HasItem { private set; get; }
   
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Vector3 item_pos;
        public Vector3 ItemPos { private set { item_pos = value; } get { return item_pos; } }

        [SerializeField]
        private ParticleSystem[] effects;

        // ステージセレクト画面での難易度
        public int dificlity = 1;

        private Timer timer;

        private Vector3 target_pos;

        private Vector3 center_pos;
        private float radius;
        private float duration;
        // 角速度
        private float anguler_velocity;

        private bool inverse;
        private float initial_theta;

        // 初期化
        public void Init(bool has_item, int _dificulity = 1)
        {
            HasItem = has_item;
            dificlity = _dificulity;
            IsOpened = false;
        }

        // 移動の準備 移動 目的地 移動時間 回転数
        public void MoveInit(Vector3 _target_pos, float _duration, int _round_num = 1, bool _inverse = false, bool _change_pos = false)
        {
            IsMoving = true;
            target_pos = _target_pos;
            timer = new Timer(_duration);
            inverse = _inverse;
            duration = _duration;

            // 移動速度などを決める
            radius = Vector3.Distance(transform.position, _target_pos) / 2.0f;
            center_pos = (transform.position + _target_pos) / 2.0f;

            Vector3 dir = center_pos - transform.position;
            initial_theta = Mathf.Atan2(dir.z, dir.x) + Mathf.PI; 
            float dif = -0.5f;
            if (_change_pos) dif = -1f;
            // 各速度
            anguler_velocity = 2.0f * Mathf.PI / (_duration / (_round_num - dif));

            if (_change_pos) target_pos = transform.position;

        }

        // 移動 目的地 移動時間 いーじんぐ
        public void Moving()
        {
            timer.TimeUpdate(Time.deltaTime);

            if (timer.IsTimeout())
            {
                IsMoving = false;
                timer.TimeReset();
                transform.position = target_pos;
                return;
            }

            float theta = anguler_velocity * SineInOut((inverse)? duration - timer.GetTime() : timer.GetTime(), duration, 0f, duration);
            theta += initial_theta;
            transform.position = center_pos + new Vector3(radius * Mathf.Cos(theta), 0.0f, radius * Mathf.Sin(theta));
        }

        // オープン
        public void Open(Transform lookat, bool is_effect_on = false)
        {
            if (is_effect_on)
            {
                if (HasItem) for (int i = 1; i < effects.Length; ++i) effects[i].Play();
                else effects[0].Play();
            }
            IsOpened = true;
            // カメラの方向を向くようにする
            transform.LookAt(lookat);
            transform.localEulerAngles = new Vector3(0f, transform.eulerAngles.y + Mathf.Rad2Deg * Mathf.PI, 0f);
            animator.Play("open");
        }

        // クローズ
        public void Close(Transform lookat)
        {
            IsOpened = false;
            // カメラの方向を向くようにする
            transform.LookAt(lookat);
            transform.localEulerAngles = new Vector3(0f, transform.eulerAngles.y + Mathf.Rad2Deg * Mathf.PI, 0f);
            animator.Play("close");
        }

        // いーじんぐ
        private float SineInOut(float t, float total_time, float min, float max)
        {
            max -= min;
            return -max / 2 * (Mathf.Cos(t * Mathf.PI / total_time) - 1) + min;
        }
    }
}