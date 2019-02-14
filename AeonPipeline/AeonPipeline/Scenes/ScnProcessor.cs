using Aeon.Collections;
using Aeon.SceneManagement;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Pipeline.Scenes
{
    /// <summary>
    /// Used to process scenes.
    /// </summary>
    [ContentProcessor(DisplayName = "Scene Processor - Aeon Framework")]
    public class ScnProcessor : ContentProcessor<XmlDocument, SceneInfo>
    {
        public override SceneInfo Process(XmlDocument input, ContentProcessorContext context)
        {
            SceneInfo output = new SceneInfo();

            if (input.GetElementsByTagName("Scene").Count > 0)
            {
                //The amount of objects to be added.
                int count = 0;

                foreach (XmlNode node in input.GetElementsByTagName("Scene")[0].ChildNodes)
                    switch (node.Name)
                    {
                        case "Count":
                            int.TryParse(node.InnerText, out  count);
                            break;
                    }

                for (int i = 0; i < count; i++)
                {
                    int paramsCount = 0;
                    string type = "";

                    foreach (XmlNode node in input.GetElementsByTagName("Object" + i)[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "Parameters":
                                int.TryParse(node.InnerText, out paramsCount);
                                break;

                            case "Type":
                                type = node.InnerText;
                                break;
                        }

                    ParameterCollection parameters = new ParameterCollection();

                    for (int j = 0; j < paramsCount; j++)
                    {
                        object obj = null;

                        foreach (XmlNode node in input.GetElementsByTagName("Obj" + i + "Param" + j)[0].ChildNodes)
                            switch (node.Name)
                            {
                                case "Value":
                                    SerializationHelper.GetTypeValue(node.InnerText, out obj);
                                    break;
                            }

                        parameters.Add(obj);
                    }

                    output.Objects.Add(type, parameters);
                }
            }

            return output;
        }
    }
}