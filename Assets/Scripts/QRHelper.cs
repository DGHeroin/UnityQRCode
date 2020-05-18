using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public static class QRHelper {
    private static Color32[] Encode(string textForEncoding,
     int width, int height) {
        var writer = new BarcodeWriter {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions {
                Height = height,
                Width = width
            }
        };

        Debug.Log($"{width} : {height}");
        return writer.Write(textForEncoding);
    }
    private static Texture2D Resize(Texture2D texture2D, int targetX, int targetY) {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
    public static Texture2D GenerateQR(string text, int width=256, int height=256) {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        if (width == 256 && height == 256) {
            return encoded;
        }
        return Resize(encoded, width, height);
    }

    public static string FromQR(Texture2D tex) {
        try {
            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(tex.GetPixels32(), tex.width, tex.height);
            if (result != null) {
                return result.Text;
            }
        } catch(Exception e) {
            Debug.LogError(e);
        }
        return null;
    }
}
