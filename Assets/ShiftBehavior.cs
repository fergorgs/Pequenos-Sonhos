using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShiftBehavior : MonoBehaviour
{
    public GameObject wrdControl;
    private Camera mainCamera;
    private WorldSwitchScript wrdSwitch;
    
    public GameObject pauseControll;
    private PauseScript pauseScript;

    private SpriteRenderer[] sprd;
    private Collider2D[] colliders;
    private Rigidbody2D rb2d;

    private Color color;
    public bool startsReal;
    public bool isShiftable;

    private bool isReal;
    public bool GetIsReal() { return isReal; }
	public void SetIsReal(bool val) { isReal = val; }
    private bool clicked;

    public float transVal = 0.5f;

	private float clickCoolDownTime = 0.5f;
	private bool clickCoolDown = false;
	private bool isProp = false;

	public Animator childComponent;


    // Use this for initialization
    void Start()
    {
        if (wrdControl == null)
            wrdControl = GameObject.FindGameObjectWithTag("WCS");
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        isReal = startsReal;
        
        pauseControll = GameObject.FindGameObjectWithTag("PC");
        pauseScript = pauseControll.GetComponent<PauseScript>();

        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

        sprd = GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
        if (GetComponent<Rigidbody2D>() != null)
            rb2d = GetComponent<Rigidbody2D>();

        if (startsReal)
        {
            foreach (SpriteRenderer s in sprd)
            {
                s.color = Color.white;
            }
            for (int i = 0; i < colliders.Length; i++)
                colliders[i].enabled = true;
            if (rb2d != null)
                rb2d.simulated = true;
        }
        else
        {
            foreach (SpriteRenderer s in sprd)
            {
                if (gameObject.tag == "Caixa Empurravel" || gameObject.tag == "Caixa" || gameObject.tag == "Plataforma")
                    s.color = new Color(1, 1, 1, transVal);
                else
                    s.color = Color.clear;
            }
            for (int i = 0; i < colliders.Length; i++)
                colliders[i].enabled = false;
            if (rb2d != null)
                rb2d.simulated = false;
        }

		if (gameObject.tag == "Caixa" || gameObject.tag == "Caixa Empurravel" || gameObject.tag == "Plataforma")
			isProp = true;
	}

    // Update is called once per frame
    void Update()
    {

        if (wrdSwitch.worldHasShifted())
        {
            foreach (SpriteRenderer s in sprd)
            {
                s.color = Color.white;
            }

			if (clicked)
			{
				if (isProp)
				{
					GetComponent<Animator>().CrossFade("Pop_up", 0);
					GetComponentInChildren<PropAuraScript>().PopUpEffect();
				}
				clicked = false;
			}

            if (wrdSwitch.worldIsReal() == isReal)
            {
                if (ColorUtility.TryParseHtmlString(wrdSwitch.colorReal, out color))
                {
                    color.a = 0;
                    mainCamera.backgroundColor = color;
                }
                foreach (SpriteRenderer s in sprd)
                {
                    s.color = Color.white;
                }
                for (int i = 0; i < colliders.Length; i++)
                    colliders[i].enabled = true;
                if (rb2d != null)
                    rb2d.simulated = true;

            }
            else
            {
                if (ColorUtility.TryParseHtmlString(wrdSwitch.colorDream, out color))
                {
                    color.a = 0;
                    mainCamera.backgroundColor = color;
                }
                foreach (SpriteRenderer s in sprd)
                {
                    if (gameObject.tag == "Caixa Empurravel" || gameObject.tag == "Caixa" || gameObject.tag == "Plataforma")
                        s.color = new Color(1, 1, 1, transVal);
                    else
                        s.color = Color.clear;
                }
                for (int i = 0; i < colliders.Length; i++)
                    colliders[i].enabled = false;
                if (rb2d != null)
                    rb2d.simulated = false;
            }
        }
    }

	private IEnumerator ClickCooldown()
	{
		yield return new WaitForSeconds(clickCoolDownTime);
		clickCoolDown = false;
	}


	void OnMouseUp()
    {
		Debug.Log("Clicked");
		if (gameObject.name == "Caixa_E")
		{
			if (!pauseScript.IsPaused())
			{
				if (isShiftable && !clickCoolDown)
				{
					isReal = !isReal;
					clicked = !clicked;
					foreach (SpriteRenderer s in sprd)
					{
						if (s.color.r == 1 && s.color.g == 0 && s.color.b == 1)
							s.color = Color.white;
						else
							s.color = Color.magenta;
					}

					clickCoolDown = true;
					StartCoroutine(ClickCooldown());
				}
			}
		}
		else
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				if (!pauseScript.IsPaused())
				{
					if (isShiftable && !clickCoolDown)
					{
						isReal = !isReal;
						clicked = !clicked;
						foreach (SpriteRenderer s in sprd)
						{
							if (s.color.r == 1 && s.color.g == 0 && s.color.b == 1)
								s.color = Color.white;
							else
								s.color = Color.magenta;
						}

						clickCoolDown = true;
						StartCoroutine(ClickCooldown());
					}
				}
			}
		}
    }
}

