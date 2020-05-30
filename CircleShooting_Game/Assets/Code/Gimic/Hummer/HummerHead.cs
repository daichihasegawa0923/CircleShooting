using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerHead : MonoBehaviour
{
    [SerializeField] private PointEffect _pointEffect;

    [SerializeField] float _burstSpeed = 10.0f;
    [SerializeField] AudioSource _audioSouce;

    [SerializeField] GameObject _hibanaEffect;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var trap = collision.gameObject.GetComponent<TrapBase>();

        if (trap)
        {
            this.AppearHibanaEffect(collision.contacts[0].point);
            var speedBase = trap.transform.position - collision.contacts[0].point;
            speedBase *= _burstSpeed;
            speedBase.y += 5.0f;
            trap.Burst(speedBase);
            this._audioSouce.Play();

            var pointEffect = Instantiate(this._pointEffect.gameObject).GetComponent<PointEffect>();
            pointEffect.AppearPointEffect(collision.contacts[0].point,trap.Damage);
        }
    }

    private void AppearHibanaEffect(Vector3 position)
    {
        if (!this._hibanaEffect)
            return;

        var effect = (GameObject)Instantiate(this._hibanaEffect);
        effect.transform.position = position;
        effect.transform.parent = transform;
        Destroy(effect, 2.0f);
    }
}
