using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected List<TrapGenerator> _trapGenerators;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
