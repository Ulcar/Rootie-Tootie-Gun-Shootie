using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableItem : MonoBehaviour
{
    public int Price;
    public bool ForSale = false;
    // Start is called before the first frame update

    IEnumerator CashFlash()
    {
        for (float i = 255f; i >= 0; i--)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c = new Color(i, c.g, c.b);
            Debug.Log("C.a: " + c.a);
            GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }
        for (float i = 0f; i < 255; i++)
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c = new Color(i, c.g, c.b);
            Debug.Log("C.a: " + c.a);
            GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }
    }

    public void CashFlashing()
    {
        StartCoroutine("CashFlash");
        Debug.Log("CashFlash");
    }
}
