using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Npc_Cinematico : MonoBehaviour {

    private NPC_Dialoge npc_DialogueScript;

    private Npc_move npc_MoveScript;

    private  Animator animatorNpc ;

    public bool activateCinemati;  

    [Range(1,4)]
    public int faceNumAnimator;

    public bool walk;



    // Use this for initialization
    void Start ()
    {
        //npc_DialogueScript = GetComponent<NPC_Dialoge>();

        //npc_MoveScript = GetComponent<Npc_move>();

        animatorNpc = GetComponent<Animator>();

     



       
    }

    // Update is called once per frame
    void Update()
    {
        if (activateCinemati == false)
        {
            //npc_DialogueScript.stopDialogue = false;
           // npc_MoveScript.NPCStop = false;
            return;
        }

      //  npc_DialogueScript.stopDialogue = true;
      //  npc_MoveScript.NPCStop = true;

        animatorNpc.SetInteger("face", faceNumAnimator);
        animatorNpc.SetBool("walk", walk);
    }


    
}
