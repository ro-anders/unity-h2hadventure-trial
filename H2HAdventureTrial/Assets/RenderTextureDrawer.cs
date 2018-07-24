using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEngine;

public class RenderTextureDrawer : MonoBehaviour
{
    public RenderTexture renderTexture; // renderTextuer that you will be rendering stuff on
    public Renderer quadRenderer; // renderer in which you will apply changed texture
    Texture2D texture;
    UnityAdventureView view;

    void Start()
    {

        texture = new Texture2D(renderTexture.width, renderTexture.height);
        quadRenderer.material.mainTexture = texture;
        view = new UnityAdventureView();
        view.AdventureSetup(renderTexture, texture);
    }

    // Update is called once per frame
    void Update()
    {
        view.AdventureUpdate();
    }
}
