// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEnemySubtree : ScriptableObject
{
    public virtual BTSequence BuildSequenceSubtree(EnemyAIContext context)
    {
        return null;
    }

    public virtual BTSelector BuildSelectorSubtree(EnemyAIContext context)
    {
        return null;
    }
}
