using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Image image;
    public Text text;
    public string targetString;
    void Start() {
        var tex = QRHelper.GenerateQR(targetString, 512, 512);
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        image.SetNativeSize();
        text.text = QRHelper.FromQR(tex);
    }
}
