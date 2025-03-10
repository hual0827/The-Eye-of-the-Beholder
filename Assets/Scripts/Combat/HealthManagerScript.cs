using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagerScript : MonoBehaviour
{
    public static bool hasChangedHealth = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hasChangedHealth = false;
    }
    
    
    public static void ChangeHealth(string character, int damage, float timeDelay)
    {
        switch (character)
        {
            // Main Characters
            case "Netrixi":
                var netrixi = new GameObject("runner");
                var runner1 = netrixi.AddComponent<CoroutineRunner>();
                runner1.StartCoroutine(runner1.Wait("Netrixi", damage, netrixi, timeDelay));
                break;
            
            case "Folkvar":
                var folkvar = new GameObject("runner");
                var runner2 = folkvar.AddComponent<CoroutineRunner>();
                runner2.StartCoroutine(runner2.Wait("Folkvar", damage, folkvar, timeDelay));
                break;
            
            case "Iv":
                var iv = new GameObject("runner");
                var runner3 = iv.AddComponent<CoroutineRunner>();
                runner3.StartCoroutine(runner3.Wait("Iv", damage, iv, timeDelay));
                break;
            
            
            
            // Enemy Characters
            case "Enemy 1":
                var enemy1 = new GameObject("runner");
                var runner4 = enemy1.AddComponent<CoroutineRunner>();
                runner4.StartCoroutine(runner4.Wait("Enemy 1", damage, enemy1, timeDelay));
                break;
            
            case "Enemy 2":
                var enemy2 = new GameObject("runner");
                var runner5 = enemy2.AddComponent<CoroutineRunner>();
                runner5.StartCoroutine(runner5.Wait("Enemy 2", damage, enemy2, timeDelay));
                break;
            
            case "Enemy 3":
                var enemy3 = new GameObject("runner");
                var runner6 = enemy3.AddComponent<CoroutineRunner>();
                runner6.StartCoroutine(runner6.Wait("Enemy 3", damage, enemy3, timeDelay));
                break;
        }
    }



    public static void StartingHealth(int enemy1, int enemy2, int enemy3)
    {
        if (!hasChangedHealth)
        {
            CombatManagerScript.enemy1HP = enemy1;
            CombatManagerScript.enemy2HP = enemy2;
            CombatManagerScript.enemy3HP = enemy3;

            hasChangedHealth = true;
        }
    }
}



public class CoroutineRunner : MonoBehaviour
{
    public IEnumerator Wait(string character, int damage, GameObject runner, float timeDelay)
    {
        int counter;
        
        // If the character was harmed
        if (damage >= 0) counter = damage;
        // If the character was healed
        else counter = -damage;
        
        for (int i = 0; i < counter; i++)
        {
            yield return new WaitForSecondsRealtime(timeDelay);
        
            // If the character was harmed
            if (damage >= 0) HurtCharacter(character);
            // If the character was healed
            else HealCharacter(character);

            if (i == damage - 1) Destroy(runner);
        }
    }

    private static void HurtCharacter(string character)
    {
        switch (character)
        {
            case "Netrixi": CombatManagerScript.netrixiHP--;
                break;
            
            case "Folkvar": CombatManagerScript.folkvarHP--;
                break;
            
            case "Iv": CombatManagerScript.ivHP--;
                break;
            
            
            case "Enemy 1": CombatManagerScript.enemy1HP--;
                break;
            
            case "Enemy 2": CombatManagerScript.enemy2HP--;
                break;
            
            case "Enemy 3": CombatManagerScript.enemy3HP--;
                break;
        }
    }
    
    
    private static void HealCharacter(string character)
    {
        switch (character)
        {
            case "Netrixi": 
                if (CombatManagerScript.netrixiHP < HealthValues.netrixiHP) CombatManagerScript.netrixiHP++;
                break;
            
            case "Folkvar": 
                if (CombatManagerScript.folkvarHP < HealthValues.folkvarHP) CombatManagerScript.folkvarHP++;
                break;
            
            case "Iv": 
                if (CombatManagerScript.ivHP < HealthValues.ivHP) CombatManagerScript.ivHP++;
                break;
            
            
            case "Enemy 1": 
                if (CombatManagerScript.enemy1HP < CombatManagerScript.enemy1StartingHP) CombatManagerScript.enemy1HP++;
                break;
            
            case "Enemy 2": 
                if (CombatManagerScript.enemy2HP < CombatManagerScript.enemy2StartingHP) CombatManagerScript.enemy2HP++;
                break;
            
            case "Enemy 3": 
                if (CombatManagerScript.enemy3HP < CombatManagerScript.enemy3StartingHP) CombatManagerScript.enemy3HP++;
                break;
        }
    }
}
