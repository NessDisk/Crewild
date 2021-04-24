using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class test_change_Stage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("test2");
        Debug.ClearDeveloperConsole();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
