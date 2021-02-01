using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private EnemyAnimator enemyAnimator => transform.parent.GetComponent<EnemyAnimator>();
    public GameObject hitbox;
    public BoxCollider collider => hitbox.GetComponent<BoxCollider>();

    public void AttackDone()
    {
        enemyAnimator.AttackDone();
    }

    public void HitstunDone()
    {
        enemyAnimator.HitstunDone();
    }

    public void ColliderOnOff()
    {
       collider.enabled = !collider.enabled;
    }

    public void PlaySlamSFX()
    {
        enemyAnimator.PlaySlamSFX();
    }
}
