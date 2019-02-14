using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Pipeline.Models
{
    /// <summary>
    /// Defines a model processor. Some of the code involved in 
    /// this processor came from the normal mapping sample available on 
    /// the XNA creators club websit. A link to where the 
    /// project comes from is given in the references section of my report.
    /// </summary>
    [ContentProcessor(DisplayName = "Basic Pre Pass Model Processor - Aeon Framework")]
    public class MdlProcessor : ModelProcessor
    {
        #region Fields

        protected string filepath;

        string normFilepath;
        string normalMapKey = "NormalMap";

        string specularFilepath;
        string specularMapKey = "SpecularMap";

        static IList<string> acceptableVertexChannelNames = new string[]
        {
            VertexChannelNames.TextureCoordinate(0),
             VertexChannelNames.Normal(0),
             VertexChannelNames.Tangent(0),
             VertexChannelNames.Binormal(0)
        };

        #endregion
        #region Properties

        [DisplayName("Normal Map Filepath")]
        [Description("The file path for the normal map to be applied the model.")]
        [DefaultValue("Aeon/Textures/DefaultNormalMap.tga")]
        public string NormalMapFilepath
        {
            get { return normFilepath; }
            set { normFilepath = value; }
        }

        [DisplayName("Normal Map Key")]
        [Description("The specific name that the processor will search for when trying to find a normal map, if a normal map filepath has not been specified.")]
        [DefaultValue("NormalMap")]
        public string NormalMapKey
        {
            get { return normalMapKey; }
            set { normalMapKey = value; }
        }

        [DisplayName("Specular Map Filepath")]
        [Description("The file path for the specular map to be applied the model.")]
        [DefaultValue("Aeon/Textures/DefaultSpecularMap.png")]
        public string SpecularMapFilepath
        {
            get { return specularFilepath; }
            set { specularFilepath = value; }
        }

        [DisplayName("Specular Map Key")]
        [Description("The specific name that the processor will search for when trying to find a specular map, if a specular map filepath has not been specified.")]
        [DefaultValue("SpecularMap")]
        public string SpecularMapKey
        {
            get { return specularMapKey; }
            set { specularMapKey = value; }
        }

        [Browsable(false)]
        public override bool GenerateTangentFrames
        {
            get { return true; }
        }

        #endregion

        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            if (input == null)
                throw new ArgumentNullException("Inputed model content must not be null.");

            filepath = Path.GetDirectoryName(input.Identity.SourceFilename);

            FindTextures(input);

            return base.Process(input, context);
        }

        protected virtual void FindTextures(NodeContent input)
        {
            MeshContent mesh = input as MeshContent;

            if (mesh != null)
            {
                #region Normal Map

                string normMapPath;

                if (!string.IsNullOrEmpty(NormalMapFilepath))
                    normMapPath = NormalMapFilepath;
                else
                    normMapPath = mesh.OpaqueData.GetValue<string>(NormalMapKey, null);

                if (normMapPath == null)
                {
                    normMapPath = Path.Combine(filepath, mesh.Name + "Norm.tga");

                    if (!File.Exists(normMapPath))
                        normMapPath = "Aeon/Textures/DefaultNormalMap.tga";
                }

                #endregion
                #region Specular Map

                string specMapPath;

                if (!string.IsNullOrEmpty(SpecularMapFilepath))
                    specMapPath = SpecularMapFilepath;
                else
                    specMapPath = mesh.OpaqueData.GetValue<string>(SpecularMapKey, null);

                if (specMapPath == null)
                {
                    specMapPath = Path.Combine(filepath, mesh.Name + "Spec.png");

                    if (!File.Exists(specMapPath))
                        specMapPath = "Aeon/Textures/DefaultSpecularMap.png";
                }

                #endregion

                foreach (GeometryContent geo in mesh.Geometry)
                {
                    if (geo.Material.Textures.ContainsKey(normalMapKey))
                    {
                        ExternalReference<TextureContent> tex = geo.Material.Textures[normalMapKey];
                        geo.Material.Textures.Remove(normalMapKey);
                        geo.Material.Textures.Add("NormalMap", tex);
                    }
                    else
                        geo.Material.Textures.Add("NormalMap", new ExternalReference<TextureContent>(normMapPath));

                    if (geo.Material.Textures.ContainsKey(specularMapKey))
                    {
                        ExternalReference<TextureContent> tex = geo.Material.Textures[specularMapKey];
                        geo.Material.Textures.Remove(specularMapKey);
                        geo.Material.Textures.Add("SpecularMap", tex);
                    }
                    else
                        geo.Material.Textures.Add("SpecularMap", new ExternalReference<TextureContent>(specMapPath));

                }
            }

            foreach (NodeContent child in input.Children)
                FindTextures(child);
        }

        protected override void ProcessVertexChannel(GeometryContent geometry, int vertexChannelIndex, ContentProcessorContext context)
        {
            string channelName = geometry.Vertices.Channels[vertexChannelIndex].Name;

            if (acceptableVertexChannelNames.Contains(channelName))
                base.ProcessVertexChannel(geometry, vertexChannelIndex, context);
            else
                geometry.Vertices.Channels.Remove(channelName);
        }

        protected override MaterialContent ConvertMaterial(MaterialContent material, ContentProcessorContext context)
        {
            EffectMaterialContent lppMat = new EffectMaterialContent();

            lppMat.Effect = new ExternalReference<EffectContent>("Aeon/Shaders/Lpp.fx");

            foreach (KeyValuePair<string, ExternalReference<TextureContent>> tex in material.Textures)
                if ((tex.Key == "Texture") || (tex.Key == "NormalMap") || (tex.Key == "SpecularMap"))
                    lppMat.Textures.Add(tex.Key, tex.Value);

            return context.Convert<MaterialContent, MaterialContent>(lppMat, typeof(MaterialProcessor).Name);
        }
    }
}