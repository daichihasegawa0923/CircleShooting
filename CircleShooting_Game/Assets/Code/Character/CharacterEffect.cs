using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterEffect : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 _cameraPosition;

    [SerializeField] private AudioSource _audioSouce;
    [SerializeField] private AudioClip _damageAudioClip;

    [SerializeField] private GameObject _damageEffectParticle;

    private void Start()
    {
        this._cameraPosition = this._camera.transform.position;
    }

    public void ShakeCamera()
    {
        var sequence = DOTween.Sequence();
        sequence
            .Append(this._camera.transform.DOShakePosition(0.9f, 1.0f, 4, 180, false, true))
            .Join(this._camera.transform.DOMove(this._cameraPosition,1.0f));
    }

    public void DamageEffectAppear(Vector3 position)
    {
        this.DamageAudioPlay();
        this.ShakeCamera();
        this.DamageParticleAppear(position);
    }

    public void DamageAudioPlay()
    {
        this._audioSouce.clip = this._damageAudioClip;
        this._audioSouce.Play();
    }

    public void DamageParticleAppear(Vector3 position)
    {
        var particle = Instantiate(this._damageEffectParticle);
        particle.transform.position = position;
        Destroy(particle, 2.0f);
    }
}
