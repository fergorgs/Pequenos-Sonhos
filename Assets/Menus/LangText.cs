using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangText : MonoBehaviour {

    public string PortText;
    public string IngText;
    public string TchecText;

    public GameObject LangMan;

    private Text uiText;
    private TextMesh goText;

    private bool isUi = true;

    // Use this for initialization
    void Start () {

        uiText = GetComponent<Text>();
        goText = GetComponent<TextMesh>();

        if (uiText == null)
            isUi = false;

        LangMan = GameObject.FindGameObjectWithTag("LM");

        PortText = PortText.Replace("@", System.Environment.NewLine);
        IngText = IngText.Replace("@", System.Environment.NewLine);
        TchecText = TchecText.Replace("@", System.Environment.NewLine);

    }

    void Update()
    {
        if (isUi)
        {
            if (LangMan.GetComponent<CurrentLanguage>().GetLingua() == CurrentLanguage.Lingua.Tchec)
                uiText.text = TchecText;
            else if (LangMan.GetComponent<CurrentLanguage>().GetLingua() == CurrentLanguage.Lingua.Ing)
                uiText.text = IngText;
            else
                uiText.text = PortText;
        }
        else
        {
            if (LangMan.GetComponent<CurrentLanguage>().GetLingua() == CurrentLanguage.Lingua.Tchec)
                goText.text = TchecText;
            else if (LangMan.GetComponent<CurrentLanguage>().GetLingua() == CurrentLanguage.Lingua.Ing)
                goText.text = IngText;
            else
                goText.text = PortText;
        }
    }
}
