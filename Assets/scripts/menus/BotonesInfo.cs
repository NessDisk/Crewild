using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesInfo : MonoBehaviour
{
    movimiento movPlayer;

    Canvas InfoBotones;
    // Start is called before the first frame update
    void Start()
    {
        movPlayer = FindObjectOfType<movimiento>();
        InfoBotones = GameObject.Find("Pantalla Vanilla").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) == true && InfoBotones.enabled == false && movPlayer.DisparadorEvento == false)
        {
            InfoBotones.enabled = true;
            movPlayer.DisparadorEvento = true;

        }

        else  if (Input.GetKeyDown(KeyCode.I) == true && InfoBotones.enabled == true)
        {
            InfoBotones.enabled = false;
            movPlayer.DisparadorEvento = false;

        }
    }
}
