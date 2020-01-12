using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailSafeScript : MonoBehaviour
{
	public GameObject targetObj;

	public Vector3 targetDest;

	public enum Cases { Dream, Real, Both };

	public Cases whenToTeleport = Cases.Real;

	public WorldSwitchScript wrdCtrl;

    // Start is called before the first frame update
    void Start()
    {
		GetComponent<SpriteRenderer>().color = Color.clear;

		wrdCtrl = GameObject.FindGameObjectWithTag("WCS").GetComponent<WorldSwitchScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerStay2D(Collider2D collision)
	{
		if (targetObj.GetComponent<ShiftBehavior>().GetIsReal() == wrdCtrl.worldIsReal())
		{
			switch (whenToTeleport)
			{

				case Cases.Real:
					if (collision.gameObject == targetObj && wrdCtrl.worldIsReal())
						targetObj.transform.position = targetDest;
					break;

				case Cases.Dream:
					if (collision.gameObject == targetObj && !wrdCtrl.worldIsReal())
						targetObj.transform.position = targetDest;
					break;

				case Cases.Both:
					if (collision.gameObject == targetObj)
						targetObj.transform.position = targetDest;
					break;
			}
		}
	}
}
