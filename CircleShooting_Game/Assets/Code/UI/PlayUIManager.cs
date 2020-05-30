using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    // Start is called before the first frame update

    private void Update()
    {
        this.SetScoreText();
    }

    public void SetScoreText()
    {
        this._scoreText.text = GameManager.GetScore().ToString();
    }
}
