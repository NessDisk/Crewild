using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noTeDestruyasInformacion : MonoBehaviour
{

    public static noTeDestruyasInformacion NoteDestruir;

    public Camera camara;
    public Canvas canvasInfo;
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

        canvasInfo = gameObject.GetComponent<Canvas>();


    }
    // Start is called before the first frame update
    void Start()
    {
        canvasInfo = gameObject.GetComponent<Canvas>();
        camara = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //cuando la camara se se desvincula en el cambio de scena
        if (canvasInfo.worldCamera == null)
        {
            camara = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvasInfo.worldCamera = camara;
        }
    }
}
