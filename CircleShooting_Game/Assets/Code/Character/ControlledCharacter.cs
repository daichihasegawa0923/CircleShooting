using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlledCharacter : MonoBehaviour
{
    [SerializeField]
    protected int _maxHp = 100;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private Slider _hpSlider;

    [SerializeField]
    protected CharacterEffect _characterEffect;

    protected int Hp { get => _hp; set
        {
            _hpSlider.value = value;
            _hp = value; 

        } }

    /// <summary>
    /// HPの初期値を設定する
    /// </summary>
    protected virtual void Awake()
    {
        this._hpSlider.maxValue = this._maxHp;
        this.Hp = this._maxHp;
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
        this._characterEffect.ShakeCamera();
        this._characterEffect.DamageAudioPlay();
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
}
