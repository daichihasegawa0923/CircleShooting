using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledCharacter : MonoBehaviour
{
    [SerializeField]
    protected int _maxHp = 100;
    [SerializeField]
    protected int _hp;

    /// <summary>
    /// HPの初期値を設定する
    /// </summary>
    protected virtual void Awake()
    {
        this._hp = this._maxHp;
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damagePoint"></param>
    public virtual void Damaged(int damagePoint)
    {
        this._hp -= damagePoint;
    }

    /// <summary>
    /// 回復する
    /// </summary>
    /// <param name="healPoint"></param>
    public virtual void Heal(int healPoint)
    {
        this._hp += healPoint;
        if (this._hp > this._maxHp)
            this._hp = this._maxHp;
    }
}
