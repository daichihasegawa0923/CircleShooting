using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapPiero : TrapBase
{
    [SerializeField] private TrapGenerator[] _generators;

    private void Awake()
    {
        _generators.All((g) => 
        {
            g.StartGenerate();
            return true;
        });
    }

    public override void Burst(Vector3 speed)
    {
        _generators.All((g) =>
        {
            if (g != null)
                Destroy(g);
            return true;
        });

        base.Burst(speed);
    }

    protected override void Chase()
    {
        if (this._isBursted)
            return;
        if (this.transform.position.z < this._aim.transform.position.z - 10)
            Destroy(gameObject);

        if (transform.position.z >= this._aim.transform.position.z + 3.0f)
            base.Chase();
        else
        {
            var spin = transform.eulerAngles;
            spin.y = 180;
            transform.eulerAngles = spin;
            this._rigidbody.velocity = transform.forward * this.Speed;
        }
    }
}
