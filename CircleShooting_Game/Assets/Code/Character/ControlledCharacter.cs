using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledCharacter : MonoBehaviour
{
    [SerializeField]
    protected int _maxHp = 100;

    protected int _hp;
    public int Hp { get => _hp; protected set => _hp = value; }

    /// <summary>
    /// HPの初期値を設定する
    /// </summary>
    protected virtual void Awake()
    {
        this.Hp = this._maxHp;
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damagePoint"></param>
    public virtual void Damaged(int damagePoint)
    {
        this.Hp -= damagePoint;
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
