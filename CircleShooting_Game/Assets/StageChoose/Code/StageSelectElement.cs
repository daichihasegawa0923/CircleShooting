﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectElement : MonoBehaviour
{
    public string _stageName;
    public string _sceneName;
    public Sprite _stageImage;
    public string _descrioption;
    public string _highScoreKey;

    public bool _isLocked = true;
    public string _lockReleaseKeyName;
    public int _lockReleaseScore = 0;
    private void OnEnable()
    {
        if(_isLocked)
        {
            var score = PlayerPrefs.GetInt(this._lockReleaseKeyName,0);
            if (score >= this._lockReleaseScore)
                this._isLocked = false;
        }
    }
}
