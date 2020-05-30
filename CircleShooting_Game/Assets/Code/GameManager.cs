using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected List<TrapGenerator> _trapGenerators;

    public enum GameState {onGame,menu,end};

    [SerializeField] private GameState _currentState;

    private static int score = 0;

    private static int comboCount = 0;
    private static float countTimeForCombo;

    // Start is called before the first frame update
    void Start()
    {
        StartGenerateTrap();
    }

    // Update is called once per frame
    void Update()
    {
        switch (this._currentState)
        {
            case GameState.onGame:
                if (GameManager.comboCount > 0)
                    GameManager.countTimeForCombo += Time.deltaTime;
                break;
            default:
                break;
        }
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
        this._trapGenerators.ForEach(tg => tg.StartGenerate());
    }
}
