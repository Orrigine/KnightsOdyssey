using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlackboardKeyValueBase
{
	[SerializeField]
	private string _key;
    private System.Type _valueType;

    public string Key => _key;
    public System.Type ValueType => _valueType;

    public BlackboardKeyValueBase(string key, System.Type valueType)
    {
		_key = key;
        _valueType = valueType;
	}
}

[Serializable]
public class BlackboardKeyValue<T> : BlackboardKeyValueBase
{
    [SerializeField]
    private T _value;

    public T Value { get => _value; set => _value = value; }

	public BlackboardKeyValue(string key) : base(key, typeof(T))
	{
        _value = default(T);
	}
}
