using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Pipeline.Scenes
{
    /// <summary>
    /// Used to import scene files.
    /// </summary>
    [ContentImporter(".scn", DisplayName = "Scene Importer - Aeon Framework", DefaultProcessor = "Scene Processor - Aeon Framework")]
    public class ScnImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
