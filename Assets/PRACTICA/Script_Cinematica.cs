using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Cinematica : MonoBehaviour {

    public Animation animationCinema;

    public string  StringCinemaActual;
    public bool ActivaCinema;




    // Use this for initialization
    void Start () {
        animationCinema = GetComponent<Animation>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// pausa la animacion y pide un nombre para el trigger cinematica
    /// </summary>
    /// <param name="dialogueTrigger"></param>
    public void pausa(string dialogueTrigger)
    {
      
        GameObject.Find(dialogueTrigger).GetComponent<Trigger_Cinematic>().dialogueCinema();
        animationCinema[GameObject.Find(dialogueTrigger).GetComponent<Trigger_Cinematic>().cinematicName].speed = 0;
    }

    public void Exit(string dialogueTrigger)
    {
        animationCinema.Stop(GameObject.Find(dialogueTrigger).GetComponent<Trigger_Cinematic>().cinematicName);
        GameObject.Find(dialogueTrigger).GetComponent<Trigger_Cinematic>().exit();
    }

}
