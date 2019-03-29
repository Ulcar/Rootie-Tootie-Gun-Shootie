using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tossable : MonoBehaviour
{
    public bool CurrentlyBeingTossed = false;
    public float tossSpeed;
    public float FloorY;
    private float yVelocity;
    bool tossDirectionNegative;
    int count = 0;
    // Start is called before the first frame update
    public void TossItem(bool TossDirectionNegative)
    {
        tossDirectionNegative = TossDirectionNegative;
        StartCoroutine("Toss");
    }
    public IEnumerator Toss()
    {
        CurrentlyBeingTossed = true;
        for (; tossSpeed > 0; tossSpeed -= (0.02f * count / 5))
        {
            Debug.Log("TOSSING");
            if (tossSpeed > 0)
            {
                if (transform.position.y > FloorY)
                {
                    Debug.Log("Y changed, Y: " + transform.position.y);
                    transform.position = new Vector3(transform.position.x, transform.position.y - yVelocity, transform.position.z);
                    if (transform.position.y <= FloorY)
                    {
                        transform.position = new Vector3(transform.position.x, FloorY, transform.position.z);
                    }
                    yVelocity += 0.2f;
                }
                if (tossDirectionNegative)
                {
                    transform.position = new Vector3(transform.position.x - tossSpeed, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + tossSpeed, transform.position.y, transform.position.z);
                }
                count++;
            }
            else
            {
                Debug.Log("TossY: " + transform.position.y + ", FloorY: " + FloorY);
                CurrentlyBeingTossed = false;
                count = 0;
                yVelocity = 0;
                yield break;
            }
            yield return null;
        }
        Debug.Log("TossY: " + transform.position.y + ", FloorY: " + FloorY);
        count = 0;
        yVelocity = 0;
        CurrentlyBeingTossed = false;
        yield break;
    }
}
