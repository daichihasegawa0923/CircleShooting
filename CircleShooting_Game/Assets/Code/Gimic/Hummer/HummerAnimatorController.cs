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
        this._animator.SetTrigger(animationName);
    }

    public void StopWand()
    {
        this._animator.SetTrigger(this._noWand);
    }
}
