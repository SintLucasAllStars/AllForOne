using System;

public class Utils
{
    public static void Normalized(float f)
    {
        if (f > 1 || f < 0)
        {
            throw new ArgumentException($"Float {f} not normalized!");
        }
    }
}