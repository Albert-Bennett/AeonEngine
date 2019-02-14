using System;
using System.Collections.Generic;
using System.Linq;

namespace Aeon.Collections
{
    public class ParameterCollection
    {
        List<object> parameters = new List<object>();

        public int Count { get { return parameters.Count; } }
        public List<object> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public ParameterCollection() { }

        public void Add(object obj)
        {
            parameters.Add(obj);
        }

        public bool Contains(object obj)
        {
            return parameters.Contains(obj);
        }

        public void Clear()
        {
            parameters.Clear();
        }
    }
}
