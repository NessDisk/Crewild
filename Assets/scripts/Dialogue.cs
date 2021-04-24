using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

}

[System.Serializable]
public class DialogueCinema
{

    public string name;

    public bool PlayContinue;

    [TextArea(3, 10)]
    public string sentences;

}
