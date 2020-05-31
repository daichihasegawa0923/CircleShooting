using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PointEffect : MonoBehaviour
{
    [SerializeField]
    private Text _pointText;
    [SerializeField]
    private Text _comboText;
    public void AppearPointEffect(Vector3 position,int point,int comboCount)
    {
        position.y += 2.0f;
        transform.position = position;
        var endPosition = transform.position;
        position.y -= 2.0f;
        transform.position = position;

        var spin = transform.localEulerAngles;
        var scale = transform.localScale;

        transform.localEulerAngles += new Vector3(0, 180, 0);
        transform.localScale *= 0.1f;
        transform.DOMove(endPosition, 0.2f);
        transform.DORotate(spin, 0.2f);
        transform.DOScale(scale, 0.2f);
        Destroy(gameObject, 0.5f);

        _pointText.text = point.ToString();
        _comboText.text = comboCount.ToString();
    }
}
