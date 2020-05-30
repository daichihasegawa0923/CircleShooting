using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ControlledCharacter : MonoBehaviour
{
    [SerializeField]
    protected int _maxHp = 100;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private Slider _hpSlider;

    
    [SerializeField]
    private GameObject _hpBar;
    [SerializeField]
    private GameObject _hpBarBase;

    private float _hpBarXSize;

    [SerializeField]
    protected CharacterEffect _characterEffect;

    protected int Hp { get => _hp; set
        {
            var scale = _hpBar.transform.localScale;
            scale.x = this._hpBarXSize * ((float)value / this._maxHp);
            _hpBar.transform.localScale = scale;

            _hp = value; 

        } }

    /// <summary>
    /// HPの初期値を設定する
    /// </summary>
    protected virtual void Awake()
    {
        this._hpBarXSize = _hpBar.transform.localScale.x;
        this._hpSlider.maxValue = this._maxHp;
        this.Hp = this._maxHp;

        this.AppearHpStatusBar();
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damagePoint"></param>
    public virtual void Damaged(int damagePoint)
    {
        this.Hp -= damagePoint;
        if (this.Hp < 0)
            this.Hp = 0;
        this._characterEffect.DamageEffectAppear(transform.position);
    }

    /// <summary>
    /// 回復する
    /// </summary>
    /// <param name="healPoint"></param>
    public virtual void Heal(int healPoint)
    {
        this.Hp += healPoint;
        if (this.Hp > this._maxHp)
            this.Hp = this._maxHp;
    }

    protected void AppearHpStatusBar()
    {
        var position = this._hpBarBase.transform.position;
        var positionY = this._hpBarBase.transform.position;
        positionY.y -= 25.0f;
        this._hpBarBase.transform.position = positionY;

        this._hpBarBase.transform.DOMove(position,4.0f);
    }
}
