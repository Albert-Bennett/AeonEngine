using System.Collections.Generic;

namespace Aeon.Collections
{
    /// <summary>
    /// Defines a doctionary where their can be multiples of the key in it.
    /// </summary>
    /// <typeparam name="KeyType">The type of value that every key will hold.</typeparam>
    /// <typeparam name="ValueType">The type of value every value will have.</typeparam>
    public class AeonDictionary<KeyType, ValueType>
    {
        List<KeyType> keys = new List<KeyType>();
        List<ValueType> values = new List<ValueType>();

        public List<KeyType> Keys { get { return keys; } }
        public List<ValueType> Values { get { return values; } }

        public int Count { get { return keys.Count; } }

        public void Add(KeyType key, ValueType value)
        {
            keys.Add(key);
            values.Add(value);
        }

        public void Add(AeonKeyValuePair<KeyType, ValueType> kvp)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }

        public AeonKeyValuePair<KeyType, ValueType> GetValueByIndex(int index)
        {
            if (index < keys.Count)
                return new AeonKeyValuePair<KeyType, ValueType>()
                {
                     Key= keys[index],
                     Value = values[index]
                };

            throw new System.ArgumentOutOfRangeException("The given index: " + index +
                " is out of the range of the collection.");
        }

        public ValueType GetRemove(KeyType key)
        {
            object obj = null;
            ValueType val = (ValueType)obj;

            bool removed = false;

            if (Contains(key))
                for (int i = 0; i < Count; i++)
                    if (!removed)
                        if (keys[i].Equals(key))
                        {
                            removed = true;
                            keys.Remove(keys[i]);

                            val = values[i];
                            values.Remove(values[i]);
                        }

            return val;
        }

        public void Remove(KeyType key)
        {
            bool removed = false;

            if (Contains(key))
                for (int i = 0; i < Count; i++)
                    if (!removed)
                        if (keys[i].Equals(key))
                        {
                            removed = true;
                            keys.Remove(keys[i]);
                            values.Remove(values[i]);
                        }
        }

        public bool Contains(KeyType key)
        {
            for (int i = 0; i < Count; i++)
                if (keys[i].Equals(key))
                    return true;

            return false;
        }

        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }
    }
}
