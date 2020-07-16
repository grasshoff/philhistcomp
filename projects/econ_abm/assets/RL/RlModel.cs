using System;
using System.Collections.Generic;
using System.Text;

namespace Msal.Rl
{
    public interface RlModel<StateId_t, Action_t>
    {
/*
        function rlModelSetState(state:XML):void;
        function rlModelGetState():XML;
*/

        RlModel<StateId_t, Action_t> rlModelClone();
        void rlModelCopy(RlModel<StateId_t, Action_t> source);
        StateId_t rlModelStateGetId();
        bool rlModelStateIsTerminal(RlModel<StateId_t, Action_t> origin, int maxGameLength);
        void rlModelInitES();

        int rlModelGetActorsCount();
        int rlModelGetActorToAct();

        void rlModelActionGetNone(out Action_t action);
        bool rlModelActionIsNone(ref Action_t action);
        int rlModelGetActions(int actorId, out Action_t[] result);
        bool rlModelRandomAction(int actorId, out Action_t action);
        double rlModelApplyAction(int actorId, Action_t action);

        void rlModelEvaluate(int actorId, ref double[] rewards);
        void rlModelEstimate(RlModel<StateId_t, Action_t> origin, ref double[] rewards, string tag);
        string rlModelActionToString(Action_t action);
    }
}
