using System;
namespace GameEngine
{
    public abstract class Sync
    {
        public abstract void BroadcastAction(RemoteAction action);

    }

    //// TEMP - NO-OP Implementation
    public class NoopSync: Sync {
        public override void BroadcastAction(RemoteAction action) {
            
        }
    }
}
