using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interactivity.ActionComponents
{

    class SetGlobalVarConsequence : Consequence
    {
        public string VarToSet;
        public int ValueToSet;
        public void Awake()
        {
        }

        public override void Disengage()
        {
            var global = GlobalVars.getGlobalVars();
            global.setVar(VarToSet, ValueToSet);
        }
        public override void Engage()
        {

        }
    }
}
