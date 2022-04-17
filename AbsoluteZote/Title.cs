using System.Collections.Generic;
using UnityEngine;
using Modding;
using Satchel;


namespace AbsoluteZote
{
    public class Title
    {
        private readonly Mod mod_;
        public readonly Dictionary<string, GameObject> prefabs = new();
        public Title(Mod mod)
        {
            mod_ = mod;
        }
        private void Log(object message) => mod_.Log(message);
        public List<(string, string)> GetPreloadNames()
        {
            return new List<(string, string)>
            {
                 ("GG_Radiance", "Boss Control"),
                 ("GG_Grey_Prince_Zote", "Mighty_Zote_0005_17"),
            };
        }
        public void LoadPrefabs(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            var bossControl = preloadedObjects["GG_Radiance"]["Boss Control"];
            var title = bossControl.transform.Find("Boss Title").gameObject;
            title.name = "title";
            prefabs["title"] = title;
            var background = preloadedObjects["GG_Grey_Prince_Zote"]["Mighty_Zote_0005_17"];
            var whiteFader = bossControl.transform.Find("White Fader").gameObject;
            background.GetComponent<UnityEngine.SpriteRenderer>().sprite = whiteFader.GetComponent<UnityEngine.SpriteRenderer>().sprite;
            background.transform.position = new Vector3(0, 0, -16);
            background.transform.localScale = Vector3.one * 256;
            background.name = "background";
            prefabs["background"] = background;
        }
        public void UpgradeFSM(PlayMakerFSM fsm)
        {
            if (fsm.gameObject.scene.name == "GG_Grey_Prince_Zote" && fsm.gameObject.name == "Grey Prince Title" && fsm.FsmName == "Control")
            {
                for (int i = 1; i <= 13; ++i)
                {
                    fsm.RemoveAction("Extra " + i.ToString(), 1);
                }
                fsm.RemoveAction("Main Title", 0);
            }
        }
    }
}