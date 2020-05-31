using DiamondGames.CicleShooting.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChooseManager : MonoBehaviour
{
    [SerializeField]
    private StageSelectElement[] _stageSelectElements;
    [SerializeField]
    private int _index = 0;

    [SerializeField]
    private SceneLoader _sceneLoader;

    public StageSelectElement GetStageSelectElement()
    {
        return this._stageSelectElements[this._index];
    }

    /// <summary>
    /// 選択中のステージを変更する
    /// </summary>
    /// <param name="i">変更する数字</param>
    public void ChangeStage(int i)
    {
        this._index += i;
        if (this._index < 0)
            this._index = 0;
        if (this._index >= this._stageSelectElements.Length)
            this._index = this._stageSelectElements.Length - 1;
    }

    public void LoadScene()
    {
        this._sceneLoader.LoadSceneWithFadeout(this._stageSelectElements[this._index]._sceneName);
    }
}
