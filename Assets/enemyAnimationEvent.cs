using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimationEvent : MonoBehaviour
{
    private EnemyAnimator enemyAnimator => transform.parent.GetComponent<EnemyAnimator>();

    public void AttackDone()
    {
        enemyAnimator.AttackDone();
    }

    public void HitstunDone()
    {
        enemyAnimator.HitstunDone();
    }
}
