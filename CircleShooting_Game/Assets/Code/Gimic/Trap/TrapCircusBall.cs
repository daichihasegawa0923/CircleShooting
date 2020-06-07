using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCircusBall : TrapBase
{
    [SerializeField]
    private float _jumpPower = 5.0f;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        var point = collision.contacts[0].point;
        var jumpPowerBase = transform.position - point;
        this._rigidbody.velocity += jumpPowerBase * this._jumpPower;
    }
}
