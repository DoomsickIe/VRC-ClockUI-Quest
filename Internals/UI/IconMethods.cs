using System;
using UnityEngine;

namespace ClockUI.Internals.UI;

internal class IconMethods
{
    //ty cyril-xd
    internal static Texture2D CreateTexture(string base64)
    {
        return CreateTexture(Convert.FromBase64String(base64));
    }
    internal static Texture2D CreateTexture(byte[] data)
    {
        Texture2D texture2D = new(2, 2);
        ImageConversion.LoadImage(texture2D, data);
        texture2D.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        return texture2D;
    }
    internal static Sprite Base64toSprite(string base64)
    {
        Texture2D texture2D = CreateTexture(base64);
        Rect rect = new Rect(0f, 0f, texture2D.width, texture2D.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Vector4 border = Vector4.zero;
        Sprite sprite = Sprite.CreateSprite_Injected(texture2D, ref rect, ref pivot, 100f, 0u, SpriteMeshType.Tight, ref border, generateFallbackPhysicsShape: false);
        sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
        return sprite;
    }
}
