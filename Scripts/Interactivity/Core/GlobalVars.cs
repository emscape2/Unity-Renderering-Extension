using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interactivity.ActionComponents
{
    public class GlobalVars
    {
        private Dictionary<string, int> globalVars;
        private static GlobalVars thisObject;
        public static GlobalVars getGlobalVars()
        {
            if (thisObject == null)
            {
                thisObject = new GlobalVars();
                return thisObject;
            }
            else
            {
                return thisObject;
            }
        }

        public void setVar(string var, int value)
        {
            if (globalVars.ContainsKey(var))
                globalVars[var] = value;
            else
                globalVars.Add(var, value);
        }

        public int getVar(string var)
        {
            if (!globalVars.ContainsKey(var))
            {
                globalVars.Add(var, 0);
            }
            return globalVars[var];
        }

        private GlobalVars()
        {
            globalVars = new Dictionary<string, int>();
            CalculateDefaultGLobals();
        }

        private void CalculateDefaultGLobals()
        {
            int width = Mathf.RoundToInt(Screen.width * 25 / Screen.dpi);
            setVar("ScreenW", width);
            int height = Mathf.RoundToInt(Screen.height * 25 / Screen.dpi);
            setVar("ScreenH", height);
            setVar("ScreenDiag", Mathf.RoundToInt(Mathf.Sqrt(height * height + width * width)));
        }
    }
}
