using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.GameEvents
{
    internal class NullResponse : GameEventResponse
    {
        public NullResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "No option was available...";
        }

        public override void Execute()
        {
            
        }
    }
}
