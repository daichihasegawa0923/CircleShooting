using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerPrefsManager : MonoBehaviour
{
    // キーを保存するためのUI
    [SerializeField] private InputField _setKeyNameInputField;
    [SerializeField] private InputField _setValueInputField;
    [SerializeField] private Dropdown _setValueStyleDropdown;

    // 型を指定するためのリスト（読み取り専用）
    private readonly List<string> VALUE_STYLES = new List<string>()
    {
        "int",
        "float",
        "string"
    };

    // PlayerPrefsに保存されている値を、キーから取得する
    [SerializeField] private InputField _getKeyNameInputField;


    // エラー時にコンソールに出力するメッセージ
    private readonly string MESSAGE01_EMPTY_SET_KEY_NAME = "保存するPlayerPrefsのキー名が設定されていません。";
    private readonly string MESSAGE02_EMPTY_VALUE = "保存する値が空です。";
    private readonly string MESSAGE03_EMPTY_GET_KEY_NAME = "取得するするPlayerPrefsのキー名が設定されていません。";

    private void Awake()
    {
        _setValueStyleDropdown.AddOptions(this.VALUE_STYLES);
    }

    public void SetPlayerPrefsValue()
    {
        if (string.IsNullOrWhiteSpace(this._setKeyNameInputField.text))
        {
            Debug.Log(this.MESSAGE01_EMPTY_SET_KEY_NAME);
            return;
        }

        if(string.IsNullOrWhiteSpace(this._setValueInputField.text))
        {
            Debug.Log(this.MESSAGE02_EMPTY_VALUE);
        }

        var selectedItem = this._setValueStyleDropdown.options[this._setValueStyleDropdown.value].text;
        if (selectedItem == "int")
            PlayerPrefs.SetInt(this._setKeyNameInputField.text, int.Parse(this._setValueInputField.text));
        if (selectedItem == "float")
            PlayerPrefs.SetFloat(this._setKeyNameInputField.text, float.Parse(this._setValueInputField.text));
        if (selectedItem == "string")
            PlayerPrefs.SetString(this._setKeyNameInputField.text, this._setValueInputField.text);

        Debug.Log(this._setKeyNameInputField.text + "を" + selectedItem + "型で" + this._setValueInputField.text+"という値を保存しました。");
    }

    public void GetPlayerPrefsValue()
    {
        if(string.IsNullOrWhiteSpace(this._getKeyNameInputField.text))
        {
            Debug.Log(this.MESSAGE01_EMPTY_SET_KEY_NAME);
        }

        var intValue = PlayerPrefs.GetInt(this._getKeyNameInputField.text);
        var floatValue = PlayerPrefs.GetFloat(this._getKeyNameInputField.text);
        var stringValue = PlayerPrefs.GetString(this._getKeyNameInputField.text, "no value");

        Debug.Log(this._getKeyNameInputField.text + ":int:" + intValue);
        Debug.Log(this._getKeyNameInputField.text + ":float:" + floatValue);
        Debug.Log(this._getKeyNameInputField.text + ":string:" + stringValue);
    }
}
