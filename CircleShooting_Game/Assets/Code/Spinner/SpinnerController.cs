using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiamondGames.CicleShooting.Spinner
{
    public class SpinnerController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _spinObject;

        protected Vector3 _firstTouchScreenPosition = Vector3.zero;
        protected bool _isTouching = false;

        protected Vector3 _currentSpinObjectSpin;
        protected float _touchingSpin;

        [SerializeField] CanvasForDebug _canvasForDebug;

        protected Vector3 _centerOfScreenPosition;

        // Start is called before the first frame update
        void Start()
        {
            // スクリーンの中央座標の取得
            this._centerOfScreenPosition = Vector3.zero;
            this._centerOfScreenPosition.x = Screen.width / 2;
            this._centerOfScreenPosition.y = Screen.height / 2;
        }

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

                _touchingSpin = this.CalcAngle(this._centerOfScreenPosition, Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
            }

            // タップ中の処理
            if (!_isTouching)
                return;

            var angle = this.CalcAngle(this._centerOfScreenPosition, Input.mousePosition);
            var reangle = angle - _touchingSpin;
            _canvasForDebug.SetAngleText(angle.ToString() + ":" + this._touchingSpin);

            //角度をオブジェクトに反映する
            var spins = _spinObject.transform.eulerAngles;
            spins.y = _currentSpinObjectSpin.y + reangle;
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

