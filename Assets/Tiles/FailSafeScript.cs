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

	private bool isPlayer = false;
	private float rightBound, leftBound, upperBound, lowerBound;

    // Start is called before the first frame update
    void Start()
    {
		GetComponent<SpriteRenderer>().color = Color.clear;

		wrdCtrl = GameObject.FindGameObjectWithTag("WCS").GetComponent<WorldSwitchScript>();

		isPlayer = targetObj.CompareTag("Player");

		rightBound = transform.position.x + 0.5f;
		leftBound = transform.position.x - 0.5f;
		upperBound = transform.position.y + 0.5f;
		lowerBound = transform.position.y - 0.5f;
	}

    // Update is called once per frame
    void Update()
    {
		if (isPlayer)
		{
			float tgtx = targetObj.transform.position.x;
			float tgty = targetObj.transform.position.y;

			if ((tgtx > leftBound && tgtx < rightBound) && (tgty > lowerBound && tgty < upperBound))
			{
				switch (whenToTeleport)
				{
					case Cases.Real:
						if (wrdCtrl.worldIsReal())
							targetObj.transform.position = targetDest;
						break;

					case Cases.Dream:
						if (!wrdCtrl.worldIsReal())
							targetObj.transform.position = targetDest;
						break;

					case Cases.Both:
						targetObj.transform.position = targetDest;
						break;
				}
			}
		}
    }

	void OnTriggerStay2D(Collider2D collision)
	{
		if (!isPlayer)
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
}
