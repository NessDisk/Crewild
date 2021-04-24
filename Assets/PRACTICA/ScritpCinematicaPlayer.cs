using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScritpCinematicaPlayer : MonoBehaviour {

    private Animator animatorPlayer;

   

    private movimiento moveplayer;

    public bool activateCinemati;

    [Range(1, 4)]
    public int faceNumAnimator;

    public bool walk;

    // Use this for initialization
    void Start ()
    {

        moveplayer = GetComponent<movimiento>();
        animatorPlayer = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (activateCinemati == false)
        {
           return;
        }

      

        animatorPlayer.SetInteger("face", faceNumAnimator);
        animatorPlayer.SetBool("walk", walk);
    }
}
