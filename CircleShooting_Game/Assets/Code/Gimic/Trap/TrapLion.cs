using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLion : TrapBase
{
    [SerializeField] private int _hp = 2;
    [SerializeField] bool _isDamaged = false;
    [SerializeField] private Animator _animator;

    [SerializeField] private string DAMAGED_ANIMATOR_TRIGGER = "damaged";
    [SerializeField] private string RUN_ANIMATOR_TRIGGER = "run";
    public override void Burst(Vector3 speed)
    {
        this._animator.SetTrigger(this.DAMAGED_ANIMATOR_TRIGGER);
        if (this._hp > 0)
        {
            this.BurstSound();
            if (!this._isDamaged)
            {
                this._hp -= 1;
                this._isDamaged = true;
                speed *= 0.5f;
                speed.y += 20.0f;
                this._rigidbody.velocity = speed;
                StartCoroutine("DamagedCotoutine");
            }
            return;
        }
        base.Burst(speed);
    }

    protected override void Chase()
    {
        if (this._isDamaged)
            return;

        base.Chase();
    }

    IEnumerator DamagedCotoutine()
    {
        yield return new WaitForSeconds(2.0f);
        this._animator.SetTrigger(this.RUN_ANIMATOR_TRIGGER);
        this._isDamaged = false;
    }
}
