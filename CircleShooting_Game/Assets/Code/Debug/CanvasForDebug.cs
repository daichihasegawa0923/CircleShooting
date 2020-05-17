using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasForDebug : MonoBehaviour
{
    [SerializeField]
    private Text _angleText;

    public void SetAngleText(string textValue)
    {
        _angleText.text = textValue;
    }
}
