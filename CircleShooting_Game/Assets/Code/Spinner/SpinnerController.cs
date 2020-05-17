using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _spinObject;

    protected Vector3 _firstTouchScreenPosition = Vector3.zero;
    protected bool _isTouching = false;

    protected Vector3 _currentSpinObjectSpin;

    [SerializeField] CanvasForDebug _canvasForDebug;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Spin();
    }

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
        }

        if (!_isTouching)
            return;

        // スクリーンの中央座標の取得
        var centerOfScreen = Vector3.zero;
        centerOfScreen.x = Screen.width / 2;
        centerOfScreen.y = Screen.height / 2;

        // スライドした際の、中央の角度を求める
        var firstVector = _firstTouchScreenPosition - centerOfScreen;
        var secondVector = Input.mousePosition - centerOfScreen;

        var angle = Vector3.SignedAngle(secondVector,firstVector,Vector3.up);

        _canvasForDebug.SetAngleText(angle.ToString());

        //角度をオブジェクトに反映する
        var spins = _spinObject.transform.eulerAngles;
        spins.y = _currentSpinObjectSpin.y + angle;
        _spinObject.transform.eulerAngles = spins;
    }
}
