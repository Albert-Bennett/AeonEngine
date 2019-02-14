using Aeon.SceneManagement;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Pipeline.Scenes
{
    /// <summary>
    /// Used to process a levels file.
    /// </summary>
    [ContentProcessor(DisplayName = "Scenes Processor - Aeon Framework")]
    public class ScenesProcessor : ContentProcessor<XmlDocument, ScenesInfo>
    {
        public override ScenesInfo Process(XmlDocument input, ContentProcessorContext context)
        {
            ScenesInfo output = new ScenesInfo();

            if (input.GetElementsByTagName("Info").Count > 0)
            {
                int count = 0;

                foreach (XmlNode node in input.GetElementsByTagName("Info")[0].ChildNodes)
                    switch (node.Name)
                    {
                        case "AssemblyRef":
                            output.AssemblyRef = node.InnerText;
                            break;

                        case "Count":
                            int.TryParse(node.InnerText, out count);
                            break;
                    }

                if (count > 0)
                    for (int i = 0; i < count; i++)
                    {
                        string text = "";

                        foreach (XmlNode node in input.GetElementsByTagName("Scene" + i)[0].ChildNodes)
                            switch (node.Name)
                            {
                                case "Value":
                                    text = node.InnerText;
                                    break;
                            }

                        output.SceneFilepaths.Add(text);
                    }
            }

            return output;
        }
    }
}