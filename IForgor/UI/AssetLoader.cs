using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace IForgor
{
    internal class AssetLoader
    {
        public Sprite spr_bloq { get; }
        public Sprite spr_arrow { get; }
        public Sprite spr_dot { get; }
        public Sprite spr_cut_arrow { get; }
        public Sprite spr_saber_bg { get; }
        public Sprite spr_saber_fg { get; }

        public Material mat_UINoGlow { get; }

        public AssetLoader()
        {
            spr_bloq = LoadSpriteFromResource("IForgor.Resources.bloq.png");
            spr_arrow = LoadSpriteFromResource("IForgor.Resources.arrow.png");
            spr_dot = LoadSpriteFromResource("IForgor.Resources.dot.png");
            spr_cut_arrow = LoadSpriteFromResource("IForgor.Resources.cut_arrow.png");
            spr_saber_bg = LoadSpriteFromResource("IForgor.Resources.saber_bg.png");
            spr_saber_fg = LoadSpriteFromResource("IForgor.Resources.saber_fg.png");

            mat_UINoGlow = new Material(Resources.FindObjectsOfTypeAll<Material>().Where(m => m.name == "UINoGlow").First())
            {
                name = "UINoGlowEvenMoreCustomThanBSMLLOLnojk"
            };
        }

        private static Sprite LoadSpriteFromResource(string path)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(path);
            if (stream != null)
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                Texture2D tex = new Texture2D(2, 2);
                if (tex.LoadImage(data))
                {
                    Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100);
                    return sprite;
                }
            }

            return null;
        }
    }
}