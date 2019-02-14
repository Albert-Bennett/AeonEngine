using System;

namespace Aeon.Collections
{
    /// <summary>
    /// Used to define a Key value pair in a AeonDictionary.
    /// </summary>
    /// <typeparam name="KeyType">The type of value the key of this AeonKeyValuePair will be.</typeparam>
    /// <typeparam name="ValueType">The type of value the value of this AeonKeyValuePair will be.</typeparam>
    [Serializable]
    public struct AeonKeyValuePair<KeyType, ValueType>
    {
        public KeyType Key;
        public ValueType Value;
    }
}
