using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class TrapBase : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    protected ControlledCharacter _aim;
    protected Rigidbody _rigidbody;
    [SerializeField]
    private int _damage = 10;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _burstedAudioClip;

    [SerializeField]
    private PointEffect _pointEffect;

    public float Speed { get => _speed; set => _speed = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public PointEffect PointEffect { get => _pointEffect; set => _pointEffect = value; }

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
        if (this._isBursted)
            return;

        // 点数の処理
        var point = Damage;
        var pointEffect = Instantiate(this.PointEffect.gameObject).GetComponent<PointEffect>();
        var combo = GameManager.PlusScore(ref point);
        pointEffect.AppearPointEffect(transform.position, point, combo);

        this._audioSource.clip = this._burstedAudioClip;
        this._audioSource.Play();

        this._rigidbody.velocity = speed;
        this._isBursted = true;
        Destroy(gameObject, 3.0f);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var character = collision.gameObject.GetComponent<ControlledCharacter>();
        var trapBase = collision.gameObject.GetComponent<TrapBase>();

        if (character && !this._isBursted)
        {
            character.Damaged(this._damage);
            Destroy(gameObject);
        }

        if(trapBase && this._isBursted)
        {
            var speed = collision.contacts[0].point - transform.position;
            speed *= 10.0f;
            speed.y = 1.0f;
            trapBase.Burst(speed);
        }
    }
}
