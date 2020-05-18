using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSelector : MonoBehaviour
{
	public string PortText;
	public string IngText;
	public string TchecText;

	public GameObject LangMan;

	// Start is called before the first frame update
	void Start()
    {
		LangMan = GameObject.FindGameObjectWithTag("LM");

		PortText = PortText.Replace("@", System.Environment.NewLine);
		IngText = IngText.Replace("@", System.Environment.NewLine);
		TchecText = TchecText.Replace("@", System.Environment.NewLine);
	}

    // Update is called once per frame
    void Update()
    {
		if (!LangMan.GetComponent<CurrentLanguage>().textsActive || LangMan == null)
		{
			GetComponent<LangText>().PortText = PortText;
			GetComponent<LangText>().IngText = IngText;
			GetComponent<LangText>().TchecText = TchecText;
		}
    }
}
