using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform PosCam;
    // Start is called before the first frame update
    void Start()
    {
        PosCam = GameObject.Find("personaje").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new  Vector3(PosCam.position.x, PosCam.position.y, -3); 
    }
}
