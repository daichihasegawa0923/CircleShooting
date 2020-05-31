using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGameCanvasManager : MonoBehaviour
{
    [SerializeField]
    private PanelLoader _panelLoader;

    [SerializeField]
    private string _playPanelName = "Play";
    [SerializeField]
    private string _gameOverPanelName = "GameOver";

    [SerializeField]
    private Text _lastScoreText;

    public string PlayPanelName { private set => this._playPanelName = value; get => this._playPanelName; }
    public string GameOverPanelName { get => _gameOverPanelName; private set => _gameOverPanelName = value; }

    public void StartGame()
    {
        StartCoroutine("StartGameCoroutine",this._playPanelName);
    }

    public void ChangePanel(string panelName)
    {
        this._panelLoader.ActiveOnePanelByName(panelName);
    }
    
    private IEnumerator StartGameCoroutine(string panelName)
    {
        yield return new WaitForSeconds(2.0f);
        _panelLoader.ActiveOnePanelByName(panelName);
    }

    public void SetLastScoreText(int score)
    {
        this._lastScoreText.text = score.ToString();
    }
}
