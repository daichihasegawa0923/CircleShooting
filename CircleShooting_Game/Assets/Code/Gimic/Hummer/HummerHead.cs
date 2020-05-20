using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerHead : MonoBehaviour
{
    [SerializeField] float _burstSpeed = 10.0f;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var trap = collision.gameObject.GetComponent<TrapBase>();

        if (trap)
        {
            var speedBase = trap.transform.position - collision.contacts[0].point;
            speedBase *= _burstSpeed;
            speedBase.y += 10.0f;
            trap.Burst(speedBase);
        }
    }
}
