﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightProperty : MonoBehaviour {

	//public float hPl = 4;

    public float GetHeight() {

        if (tag == "Inimigo")
            return 4;
        if (tag == "Caixa Empurravel")
            return 2;
		if (tag == "Player")
			return 3.8f;// hPl;

        return 0;
    }
}
