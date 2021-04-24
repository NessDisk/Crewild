using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestruyas : MonoBehaviour
{
    public static NoteDestruyas NoteDestruir;
    void Awake()
    {
        if (NoteDestruir == null)
        {
            print("llegue  aqui");
            NoteDestruir = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (NoteDestruir != this)
        {
            Destroy(gameObject);
        }
        print("llegue  aqui 3");
    }
    // Start is called before the first frame update
    void Start()
    {
        print("llegue  aqui 2");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
