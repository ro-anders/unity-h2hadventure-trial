using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEngine;

public class UnityAdventureView: AdventureView
{
    private const int DRAW_AREA_WIDTH = 320;
    private const int DRAW_AREA_HEIGHT = 256;

    private RenderTexture renderTexture;
    private Texture2D texture;
    private AdventureGame gameEngine;
    private int at = 0;

    public void AdventureSetup(RenderTexture inRenderTexture, Texture2D inTexture) {
        gameEngine = new AdventureGame(this, 2, 0, 0);
        renderTexture = inRenderTexture;
        texture = inTexture;

        // Start with the whole display black.
        RenderTexture.active = renderTexture;
        for (int i = 0; i < renderTexture.width; i++)
            for (int j = 0; j < renderTexture.height; j++)
            {
                texture.SetPixel(i, j, new Color(0, 0, 0));
            }
        texture.Apply();
        RenderTexture.active = null;
    }

    public void AdventureUpdate() {
        RenderTexture.active = renderTexture;
        gameEngine.Adventure_Run();
        texture.Apply();
        RenderTexture.active = null; //don't forget to set it back to null once you finished playing with it. 
   }

    public void DemoUpdate() {
        int viewWidth = renderTexture.width;
        int viewHeight = renderTexture.height;
        // Don't draw anything if the drawing space is not big enough.
        if ((viewWidth >= DRAW_AREA_WIDTH) && (viewHeight >= DRAW_AREA_HEIGHT)) {
            RenderTexture.active = renderTexture;
 
            float stripeWidth = DRAW_AREA_WIDTH * .2f;
            at = (at < DRAW_AREA_WIDTH ? at + 1 : 0);

            int drawXStart = (viewWidth - DRAW_AREA_WIDTH) / 2;
            int drawYStart = (viewHeight - DRAW_AREA_HEIGHT) / 2;

            for (int i = 0; i < DRAW_AREA_WIDTH; i++)
                for (int j = 0; j < DRAW_AREA_HEIGHT; j++)
                {
                    Color color = ((i >= at) && (i <= at + stripeWidth) ? new Color(0xFF/256.0f, 0xD8/256.0f, 0x4C/256.0f) : new Color(0, 0, 0));
                    texture.SetPixel(drawXStart + i, drawYStart + j, color);
                }
            texture.Apply();
            RenderTexture.active = null; //don't forget to set it back to null once you finished playing with it. 
        }
    }

    public void Platform_PaintPixel(int r, int g, int b, int x, int y, int width, int height)
    {
        Color color = new Color(r/256.0f, g/256.0f, b/256.0f);
        for (int i = 0; i < width; ++i)
            for (int j = 0; j < height; ++j) {
                int xi = x + i;
                int yj = y + j;
                if ((xi >= 0) && (xi < DRAW_AREA_WIDTH) && (yj >= 0) && (yj < DRAW_AREA_HEIGHT)) {
                    texture.SetPixel(x + i, y + j, color);
                }
            }
    }

    public void Platform_ReadJoystick(ref bool joyLeft, ref bool joyUp, ref bool joyRight, ref bool joyDown, ref bool joyFire) {
        joyLeft = Input.GetKey(KeyCode.LeftArrow);
        joyUp = Input.GetKey(KeyCode.UpArrow);
        joyRight = Input.GetKey(KeyCode.RightArrow);
        joyDown = Input.GetKey(KeyCode.DownArrow);
        joyFire = Input.GetKey(KeyCode.Space);
    }

    public void Platform_MakeSound(SOUND sound, float volume) {
        //// TBD
    }

}
