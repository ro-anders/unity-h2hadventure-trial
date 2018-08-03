using System;
namespace GameEngine
{
  
    public enum SOUND
    {
        WON = 0,
        ROAR,
        EATEN,
        DRAGONDIE,
        PUTDOWN,
        PICKUP,
        SOUND_GLOW
    }

    public static class MAX {
        public const float VOLUME = 11.0f;
    }

    public interface AdventureView
    {
        void Platform_PaintPixel(int r, int g, int b, int x, int y, int width, int height);

        void Platform_ReadJoystick(ref bool joyLeft, ref bool joyUp, ref bool joyRight, ref bool joyDown, ref bool joyFire);

        void Platform_MakeSound(SOUND sound, float volume);

    }
}
