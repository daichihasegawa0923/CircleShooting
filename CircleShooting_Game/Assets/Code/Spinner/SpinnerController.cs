using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiamondGames.CicleShooting.Spinner
{
    public class SpinnerController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _spinObject;

        [SerializeField]
        protected float _spinSpeed = 1.0f;
        protected Vector3 _firstTouchScreenPosition = Vector3.zero;
        protected bool _isTouching = false;

        protected Vector3 _currentSpinObjectSpin;

        [SerializeField]
        CanvasForDebug _canvasForDebug;

        [SerializeField]
        HummerAnimatorController _hummerAnimatorController;

        // Update is called once per frame
        protected virtual void Update()
        {
            this.Spin();
        }

        /// <summary>
        /// オブジェクトを回転させる
        /// </summary>
        protected virtual void Spin()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _firstTouchScreenPosition = Input.mousePosition;
                _currentSpinObjectSpin = _spinObject.transform.eulerAngles;
                _isTouching = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                _hummerAnimatorController.StopWand();
            }

            // タップ中の処理
            if (!_isTouching)
                return;

            var spin = Input.mousePosition.x > _firstTouchScreenPosition.x ? 1 : -1;
            var spinSpeed = Mathf.Abs(Input.mousePosition.x - _firstTouchScreenPosition.x) / Screen.width;

            // スクリーンの5%未満である時、処理を中断する
            if (Mathf.Abs(Input.mousePosition.x - _firstTouchScreenPosition.x) < Screen.width * 0.05f)
                return;

            this._hummerAnimatorController.Wand(spin > 0);
            
            //角度をオブジェクトに反映する
            var spins = _spinObject.transform.eulerAngles;
            spins.y += spin * _spinSpeed * spinSpeed * Time.timeScale;
            _spinObject.transform.eulerAngles = spins;
        }

        /// <summary>
        /// 座標から内角を計算する
        /// </summary>
        /// <param name="from">座標1</param>
        /// <param name="to">座標2</param>
        /// <returns>内角</returns>
        protected virtual float CalcAngle(Vector3 from, Vector3 to)
        {
            var dt = to - from;
            var rad = Mathf.Atan2(dt.x, dt.y);
            return rad * Mathf.Rad2Deg;
        }
    }
}

