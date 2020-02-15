using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static float Map(this float value, float from1, float from2, float to1, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
