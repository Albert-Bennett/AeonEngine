using Microsoft.Xna.Framework.Content;

namespace Aeon.Engine
{
    public class FrameworkCreation
    {
        [ContentSerializer(FlattenContent = true, CollectionItemName = "Component")]
        public string[] EngineComponents { get; set; }

        [ContentSerializer(FlattenContent=true, CollectionItemName="Assembly")]
        public string[] AssemblyRefferences { get; set; }
    }
}
