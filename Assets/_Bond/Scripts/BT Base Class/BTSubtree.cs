using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSubtree : ScriptableObject
{
    public virtual BTSequence BuildSequenceSubtree(CreatureAIContext context)
    {
        return null;
    }

    public virtual BTSelector BuildSelectorSubtree(CreatureAIContext context)
    {
        return null;
    }
}
