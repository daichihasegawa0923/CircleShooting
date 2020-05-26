using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TrapBase : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    protected ControlledCharacter _aim;
    protected Rigidbody _rigidbody;
    [SerializeField]
    private int _damage = 10;

    public float Speed { get => _speed; set => _speed = value; }
    public int Damage { get => _damage; set => _damage = value; }

    protected bool _isBursted = false;

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
        if (this._isBursted)
            return;

        transform.LookAt(this._aim.transform.position);
        var forward = transform.forward;
        forward *= this._speed;
        forward.y = this._rigidbody.velocity.y;
        this._rigidbody.velocity = forward;
    }
    
    public virtual void Burst(Vector3 speed)
    {
        this._rigidbody.velocity = speed;
        this._isBursted = true;
        Destroy(gameObject, 2.0f);
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
