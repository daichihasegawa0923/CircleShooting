using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _rightWand = "right";
    [SerializeField] private string _leftWand = "left";
    [SerializeField] private string _noWand = "noWand";
    
    public void Wand(bool isRight)
    {
        var animationName = isRight ? this._rightWand : this._leftWand;
        var noAnimationName = isRight ? this._leftWand : this._rightWand;
        this._animator.SetBool(animationName,true);
        this._animator.SetBool(noAnimationName, false);
    }

    public void StopWand()
    {
        this._animator.SetBool(this._rightWand,false);
        this._animator.SetBool(this._leftWand, false);
    }
}
