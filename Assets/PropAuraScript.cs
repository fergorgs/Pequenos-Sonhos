using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAuraScript : MonoBehaviour
{
	public GameObject pauseControl, wrdControl, father;
	private PauseScript psscr;
	private WorldSwitchScript wrdscr;

	public float period, minTrans;
	private float fatherMinTrans;

	private float step;

	private SpriteRenderer sprd;

	private bool fadingOut = true;

    // Start is called before the first frame update
    void Start()
    {
		pauseControl = GameObject.FindGameObjectWithTag("PC");
		wrdControl = GameObject.FindGameObjectWithTag("WCS");
		father = transform.parent.gameObject;

		psscr = pauseControl.GetComponent<PauseScript>();
		wrdscr = wrdControl.GetComponent<WorldSwitchScript>();

		sprd = GetComponent<SpriteRenderer>();

		fatherMinTrans = father.GetComponent<ShiftBehavior>().transVal;

		StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {
		if (wrdscr.worldHasShifted())
		{
			if (father.GetComponent<ShiftBehavior>().GetIsReal() == wrdscr.worldIsReal())
				sprd.color = sprd.color - (new Color(0, 0, 0, fatherMinTrans));
			else
				sprd.color = sprd.color + (new Color(0, 0, 0, fatherMinTrans));
		}
    }

	private IEnumerator Fade()
	{
		while (true)
		{
			step = (1 - minTrans) * Time.deltaTime / (period / 2);

			if (!psscr.IsPaused())
			{
				if (father.GetComponent<ShiftBehavior>().GetIsReal() == wrdscr.worldIsReal())
				{
					if (fadingOut)
					{
						sprd.color = sprd.color - (new Color(0, 0, 0, step));
						if (sprd.color.a < minTrans)
							fadingOut = false;
					}
					else
					{
						sprd.color = sprd.color + (new Color(0, 0, 0, step));
						if (sprd.color.a > 0.97f)
							fadingOut = true;
					}
				}
				else
				{
					if (fadingOut)
					{
						sprd.color = sprd.color - (new Color(0, 0, 0, step));
						if (sprd.color.a < minTrans - fatherMinTrans)
							fadingOut = false;
					}
					else
					{
						sprd.color = sprd.color + (new Color(0, 0, 0, step));
						if (sprd.color.a > 0.97f - fatherMinTrans)
							fadingOut = true;
					}
				}
			}

			yield return new WaitForFixedUpdate();
		}

	}
}
