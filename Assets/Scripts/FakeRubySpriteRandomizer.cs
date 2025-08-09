using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeRubySpriteRandomizer : MonoBehaviour
{
    public Image image;
    public GameController gc;

    void Start()
    {
        
    }
    void Update()
    {
        image.sprite = gc.fakeRubyTextures[gc.fakeRubyRandomisedTexture];
    }
}
