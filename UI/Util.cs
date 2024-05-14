
//잡다한 유틸.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public static class Util
{
    public static string ClassifyTier(int total, int rank)
    {
        string tier = null;
        if (rank == 0)
            tier = null;
        else if (rank / (float)total > 0.75)
            tier = "icon_league_dirt_0";
        else if (rank / (float)total > 0.5)
            tier = "icon_league_bronze_0";
        else if (rank / (float)total > 0.25)
            tier = "icon_league_silver_0";
        else if (rank / (float)total > 0.1)
            tier = "icon_league_gold_0";
        else
            tier = "icon_league_diamond_0";
        return tier;
    }
}
