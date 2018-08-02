using System;
namespace GameEngine
{
    public interface AdventureView
    {
        void Platform_PaintPixel(int r, int g, int b, int x, int y, int width, int height);

        void Platform_ReadJoystick(ref bool joyLeft, ref bool joyUp, ref bool joyRight, ref bool joyDown, ref bool joyFire);
    }
}
