using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    public BTNode behaviorTree;
    private CreatureAIContext context;
    public List<Personality> personalities = new List<Personality>();
    public Personality DefaultPersonality;

    public bool Evaluate = false;

    private void Start()
    {
        context = GetComponent<CreatureAIContext>();
        //BuildBT();
    }

    private void Awake() {
        context = GetComponent<CreatureAIContext>();
    }

    private void Update() {
        if(Evaluate){
            behaviorTree.OnParentEvaluate();
            context.animator.Move(context.agent.velocity);
        }
    }

    //build the behavior tree for the creature
    public void BuildBT() 
    {
        personalities = context.CD.personalities;
        List<BTNode> RootList = new List<BTNode>();

        #region BONDED FOLLOW PLAYER
            BTSelector FollowPlayer = null;
            foreach( Personality p in personalities){
                if(p.FollowPlayerTree != null){
                    FollowPlayer = p.FollowPlayerTree.BuildSelectorSubtree(context);
                }  
            }
            if(FollowPlayer == null){
                FollowPlayer = DefaultPersonality.FollowPlayerTree.BuildSelectorSubtree(context);
            }
        #endregion

        #region CREATURE ABILITIES
            BTSequence Ability = null;
            foreach( Personality p in personalities){
                if(p.AbilityTree != null){
                    Ability = p.AbilityTree.BuildSequenceSubtree(context);
                }  
            }
            if(Ability == null){
                Ability = DefaultPersonality.AbilityTree.BuildSequenceSubtree(context);
            }
        #endregion


        #region WILD PLAYER
            BTSequence wildNoticed = null;
            foreach( Personality p in personalities){
                if(p.WildNoticed != null){
                    wildNoticed = p.WildNoticed.BuildSequenceSubtree(context);
                }  
            }
            if(wildNoticed == null){
                wildNoticed = DefaultPersonality.WildNoticed.BuildSequenceSubtree(context);
            }
        #endregion

        //add this section later
        #region WILD NO PLAYER
            BTSelector wildAlone = null;
            foreach( Personality p in personalities){
                if(p.WildAlone != null){
                    wildAlone = p.WildAlone.BuildSelectorSubtree(context);
                }  
            }
            if(wildAlone == null){
                wildAlone = DefaultPersonality.WildAlone.BuildSelectorSubtree(context);
            }
        #endregion

        #region IS CREATURE WILD
            #region creature is wild
                List<BTNode> CreatureIsWildList = new List<BTNode>();
                CreatureIsWildList.Add(wildNoticed);
                //be sure to add the no player section later
                CreatureIsWildList.Add(wildAlone); //placeholder for wild w/ no player section

                BTSelector creatureIsWildSelector = new BTSelector("Creature Is Wild", CreatureIsWildList);
            #endregion

            #region is creature wild
                List<BTNode> IsCreatureWildList = new List<BTNode>();
                CCheckIsWild isWild = new CCheckIsWild("Is Wild?", context);
                IsCreatureWildList.Add(isWild);
                IsCreatureWildList.Add(creatureIsWildSelector);
                BTSequence isCreatureWildSequence = new BTSequence("Is Creature Wild?", IsCreatureWildList);
            #endregion
        #endregion

        #region creature isnt wild selector
            List<BTNode> CreatureIsntWildSelectorList = new List<BTNode>();
            CreatureIsntWildSelectorList.Add(Ability);
            CreatureIsntWildSelectorList.Add(FollowPlayer);

            BTSelector CreatureIsntWildSelector = new BTSelector("Creature isnt Wild Selector", CreatureIsntWildSelectorList);
        #endregion

        #region ROOT
            RootList.Add(isCreatureWildSequence);
            RootList.Add(CreatureIsntWildSelector);

            BTSelector _root = new BTSelector("Root", RootList);
        #endregion
        behaviorTree = _root;
        Evaluate = true;
    }

}
