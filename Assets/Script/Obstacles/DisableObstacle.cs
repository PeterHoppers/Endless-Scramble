using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisableObstacle : MonoBehaviour
{
    public Material disabledColor;
    Material normalColor;

    static bool disable;
    static Image arrow;

	// Use this for initialization
	void Start () 
    {
        normalColor = gameObject.GetComponent<Image>().material;
        disable = false;
        DeathManager.PlayerKilled += ResetDisable;
	}

    private void OnDestroy()
    {
        DeathManager.PlayerKilled -= ResetDisable;
    }

    void ResetDisable()
    {
        this.enabled = true;
        this.gameObject.GetComponent<Image>().material = normalColor;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        disable = false;

        if (arrow == null)
            return;

        if (!arrow.gameObject.activeInHierarchy)
            arrow.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!disable)
            {
                arrow = collision.gameObject.GetComponent<Movement>().GetRandomArrow();
                arrow.gameObject.SetActive(false);
            }
            else
            {
                arrow.gameObject.SetActive(true);
            }
            
            disable = !disable;

            DisableObstacle[] disables = transform.parent.GetComponentsInChildren<DisableObstacle>();

            foreach (DisableObstacle disable in disables)
            {
                disable.enabled = false;
                disable.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                disable.gameObject.GetComponent<Image>().material = disabledColor;
            }
            
        }
    }
}
