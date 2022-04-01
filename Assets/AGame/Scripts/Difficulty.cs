using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
    private static Diff difficulty = Diff.EASY;

    public enum Diff
    {
        EASY = 3,
        NORMAL = 5,
        HARD = 8
    }

    public static void SetDifficulty(Diff d)
    {
        difficulty = d;
    }

    public static int GetDiffiulty()
    {
        return (int)difficulty;
    }

    public static int GetArrSize(int lvl)
    {
        if (lvl < (int)difficulty)
            return (int)difficulty;
        else
            return (int)difficulty + (lvl / (int)difficulty);
    }
    
}
