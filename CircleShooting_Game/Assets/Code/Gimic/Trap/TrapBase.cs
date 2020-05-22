﻿using System.Collections;
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
