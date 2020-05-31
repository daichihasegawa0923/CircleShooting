﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _trapGeneratorParent;
    [SerializeField] OnGameCanvasManager _onGameCanvasManager;
    [SerializeField] ControlledCharacter _character;

    public enum GameState {onGame,menu,end};

    [SerializeField] private GameState _currentState;

    private static int score = 0;

    private static int comboCount = 0;
    private static float countTimeForCombo;

    // Start is called before the first frame update
    void Start()
    {
        StartGenerateTrap();
        _onGameCanvasManager.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (this._currentState)
        {
            case GameState.onGame:
                if (GameManager.comboCount > 0)
                    GameManager.countTimeForCombo += Time.deltaTime;
                this.JudgeGameOver();
                break;
            case GameState.end:

            default:
                break;
        }
    }

    private void JudgeGameOver()
    {
        if (_character.Hp > 0)
            return;

        this._currentState = GameState.end;
        this._onGameCanvasManager.SetLastScoreText(GameManager.score);
        StartCoroutine("FadeTimeScaleForGameOver");
    }

    private IEnumerator FadeTimeScaleForGameOver()
    {
        while (Time.timeScale > 0)
        {
            if (Time.timeScale <= 0.2f)
            {
                Time.timeScale = 0.0f;
                break;
            }
            Debug.Log(Time.timeScale);
            Time.timeScale -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        this._onGameCanvasManager.ChangePanel(this._onGameCanvasManager.GameOverPanelName);

        foreach (var trapBase in FindObjectsOfType<TrapBase>())
            Destroy(trapBase.gameObject);
    }

    /// <summary>
    /// スコアを加算します。
    /// </summary>
    /// <param name="plusScore">加算するスコアの元の数字</param>
    /// <returns>コンボ数</returns>
    public static int PlusScore(ref int plusScore)
    {
        if (countTimeForCombo <= 1.0f)
        {
            comboCount++;
            plusScore *= comboCount;
        }
        else
        {
            comboCount = 1;
        }

        countTimeForCombo = 0.0f;
        GameManager.score += plusScore;
        return GameManager.comboCount;
    }

    public static int GetScore()
    {
        return GameManager.score;
    }

    public void SetGameSpeed(float speed)
    {
        if (speed > 1)
            speed = 1;
        if (speed < 0)
            speed = 0;

        Time.timeScale = speed;
    }

    public void StartGenerateTrap()
    {
        var generators = this._trapGeneratorParent.GetComponentsInChildren<TrapGenerator>();
        foreach (var generator in generators)
            generator.StartGenerate();
    }
}
