using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Diplom
{
    public class ContentLoader
    {
        private ContentBuilder _contentBuilder = new ContentBuilder();
        private ContentManager _contentManager;

        private Dictionary<string, Model> _loadedModels = new Dictionary<string, Model>();
        private Dictionary<string, Effect> _loadedEffects = new Dictionary<string, Effect>();
        private Dictionary<string, Texture2D> _loadedTextures = new Dictionary<string, Texture2D>();

        public ContentLoader(IServiceProvider serviceProvider)
        {
            _contentManager = new ContentManager(serviceProvider, _contentBuilder.OutputDirectory);
        }

        private string GetContentPath(string fileName)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content/", fileName);
            return Path.GetFullPath(relativePath);
        }

        public Model LoadModel(string fileName, string modelName)
        {
            if (_loadedModels.ContainsKey(modelName))
                return _loadedModels[modelName];

            _contentBuilder.Add(GetContentPath(fileName), modelName, null, "ModelProcessor");

            // Build this new model data.
            string buildError = _contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                Model model = _contentManager.Load<Model>(modelName);
                _loadedModels.Add(modelName, model);

                return model;
            }
            else
            {
                throw new Exception(buildError);
            }
        }
        public Effect LoadEffect(string fileName, string effectName)
        {
            if (_loadedEffects.ContainsKey(effectName))
                return _loadedEffects[effectName];

            _contentBuilder.Add(GetContentPath(fileName), effectName, null, "EffectProcessor");

            // Build this new model data.
            string buildError = _contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                Effect ef = _contentManager.Load<Effect>(effectName);
                _loadedEffects.Add(effectName, ef);

                return ef;
            }
            else
            {
                throw new Exception(buildError);
            }
        }
        public Texture2D LoadTexture(string fileName, string textureName)
        {
            if (_loadedTextures.ContainsKey(textureName))
                return _loadedTextures[textureName];

            _contentBuilder.Add(GetContentPath(fileName), textureName, null, "TextureProcessor");

            // Build this new model data.
            string buildError = _contentBuilder.Build();

            if (string.IsNullOrEmpty(buildError))
            {
                Texture2D texture = _contentManager.Load<Texture2D>(textureName);
                _loadedTextures.Add(textureName, texture);

                return texture;
            }
            else
            {
                throw new Exception(buildError);
            }
        }

        public Model GetLoadedModel(string modelName)
        {
            return _loadedModels[modelName];
        }
        public Effect GetLoadedEffect(string effectName)
        {
            return _loadedEffects[effectName];
        }
        public Texture2D GetLoadedTexture(string textureName)
        {
            return _loadedTextures[textureName];
        }
    }
}
