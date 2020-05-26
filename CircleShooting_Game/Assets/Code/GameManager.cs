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

    public void StartGenerateTrap()
    {
        this._trapGenerators.ForEach(tg => tg.StartGenerate());
    }
}
