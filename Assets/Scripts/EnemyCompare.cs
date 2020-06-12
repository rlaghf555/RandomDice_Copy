using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCompare 
{
   public int Compare(Enemy a , Enemy b)
    {
        return a.movedDistance.CompareTo(b.movedDistance);
    }
}
