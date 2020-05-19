using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField] protected List<TrapBase> _trapBases;
    [SerializeField] protected float _frequency = 5.0f;

    public void StartGenerate()
    {
        StartCoroutine("Generate");
    }

    protected IEnumerator Generate()
    {
        while (true)
        {
            var trap = (GameObject)Instantiate(_trapBases[_trapBases.Count - 1].gameObject);
            trap.transform.position = transform.position;
            Destroy(trap, 60.0f);
            yield return new WaitForSeconds(this._frequency);
        }
    }
}
