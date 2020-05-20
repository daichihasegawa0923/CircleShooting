using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TrapBase : MonoBehaviour
{
    [SerializeField] protected float _speed = 1.0f;
    protected ControlledCharacter _aim;
    protected Rigidbody _rigidbody;
    [SerializeField]
    protected int _damage = 10;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _aim = FindObjectOfType<ControlledCharacter>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.Chase();
    }

    protected virtual void Chase()
    {
        var forward = Vector3.zero;
        forward.x = (_aim.transform.position.x - this.transform.position.x) / Mathf.Abs(_aim.transform.position.x - this.transform.position.x);
        forward.z = (_aim.transform.position.z - this.transform.position.z) / Mathf.Abs(_aim.transform.position.z - this.transform.position.z);
        forward.y = (_aim.transform.position.y - this.transform.position.y) / Mathf.Abs(_aim.transform.position.y - this.transform.position.y);

        _rigidbody.velocity = forward * _speed;
    }
    
    public virtual void Burst(Vector3 speed)
    {
        this._rigidbody.velocity = speed;
        Destroy(this);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var character = collision.gameObject.GetComponent<ControlledCharacter>();

        if (character)
        {
            character.Damaged(this._damage);
            Destroy(gameObject);
        }
    }
}
