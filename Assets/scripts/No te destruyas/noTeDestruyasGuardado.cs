﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noTeDestruyasGuardado : MonoBehaviour
{
    public static noTeDestruyasGuardado NoteDestruir;
    void Awake()
    {
        if (NoteDestruir == null)
        {
            
            NoteDestruir = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (NoteDestruir != this)
        {
            Destroy(gameObject);
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
