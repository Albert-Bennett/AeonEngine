using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Pipeline
{
    /// <summary>
    /// Used to import .ini files.
    /// </summary>
    [ContentImporter(".aini", DisplayName = "Aini Importer - Aeon Framework", DefaultProcessor = "Aini Processor - Aeon Framework")]
    public class AiniImporter : ContentImporter<XmlDocument>
    {
        public override XmlDocument Import(string filename, ContentImporterContext context)
        {
            return SerializationHelper.LoadXmlFile(filename, context);
        }
    }
}
