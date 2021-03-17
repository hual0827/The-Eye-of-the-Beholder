using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTargetLocation : MonoBehaviour
{
    public int positionNumber;

    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManagerScript.attack1Location == positionNumber || EnemyManagerScript.attack2Location == positionNumber || EnemyManagerScript.attack1Location2 == positionNumber || EnemyManagerScript.attack2Location2 == positionNumber)
        {
            sr.color = Color.red;
        }
        else
        {
            if (CombatManagerScript.netrixiTarget1Location == positionNumber || CombatManagerScript.netrixiTarget2Location == positionNumber)
            {
                sr.color = Color.red;
            }
            else
            {
                if (CombatManagerScript.folkvarTarget1Location == positionNumber || CombatManagerScript.folkvarTarget2Location == positionNumber)
                {
                    sr.color = Color.red;
                }
                else
                {
                    sr.color = Color.white;
                }
            }
        }
    }
}
