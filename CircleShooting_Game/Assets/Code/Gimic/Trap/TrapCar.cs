using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCar : TrapBase
{
    [SerializeField] private float _firstSpin = 20.0f;

    private bool _isStart = false;
    [SerializeField] private List<GameObject> _wheels;
    [SerializeField] private float _wheelReleaseDistance = 2.0f;
    [SerializeField] private float _wheelSpeed = 5.0f;

    protected override void Start()
    {
        var spin = this.transform.eulerAngles;
        spin.y = _firstSpin;
        this.transform.eulerAngles = spin;
        base.Start();
        StartCoroutine("WindingChase");
    }

    public override void Burst(Vector3 speed)
    {
        StartCoroutine("ReleaseWheel");
        base.Burst(speed);
    }

    protected override void Chase()
    {
        if(this.transform.position.z - this._aim.transform.position.z <= -5
            && !this._isBursted)
        {
            transform.LookAt(this._aim.transform.position);
            var spin = transform.eulerAngles;
            spin.y += 180;
            spin.x = 0;
            spin.z = 0;
            transform.eulerAngles = spin;
            this._rigidbody.velocity = transform.forward * -Speed;
        }
    }

    private IEnumerator WindingChase()
    {
        this._isStart = true;
        var count = 0;
        var dSpin = this._firstSpin  > 0 ?- 1 : 1;

        while (transform.position.z - this._aim.transform.position.z >= -5)
        {
            transform.position -= transform.forward * 0.2f;
            var spin = transform.eulerAngles;
            spin.y += dSpin;
            transform.eulerAngles = spin;
            count++;
            if (count >= Mathf.Abs(this._firstSpin) * 2)
            {
                dSpin *= -1;
                count = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator ReleaseWheel()
    {
        yield return new WaitForSeconds(1.0f);
        _wheels.ForEach(w =>
        {
            if (w == null)
                return;

            var trapBase = w.AddComponent<TrapBase>();
            trapBase.Speed = this._wheelSpeed;
            w.transform.parent = null;
        });
        _wheels.Clear();
    }
}
