// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public BTNode behaviorTree;
    private EnemyAIContext context;
    public EnemyType currentEnemyType;
    public EnemyType DefaultEnemyType;

    public bool Evaluate = false;

    private void Start()
    {
        context = GetComponent<EnemyAIContext>();
        BuildBT();
    }

    private void Awake()
    {
        context = GetComponent<EnemyAIContext>();
    }

    private void Update()
    {
        if(Evaluate)
        {
            behaviorTree.OnParentEvaluate();
            context.animator.Move(context.agent.velocity);
        }
    }

    //build the behavior tree for the creature
    public void BuildBT() 
    {
        #region IDLE
            BTSequence idleSequence = null;
            if(currentEnemyType == null || currentEnemyType.idle == null)
            {
                idleSequence = DefaultEnemyType.idle.BuildSequenceSubtree(context);
            } else 
            {
                idleSequence = currentEnemyType.idle.BuildSequenceSubtree(context);
            }
        #endregion

        #region ATTACK
            BTSequence attackSequence = null;
            if(currentEnemyType == null || currentEnemyType.attack == null)
            {
                attackSequence = DefaultEnemyType.attack.BuildSequenceSubtree(context);
            } else 
            {
                attackSequence = currentEnemyType.attack.BuildSequenceSubtree(context);
            }
        #endregion

        #region PLAYER NOTICED
            List<BTNode> playerNoticedList = new List<BTNode>();
            ECheckPlayerInRange playerNoticed = new ECheckPlayerInRange("PlayerNoticed", context);
            playerNoticedList.Add(playerNoticed);
            playerNoticedList.Add(attackSequence);
            BTSequence playerNoticedSequence = new BTSequence("Is player noticed", playerNoticedList);
        #endregion

        #region PLAYER NOTICED FIRST TIME
            List<BTNode> firstNoticedList = new List<BTNode>();
            EActionPlayAwakeAnim playAwakeAnim = new EActionPlayAwakeAnim("Play Awake Anim", context); //Anim when seeing player for first time
            BTSucceeder succeeder = new BTSucceeder("Anim Succeeder", playAwakeAnim); //Always make anim succeed
            ECheckPlayerNoticedBefore playerNoticedBefore = new ECheckPlayerNoticedBefore("Player Noticed Before", context);
            firstNoticedList.Add(playerNoticedBefore);
            firstNoticedList.Add(succeeder);
            BTSequence firstNoticeSequence = new BTSequence("First Notice Sequence", firstNoticedList);
        #endregion

        #region HITSTUN
                List<BTNode> hitstunList = new List<BTNode>();
                ECheckDamageTaken checkDamageTaken = new ECheckDamageTaken("Check if damage taken", context);
                EActionPlayHitstunAnim playHitstunAnim = new EActionPlayHitstunAnim("Play Hitstun Anim", context);
                hitstunList.Add(checkDamageTaken);
                hitstunList.Add(playHitstunAnim);
                BTSequence hitstunSequence = new BTSequence("Hitstun Sequence", hitstunList);
        #endregion

        #region DEATH CHECK
            List<BTNode> deathList = new List<BTNode>();
            ECheckIfDead checkIfDead = new ECheckIfDead("Check if dead", context);
            EActionPlayDeathAnim playDeathAnim = new EActionPlayDeathAnim("Play Death Anim", context);
            deathList.Add(checkIfDead);
            deathList.Add(playDeathAnim);

            BTSequence deathSequence  = new BTSequence("Death Sequence", deathList);
        #endregion

        #region ROOT
            List<BTNode> RootList = new List<BTNode>();
            RootList.Add(deathSequence);
            RootList.Add(hitstunSequence);
            RootList.Add(firstNoticeSequence);
            RootList.Add(playerNoticedSequence);
            RootList.Add(idleSequence);

        BTSelector _root = new BTSelector("Root", RootList);
        #endregion
        behaviorTree = _root;
        Evaluate = true;
    }
}
