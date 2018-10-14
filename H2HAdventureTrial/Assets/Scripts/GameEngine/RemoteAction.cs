using System;
namespace GameEngine
{

    public class RemoteAction
    {
        public int sender;             // The number of the player sending this action (1-3)
        public String typeCode;
        public RemoteAction() {}
        public RemoteAction(String inCode)
        {
            typeCode = inCode;
        }

        public void setSender(int inSender)
        {
            sender = inSender;
        }
    }


    public class MoveAction : RemoteAction
    {
        public int room;               // The room the player was in
        public int posx;               // The x-coordinate of the player in the room
        public int posy;               // The y-coordinate of the player in the room
        public int velx;               // -1 for moving left, 1 for right, and 0 for still or just up/down
        public int vely;               // -1 for down, 1 for up, and 0 for still or just left/right

        public MoveAction(String inCode) :
        base(inCode)
        { }

        public MoveAction(String inCode, int inRoom, int inPosx, int inPosy, int inVelx, int inVely) :
        base(inCode)
        {
            room = inRoom;
            posx = inPosx;
            posy = inPosy;
            velx = inVelx;
            vely = inVely;
        }
    }

    public class PlayerMoveAction : MoveAction
    {
        public static String CODE = "PM";

        public PlayerMoveAction() :
    base(CODE)
        { }

        public PlayerMoveAction(int inRoom, int inPosx, int inPosy, int inVelx, int inVely) :
        base(CODE, inRoom, inPosx, inPosy, inVelx, inVely) {}

    }

    public class PlayerPickupAction : RemoteAction {
        public int pickupObject;
        public int pickupX;
        public int pickupY;
        public int dropObject;
        public int dropRoom;
        public int dropX;
        public int dropY;

        public const String CODE = "PP";

        public PlayerPickupAction():
        base(CODE) {}

        public PlayerPickupAction(int inPickupObject, int inPickupX, int inPickupY, int inDropObject, int inDropRoom, int inDropX, int inDropY):
        base(CODE) {
            pickupObject = inPickupObject;
            pickupX = inPickupX;
            pickupY = inPickupY;
            dropObject = inDropObject;
            dropRoom = inDropRoom;
            dropX = inDropX;
            dropY = inDropY;
        }

        public void setPickup(int inPickupObject, int inPickupX, int inPickupY) {
            pickupObject = inPickupObject;
            pickupX = inPickupX;
            pickupY = inPickupY;
        }

        public void setDrop(int inDropObject, int inDropRoom, int inDropX, int inDropY) {
            dropObject = inDropObject;
            dropRoom = inDropRoom;
            dropX = inDropX;    
            dropY = inDropY;
        }
    };

    public class PlayerResetAction : RemoteAction {

        public static String CODE = "PR";

        public PlayerResetAction() :
        base(CODE)
        { }

    };

    public class PlayerWinAction : RemoteAction {
        public int winInRoom;

        public const String CODE = "PW";

        public PlayerWinAction(): 
        base(CODE) {}

        public PlayerWinAction(int inWinInRoom):
        base(CODE) {
            winInRoom = inWinInRoom;
        }
    };


    public class DragonMoveAction : MoveAction {
        public int dragonNum;          // 0=Rhindle, 1=Yorgle, 2=Grindle
        public int distance;           // Distance from player reporting position

        public const String CODE = "DM";

        public DragonMoveAction() :
        base(CODE){}

        public DragonMoveAction(int inRoom, int inPosx, int inPosy, int inVelx, int inVely,
                                int inDragonNum, int inDistance):
            base(CODE, inRoom, inPosx, inPosy, inVelx, inVely) {
                dragonNum = inDragonNum;
                distance = inDistance;
            }

    };

    public class DragonStateAction : RemoteAction {
        public int dragonNum;          // 0=Rhindle, 1=Yorgle, 2=Grindle
        public int newState;
        public int room;
        public int posx;
        public int posy;
        public int velx;
        public int vely;

        public const String CODE = "DS";

        public DragonStateAction() :
        base(CODE) {}

        public DragonStateAction(int inDragonNum, int inState, int inRoom, int inPosx, int inPosy, int inVelx, int inVely) :
        base(CODE) {
            dragonNum = inDragonNum;
            newState = inState;
            room = inRoom;
            posx = inPosx;
            posy = inPosy;
            velx = inVelx;
            vely = inVely;
        }
    };

    public class PortcullisStateAction : RemoteAction
    {
        public const String CODE = "GS";
        public int portPkey; // The Object::pkey of the portcullis, nothing to do with the key that unlocks it
        public int newState;
        public bool allowsEntry;


        public PortcullisStateAction() :
        base(CODE)
        { }

        public PortcullisStateAction(int inPortPkey, int inNewState, bool inAllowsEntry) :
        base(CODE)
        {
            portPkey = inPortPkey;
            newState = inNewState;
            allowsEntry = inAllowsEntry;
        }
    }

    public class BatMoveAction : MoveAction {
        public int distance;           // Distance from player reporting position

        public const String CODE = "BM";

        public BatMoveAction():
        base(CODE) {}

        public BatMoveAction(int inRoom, int inPosx, int inPosy, int inVelx, int inVely, int inDistance) :
                    base(CODE, inRoom, inPosx, inPosy, inVelx, inVely)
        {
            distance = inDistance;
        }
    };

    public class BatPickupAction : RemoteAction {
        public int pickupObject;
        public int pickupX;
        public int pickupY;
        public int dropObject;
        public int dropRoom;
        public int dropX;
        public int dropY;

        public const String CODE = "BP";

        public BatPickupAction():
        base(CODE) {}

        public BatPickupAction(int inPickupObject, int inPickupX, int inPickupY, int inDropObject, int inDropRoom, int inDropX, int inDropY):
        base(CODE) {
            pickupObject = inPickupObject;
            pickupX = inPickupX;
            pickupY = inPickupY;
            dropObject = inDropObject;
            dropRoom = inDropRoom;
            dropX = inDropX;
            dropY = inDropY;
        }
    };

    /**
     * This places an object.  It is used when an object touches a gate and in level 3 when one client randomly
     * assigns the objects and then communicates the positions to the other clients.  It can be used to move
     * the dragons and bat, too.
     */
    public class ObjectMoveAction : RemoteAction
    {
        public const String CODE = "MO";

        public int objct;
        public int room;
        public int x;
        public int y;


        public ObjectMoveAction() :
        base(CODE)
        { }

        public ObjectMoveAction(int inObject, int inRoom, int inX, int inY) :
        base(CODE)
        {
            objct = inObject;
            room = inRoom;
            x = inX;
            y = inY;
        }
    }

    public class PingAction : RemoteAction
    {
        public const String CODE = "XX";

        PingAction() : base(CODE) { }
    }
}
