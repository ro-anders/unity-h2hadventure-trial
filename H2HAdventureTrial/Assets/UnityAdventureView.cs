using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEngine;

public class UnityAdventureView: MonoBehaviour, AdventureView
{
    public AdventureAudio adv_audio;
    public AdventureDirectional adv_input;
    public RenderTextureDrawer screenRenderer;

    private const int DRAW_AREA_WIDTH = 320;
    private const int DRAW_AREA_HEIGHT = 256;

    private AdventureGame gameEngine;

    private PlayerSync thisPlayer;

    private List<PlayerSync> allPlayers = new List<PlayerSync>();

    // This is only used by the view on the server
    private int lastUsedSlot = 2;

    public void Start()
    {
        Debug.Log("UnityAdventureView started");
    }

    // Update is called once per frame
    void Update()
    {
        if (thisPlayer == null)
        {
            Debug.Log("Trying to update without a local player.");
        }
        else
        {
            AdventureUpdate();
        }
    }

    public void AdventureSetup() {
        if (thisPlayer == null)
        {
            Debug.LogError("Don't have a local player registered yet.");
        }
        gameEngine = new AdventureGame(this, 2, thisPlayer.getSlot(), 1, false, false);
    }

    public void AdventureUpdate() {
        screenRenderer.StartUpdate();
        gameEngine.Adventure_Run();
        screenRenderer.EndUpdate();
    }

    public void registerSync(PlayerSync inPlayerSync, bool isLocal) {
        Debug.Log("Registering " + (isLocal ? "local " : "remote ") + "player # " + (inPlayerSync.getSlot()+1));
        allPlayers.Add(inPlayerSync);
        if (isLocal) {
            thisPlayer = inPlayerSync;
            AdventureSetup();
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

                    screenRenderer.SetPixel(x + i, y + j, color);
                }
            }
    }

    public void Platform_ReadJoystick(ref bool joyLeft, ref bool joyUp, ref bool joyRight, ref bool joyDown, ref bool joyFire) {
        adv_input.getDirection(ref joyLeft, ref joyUp, ref joyRight, ref joyDown);
        joyFire = adv_input.getDropButton();
    }

    public void Platform_MakeSound(SOUND sound, float volume) {
        adv_audio.play(sound, volume);
    }

    public int assignPlayerSlot() {
        int newPlayerSlot = --lastUsedSlot;
        return newPlayerSlot;
    }

}
