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
        personalities = context.creatureStats.personalities;
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

        #region In Combat
            BTSequence InCombat = null;


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

        #region Enthusiasm Managing
        BTSelector enthusiasmManaging = null;
        foreach( Personality p in personalities){
                if(p.AbilityTree != null){
                    enthusiasmManaging = p.EnthusiasmTree.BuildSelectorSubtree(context);
                }  
            }
            if(enthusiasmManaging == null){
                enthusiasmManaging = DefaultPersonality.EnthusiasmTree.BuildSelectorSubtree(context);
            }
        #endregion

        #region hitsun
        List<BTNode> hitstunList = new List<BTNode>();
        CCheckIsHit isHit = new CCheckIsHit("Check if creature hit", context);
        CActionDoHitAnim doHitAnim = new CActionDoHitAnim("Play Hitstun anim", context);
        hitstunList.Add(isHit);
        hitstunList.Add(doHitAnim);

        BTSequence hitstunSequence = new BTSequence("Hitstun Sequence", hitstunList);

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

        #region on/off field selector
            List<BTNode> offFieldSelectorList = new List<BTNode>();

            #region Creature Off Field Enthusiasm increase
                //swapped sequence
                List<BTNode> offFieldSequenceList = new List<BTNode>();
                //check if off field
                CCheckNotActive isNotActive = new CCheckNotActive("Is creature not active?", context);
                //idle action that increases enthusiasm
                CActionNotActiveIdle notActiveIdle = new CActionNotActiveIdle("Gain enthusiasm while not active", context);

                offFieldSequenceList.Add(isNotActive);
                offFieldSequenceList.Add(notActiveIdle);

                BTSequence offFieldSequence = new BTSequence("Creature off field Sequence", offFieldSequenceList);
            #endregion

            #region creature isnt wild selector
                List<BTNode> CreatureIsntWildSelectorList = new List<BTNode>();
                CreatureIsntWildSelectorList.Add(Ability);
                CreatureIsntWildSelectorList.Add(FollowPlayer);

                BTSelector CreatureIsntWildSelector = new BTSelector("Creature isnt Wild Selector", CreatureIsntWildSelectorList);
            #endregion
            offFieldSelectorList.Add(offFieldSequence);
            offFieldSelectorList.Add(CreatureIsntWildSelector);
            BTSelector offFieldSelector = new BTSelector("Creature Off Field Selector", offFieldSelectorList);
        #endregion
        

        #region ROOT
            RootList.Add(isCreatureWildSequence);
            RootList.Add(offFieldSelector);

            BTSelector _root = new BTSelector("Root", RootList);
        #endregion
        behaviorTree = _root;
        Evaluate = true;
    }

}
