using DiamondGames.CicleShooting.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageChooseManager : MonoBehaviour
{
    [SerializeField]
    private StageSelectElement[] _stageSelectElements;
    [SerializeField]
    private int _index = 0;

    [SerializeField]
    private Button _leftButton;
    [SerializeField]
    private Button _rightButton;
    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private SceneLoader _sceneLoader;

    [SerializeField]
    private Image _stageImage;

    [SerializeField]
    private Text _highScoreText;

    public StageSelectElement GetStageSelectElement()
    {
        return this._stageSelectElements[this._index];
    }

    private void OnEnable()
    {
        this.SetStage(0);
    }

    /// <summary>
    /// 選択中のステージを変更する
    /// </summary>
    /// <param name="i">変更する数字</param>
    public void SetStage(int i)
    {
        this._index += i;
        if (this._index < 0)
            this._index = 0;
        if (this._index >= this._stageSelectElements.Length)
            this._index = this._stageSelectElements.Length - 1;

        // ボタンのアクティブ&非アクティブを切り替える
        this._leftButton.interactable = this._index == 0 ? false : true;
        this._rightButton.interactable = this._index == this._stageSelectElements.Length - 1 ? false : true;

        //　ステージのイメージ画像を切り替える
        this._stageImage.sprite = this._stageSelectElements[this._index]._stageImage;

        this._highScoreText.text = PlayerPrefs.GetInt(this._stageSelectElements[this._index]._highScoreKey,0).ToString();
    }

    public void LoadScene()
    {
        this._sceneLoader.LoadSceneWithFadeout(this._stageSelectElements[this._index]._sceneName);
    }
}
