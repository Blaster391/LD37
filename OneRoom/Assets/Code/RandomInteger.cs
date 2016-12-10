using System;
using System.Collections;

public static class RandomInteger  {
    private static readonly Random Rnd = new Random();
    public static int Get(int min, int max)
    {
        return Rnd.Next(min, max);
    }
	
}
