using Aeon.Engine;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace Pipeline
{
    /// <summary>
    /// Used to process .ini files.
    /// </summary>
    [ContentProcessor(DisplayName = "Aini Processor - Aeon Framework")]
    public class AiniProcessor : ContentProcessor<XmlDocument, FrameworkCreation>
    {
        XmlDocument input;

        public override FrameworkCreation Process(XmlDocument input, ContentProcessorContext context)
        {
            FrameworkCreation output = new FrameworkCreation();
            this.input = input;

            if (input.GetElementsByTagName("Setup").Count > 0)
            {
                if (input.GetElementsByTagName("Assemblies").Count > 0)
                {
                    int assemblies = 0;

                    GetNodeInt("AssemblyCount", out assemblies);

                    if (assemblies > 0)
                    {
                        output.AssemblyRefferences = new string[assemblies];

                        for (int i = 0; i < assemblies; i++)
                        {
                            string s = "";

                            GetNodeString("Assembly" + i, out s);

                            output.AssemblyRefferences[i] = s;
                        }
                    }
                }

                if (input.GetElementsByTagName("Components").Count > 0)
                {
                    int components = 0;

                    GetNodeInt("ComponentCount", out components);

                    if (components > 0)
                    {
                        output.EngineComponents = new string[components];

                        for (int i = 0; i < components; i++)
                        {
                            string comp = "";

                            GetNodeString("Comp" + i, out comp);

                            output.EngineComponents[i] = comp;
                        }
                    }
                }
            }

            return output;
        }

        void GetNodeInt(string nodeKey, out int value)
        {
            value = 0;

            foreach (XmlNode node in input.GetElementsByTagName(nodeKey)[0].ChildNodes)
                switch (node.Name)
                {
                    case "Value":
                        int.TryParse(node.InnerText, out value);
                        break;
                }
        }

        void GetNodeString(string nodeKey, out string value)
        {
            value = "";

            foreach (XmlNode node in input.GetElementsByTagName(nodeKey)[0].ChildNodes)
                switch (node.Name)
                {
                    case "Value":
                        value = node.InnerText;
                        break;
                }
        }
    }
}