using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Test", menuName = "Test", order = 2)]
[System.Serializable]
public class TestScriptableObject : ScriptableObject
{
    [SerializeField]
    private int _int = 42;
    [SerializeField]
    private float _float = 25.0f;
    [SerializeField]
    private string _string = "Hello, World!";

    public int GetInt()
    {
        return _int;
    }

    public void SetInt(int inInt)
    {
        _int = inInt;
    }

    public float GetFloat()
    {
        return _float;
    }

    public void SetFloat(float inFloat)
    {
        _float = inFloat;
    }

    public string GetString()
    {
        return _string;
    }

    public void SetString(string inString)
    {
        _string = inString;
    }
}
