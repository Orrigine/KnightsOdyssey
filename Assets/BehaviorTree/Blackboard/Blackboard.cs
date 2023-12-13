using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class Blackboard
{
    [SerializeReference]
    private List<BlackboardKeyValueBase> _keyValues;

    public void AddKeyValue<T>(string key)
    {
        BlackboardKeyValue<T> b = new BlackboardKeyValue<T>(key);
		_keyValues.Add(b);
    }

    public BlackboardKeyValueBase Find(string key)
    {
        return _keyValues.Find((p) =>
            {
                return p.Key.Equals(key);
            }
        );
    }

    public BlackboardKeyValue<T> Find<T>(string key)
    {
        BlackboardKeyValueBase b = Find(key);

        if (b == null) return null;

        if (b.ValueType != typeof(T)) return null;

        return (BlackboardKeyValue<T>) b;
    }

    public T GetValue<T>(string key)
    {
        BlackboardKeyValue<T> b = Find<T>(key);
        if (b != null)
        {
            return b.Value;
        }
        return default(T);
    }

    public void SetValue<T>(string key, T value)
    {
        BlackboardKeyValue<T> b = Find<T>(key);
        if (b != null)
        {
            b.Value = value;
        }
    }
}
