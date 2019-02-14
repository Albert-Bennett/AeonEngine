using Aeon.Collections;
using System.Collections.Generic;

namespace Aeon.SceneManagement
{
    public class SceneInfo
    {
        public AeonDictionary<string, ParameterCollection> Objects =
            new AeonDictionary<string, ParameterCollection>();
    }
}
