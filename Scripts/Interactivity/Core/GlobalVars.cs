using System.Collections.Generic;

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
            globalVars = new Dictionary<string,int>();
        }
    }
}
