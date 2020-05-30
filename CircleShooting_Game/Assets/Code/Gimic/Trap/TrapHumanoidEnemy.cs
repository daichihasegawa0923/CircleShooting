using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHumanoidEnemy : TrapBase
{
    protected enum HumanState { run,attack,afterAttack};
    [SerializeField] protected HumanState _currentHumanState;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _distance = 6.0f;

    [SerializeField] private Transform _gunMouse;
    [SerializeField] private TrapBase _burret;
    [SerializeField] private float _burretSpeed = 1.50f;
    private Vector3 _spin;

    private bool _canShoot = true;

    protected override void Start()
    {
        base.Start();
        this._spin = transform.eulerAngles;
    }

    protected override void Chase()
    {
        if (this._isBursted)
            return;

        switch (this._currentHumanState)
        {
            case HumanState.run:
                this._rigidbody.velocity = transform.forward * this.Speed;
                if (this.transform.position.z - this._aim.transform.position.z < this._distance)
                {
                    this._currentHumanState = HumanState.attack;
                    this._animator.SetBool("run", false);
                    this._rigidbody.velocity = Vector3.zero;
                    _animator.SetTrigger("gun");
                    transform.LookAt(this._aim.transform.position);
                    _animator.SetTrigger("gun_end");
                }
                break;
            case HumanState.attack:

                if (!_canShoot)
                    return;

                if (this._animator.GetCurrentAnimatorStateInfo(0).IsName("shoot"))
                {
                    _canShoot = false;
                    var burret = (GameObject)Instantiate(this._burret.gameObject);
                    burret.GetComponent<TrapBase>().Speed = this._burretSpeed;
                    burret.transform.position = this._gunMouse.position;
                    this._currentHumanState = HumanState.afterAttack;
                }
                break;
            case HumanState.afterAttack:
                if (!this._animator.GetBool("run"))
                {
                    this._animator.SetBool("run", true);
                    transform.eulerAngles = this._spin;
                }
                this._rigidbody.velocity = transform.forward * this.Speed;
                break;
            default:
                break;
        }

    }
}
