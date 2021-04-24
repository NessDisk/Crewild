using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosMenus : MonoBehaviour
{

    public AudioSource Audio , AudioAuxiliar;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        AudioAuxiliar = GameObject.Find("Game Manager/Sonidos Aux").GetComponent<AudioSource>();
    }


   }
