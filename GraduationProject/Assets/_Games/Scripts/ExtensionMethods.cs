using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Color LerpRGB(Color a, Color b, float t)
    {
        return new Color
        (
            a.r + (b.r - a.r) * t,
            a.g + (b.g - a.g) * t,
            a.b + (b.b - a.b) * t,
            a.a + (b.a - a.a) * t
        );
    }

    public static Color LerpHSV(ColorHSV a, ColorHSV b, float t)
    {
        // Hue interpolation
        float h;
        float d = b.h - a.h;
        if (a.h > b.h)
        {
            // Swap (a.h, b.h)
            var h3 = b.h;
            b.h = a.h;
            a.h = h3;

            d = -d;
            t = 1 - t;
        }

        if (d > 0.5) // 180deg
        {
            a.h = a.h + 1; // 360deg
            h = (a.h + t * (b.h - a.h)) % 1; // 360deg
        }
        if (d <= 0.5) // 180deg
        {
            h = a.h + t * d;

        }

        // Interpolates the rest
        return new ColorHSV
        (
            a.h,          // H
            a.s + t * (b.s - a.s),  // S
            a.v + t * (b.v - a.v),  // V
            a.a + t * (b.a - a.a)   // A
        );
    }
}