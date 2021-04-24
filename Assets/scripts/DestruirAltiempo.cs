using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirAltiempo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.parent = GameObject.Find("baltle interface/baltle interface").GetComponent<RectTransform>().transform;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
