using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterEffect : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 _cameraPosition;
    private void Start()
    {
        this._cameraPosition = this._camera.transform.position;
    }

    public void ShakeCamera()
    {
        var sequence = DOTween.Sequence();
        sequence
            .Append(this._camera.transform.DOShakePosition(0.9f, 1.0f, 4, 180, false, true))
            .Join(this._camera.transform.DOMove(this._cameraPosition,0.1f));
    }
}
