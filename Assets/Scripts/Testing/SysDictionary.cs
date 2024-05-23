using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[Serializable]
public class SysDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    public delegate void ElementAddedEventHandler(TKey key, TValue value);
    public event ElementAddedEventHandler OnElementAdded;

    [Serializable]
    public class Pair
    {
        public TKey key = default;
        public TValue value = default;

        public Pair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [SerializeField]
    private List<Pair> _list = null;

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();
        foreach (Pair pair in _list)
        {
            if (ContainsKey(pair.key))
            {
                continue;
            }
            Add(pair.key, pair.value);
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        // 処理なし
    }

    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        OnElementAdded?.Invoke(key, value);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SysDictionary<,>))]
public class SysDictionaryPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // ここでカスタムGUIを描画します。
        // 例えば、以下のコードはデフォルトのGUIを描画します。
        EditorGUI.PropertyField(position, property, label, true);
    }
}
#endif

