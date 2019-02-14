using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Pipeline.Scenes
{
    /// <summary>
    /// Used to process .lvls files.
    /// </summary>
    [ContentImporter(".scns", DisplayName = "Scenes Importer - Aeon Framework", DefaultProcessor = "Scenes Processor - Aeon Framework")]
    public class ScenesImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
