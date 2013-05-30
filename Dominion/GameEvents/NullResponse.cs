using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.GameEvents
{
    public class NullResponse : GameMessage, IEventResponse
    {
        public NullResponse(IActionScope turnScope) : base(turnScope)
        {
            Description = "No option was available...";
        }

        public void Execute()
        {
            
        }
    }
}
