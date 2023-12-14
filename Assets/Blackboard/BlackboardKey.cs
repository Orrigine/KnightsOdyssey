using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BlackboardKeyValueBase
{
	[SerializeField]
	private string _key;
    private System.Type _valueType;

    public string Key => _key;
    public System.Type ValueType => _valueType;

    protected BlackboardKeyValueBase(string key, System.Type valueType)
    {
		_key = key;
        _valueType = valueType;
	}

	public abstract void CopyFrom(BlackboardKeyValueBase b);
}

[Serializable]
public class BlackboardKeyValue<T> : BlackboardKeyValueBase
{
	private T _value;
	public T Value { get => _value; set => _value = value; }

	public BlackboardKeyValue(string key) : base(key, typeof(T))
	{
		_value = default(T);
	}

	public override void CopyFrom(BlackboardKeyValueBase b)
	{
		if (ValueType == b.ValueType)
		{
			BlackboardKeyValue<T> o = b as BlackboardKeyValue<T>;
			_value = o.Value;
		}
	}

	public override bool Equals(object obj)
	{
		BlackboardKeyValueBase o = obj as BlackboardKeyValueBase;

		return Key.Equals(o.Key) && ValueType.Equals(o.ValueType) && Value.Equals(((BlackboardKeyValue<T>) o).Value);
	}
}
