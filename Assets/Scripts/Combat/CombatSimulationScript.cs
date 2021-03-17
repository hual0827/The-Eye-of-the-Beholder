using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CombatSimulationScript : MonoBehaviour
{
    private static float moveDelay = DamageValues.standardDelay;
    public static float attack1Delay, attack2Delay;

    public static bool didPlayerMove1 = false, didPlayerMove2 = false;
    public static bool didEnemyMove1 = false, didEnemyMove2 = false;

    public static string playerAttacker1, playerAttacker2;
    public static int enemyAttacker1 = 0, enemyAttacker2 = 0;

    public static int playerAttack1Target = 0, playerAttack2Target = 0;
    public static string enemyAttack1Target, enemyAttack2Target;

    public static string enemy1stAttack, enemy2ndAttack;

    public IEnumerator RunSimulation(int playerFirstAttack, int playerSecondAttack, string enemyFirstAttack, string enemySecondAttack, GameObject runner)
    {
        CombatManagerScript.netrixiAttacks = false;
        CombatManagerScript.folkvarAttacks = false;
        CombatManagerScript.ivAttacks = false;
        
        
        // FIRST ATTACK
        yield return new WaitForSecondsRealtime(moveDelay);
        
        // Check to see if any Main Characters move for their first attack
        CheckForPlayerMovement(playerFirstAttack, 1);
        // If any Main Characters did move for their first attack
        if (didPlayerMove1) yield return new WaitForSecondsRealtime(moveDelay);

        // Check to see if any Enemy Characters move for their first attack
        CheckForEnemyMovement(enemyFirstAttack, 1);
        // If any Enemy Characters did move for their first attack
        if (didEnemyMove1) yield return new WaitForSecondsRealtime(moveDelay);
        
        
        
        // Check to see what the player's first attack is
        if (!didPlayerMove1)
        {
            AttackScript.PlayerAttack(playerFirstAttack, 1);
            CombatManagerScript.playerAttacking1 = true;
            
                // TODO: Play Player attack 1 animation
                PlayerAttackAnimation(playerAttacker1, playerAttack1Target);

                //print(playerAttacker1 + ", " + playerAttack1Target);
        }
        else attack1Delay = 0;

        AttackScript.enemyAttack = !AttackScript.enemyAttack;

        CombatManagerScript.netrixiTarget1Location = 0;
        CombatManagerScript.folkvarTarget1Location = 0;
        
        // Wait for attack animation to play
        yield return new WaitForSecondsRealtime(attack1Delay);
        
            
        // Check to see what the enemy's first attack is
        if (!didEnemyMove1)
        {
            DetermineEnemy(enemyFirstAttack, 1);

            AttackScript.EnemyAttack(enemy1stAttack, 1, enemyAttacker1);
            CombatManagerScript.enemyAttacking1 = true;
            
                // TODO: Play Enemy attack 1 animation
                EnemyAttackAnimation(enemyAttacker1, enemyAttack1Target);

                //print(enemyAttacker1 + ", " + enemyAttack1Target);
        }
        else attack1Delay = 0;
        
        AttackScript.playerAttack = !AttackScript.playerAttack;

        EnemyManagerScript.attack1Location = 0;
        EnemyManagerScript.attack1Location2 = 0;

        // Wait for attack animation to play
        yield return new WaitForSecondsRealtime(attack1Delay);


        
        
        // SECOND ATTACK

        // Check to see if any Main Characters move for their second attack
        CheckForPlayerMovement(playerSecondAttack, 2);
        // If any Main Characters did move for their second attack
        if (didPlayerMove2) yield return new WaitForSecondsRealtime(moveDelay);

        // Check to see if any Enemy Characters move for their second attack
        CheckForEnemyMovement(enemySecondAttack, 2);
        // If any Enemy Characters did move for their second attack
        if (didEnemyMove2) yield return new WaitForSecondsRealtime(moveDelay);
        
        
        
        // Check to see what the player's second attack is
        if (!didPlayerMove2)
        {
            AttackScript.PlayerAttack(playerSecondAttack, 2);
            CombatManagerScript.playerAttacking2 = true;
            
                // TODO: Play Player attack 2 animation
                PlayerAttackAnimation(playerAttacker2, playerAttack2Target);
            
                //print(playerAttacker2 + ", " + playerAttack2Target);
        }
        else attack2Delay = 0;
        
        AttackScript.enemyAttack = !AttackScript.enemyAttack;
        
        CombatManagerScript.netrixiTarget2Location = 0;
        CombatManagerScript.folkvarTarget2Location = 0;

        // Wait for attack animation to play
        yield return new WaitForSecondsRealtime(attack2Delay);

            
        // Check to see what the enemy's second attack is
        if (!didEnemyMove2)
        {
            DetermineEnemy(enemySecondAttack, 2);

            AttackScript.EnemyAttack(enemy2ndAttack, 2, enemyAttacker2);
            CombatManagerScript.enemyAttacking2 = true;
            
                // TODO: Play Enemy attack 2 animation
                EnemyAttackAnimation(enemyAttacker2, enemyAttack2Target);

                //print(enemyAttacker2 + ", " + enemyAttack2Target);
        }
        else attack2Delay = 0;

        AttackScript.playerAttack = !AttackScript.playerAttack;
        
        EnemyManagerScript.attack2Location = 0;
        EnemyManagerScript.attack2Location2 = 0;

        // Wait for attack animation to play
        yield return new WaitForSecondsRealtime(attack2Delay);


        
        

        // Reset variables once simulation is finished
        CombatManagerScript.firstAttack = 0;
        CombatManagerScript.secondAttack = 0;

        EnemyManagerScript.previousFirstAttack = enemyFirstAttack;
        EnemyManagerScript.previousSecondAttack = enemySecondAttack;

        NetrixiCombatScript.ResetNetrixiVariables();
        FolkvarCombatScript.ResetFolkvarVariables();
        IvCombatScript.ResetIvVariables();
                
        CombatManagerScript.roundNumber += 1;

        CombatManagerScript.playerAttacking1 = false;
        CombatManagerScript.playerAttacking2 = false;
        CombatManagerScript.enemyAttacking1 = false;
        CombatManagerScript.enemyAttacking2 = false;

        CombatManagerScript.target1Location = 0;
        CombatManagerScript.target2Location = 0;

        CharacterManagerScript.ResetVariables();
        EnemyManagerScript.ClearMoves();

        didPlayerMove1 = false;
        didPlayerMove2 = false;
        didEnemyMove1 = false;
        didEnemyMove2 = false;


        // If only one character is alive
        //if (CombatManagerScript.netrixiAlive && !CombatManagerScript.folkvarAlive && !CombatManagerScript.ivAlive) CombatManagerScript.netrixiAttacks = true;
        //if (!CombatManagerScript.netrixiAlive && CombatManagerScript.folkvarAlive && !CombatManagerScript.ivAlive) CombatManagerScript.folkvarAttacks = true;
        //if (!CombatManagerScript.netrixiAlive && !CombatManagerScript.folkvarAlive && CombatManagerScript.ivAlive) CombatManagerScript.ivAttacks = true;

        
        Destroy(runner);
    }




    public static void CheckForPlayerMovement(int playerAttack, int attackNumber)
    {
        // Main Characters
        if (playerAttack == 10 || playerAttack == 11)
        {
            if (CombatManagerScript.netrixiAlive)
            {
                if (attackNumber == 1)
                {
                    CharacterManagerScript.netrixiPosition = CharacterManagerScript.netrixi1st;
                    CombatManagerScript.playerAttacking1 = true;
                    didPlayerMove1 = true;
                }
                else
                {
                    CharacterManagerScript.netrixiPosition = CharacterManagerScript.netrixi2nd;
                    CombatManagerScript.playerAttacking2 = true;
                    didPlayerMove2 = true;
                }
            }
        }
        
        if (playerAttack == 12 || playerAttack == 13)
        {
            if (CombatManagerScript.folkvarAlive)
            {
                if (attackNumber == 1)
                {
                    CharacterManagerScript.folkvarPosition = CharacterManagerScript.folkvar1st;
                    CombatManagerScript.playerAttacking1 = true;
                    didPlayerMove1 = true;
                }
                else
                {
                    CharacterManagerScript.folkvarPosition = CharacterManagerScript.folkvar2nd;
                    CombatManagerScript.playerAttacking2 = true;
                    didPlayerMove2 = true;
                }
            }
        }
        
        if (playerAttack == 14 || playerAttack == 15)
        {
            if (CombatManagerScript.ivAlive)
            {
                if (attackNumber == 1)
                {
                    CharacterManagerScript.ivPosition = CharacterManagerScript.iv1st;
                    CombatManagerScript.playerAttacking1 = true;
                    didPlayerMove1 = true;
                }
                else
                {
                    CharacterManagerScript.ivPosition = CharacterManagerScript.iv2nd;
                    CombatManagerScript.playerAttacking2 = true;
                    didPlayerMove2 = true;
                }
            }
        }
    }


    public static void CheckForEnemyMovement(string enemyAttack, int attackNumber)
    {
        // Enemy Characters
        if (enemyAttack == "Enemy 1-Moves Left" || enemyAttack == "Enemy 1-Moves Right")
        {
            if (CombatManagerScript.enemy1Alive)
            {
                if (attackNumber == 1)
                {
                    EnemyManagerScript.enemy1Position = EnemyManagerScript.enemy1First;
                    CombatManagerScript.enemyAttacking1 = true;
                    didEnemyMove1 = true;
                }
                else
                {
                    EnemyManagerScript.enemy1Position = EnemyManagerScript.enemy1Second;
                    CombatManagerScript.enemyAttacking2 = true;
                    didEnemyMove2 = true;
                }
            }
        }
        
        if (enemyAttack == "Enemy 2-Moves Left" || enemyAttack == "Enemy 2-Moves Right")
        {
            if (CombatManagerScript.enemy2Alive)
            {
                if (attackNumber == 1)
                {
                    EnemyManagerScript.enemy2Position = EnemyManagerScript.enemy2First;
                    CombatManagerScript.enemyAttacking1 = true;
                    didEnemyMove1 = true;
                }
                else
                {
                    EnemyManagerScript.enemy2Position = EnemyManagerScript.enemy2Second;
                    CombatManagerScript.enemyAttacking2 = true;
                    didEnemyMove2 = true;
                }
            }
        }
        
        if (enemyAttack == "Enemy 3-Moves Left" || enemyAttack == "Enemy 3-Moves Right")
        {
            if (CombatManagerScript.enemy3Alive)
            {
                if (attackNumber == 1)
                {
                    EnemyManagerScript.enemy3Position = EnemyManagerScript.enemy3First;
                    CombatManagerScript.enemyAttacking1 = true;
                    didEnemyMove1 = true;
                }
                else
                {
                    EnemyManagerScript.enemy3Position = EnemyManagerScript.enemy3Second;
                    CombatManagerScript.enemyAttacking2 = true;
                    didEnemyMove2 = true;
                }
            }
        }
        
        string enemyChoice;

        string[] splitArray =  enemyAttack.Split(char.Parse("-"));
        enemyChoice = splitArray[1];
        

        // If the enemy blocks the player's incoming attack
        if (enemyChoice == "Blocks")
        {
            if (attackNumber == 1)
            {
                DetermineEnemy(enemyAttack, 1);

                AttackScript.EnemyAttack(enemy1stAttack, 1, enemyAttacker1);
                CombatManagerScript.enemyAttacking1 = true;
            
                    // TODO: Play Enemy attack 1 animation
                    EnemyAttackAnimation(enemyAttacker1, enemyAttack1Target);

                //print(enemyAttacker1 + ", " + enemyAttack1Target);

                didEnemyMove1 = true;
            }
            else
            {
                DetermineEnemy(enemyAttack, 2);

                AttackScript.EnemyAttack(enemy2ndAttack, 2, enemyAttacker2);
                CombatManagerScript.enemyAttacking2 = true;
            
                // TODO: Play Enemy attack 2 animation
                EnemyAttackAnimation(enemyAttacker2, enemyAttack2Target);

                //print(enemyAttacker2 + ", " + enemyAttack2Target);

                didEnemyMove2 = true;
            }
        }
    }



    public static void DetermineEnemy(string attack, int attackNumber)
    {
        string enemyName, enemyAttack;
        
        string[] splitArray =  attack.Split(char.Parse("-"));
        enemyName = splitArray[0];
        enemyAttack = splitArray[1];
        
        
        // Determine position of the enemy based on their name 
        switch (enemyName)
        {
            case "Pirate":
                if (attackNumber == 1) enemyAttacker1 = 1;
                else enemyAttacker2 = 1;
                break;
            
            case "Folkvar":
                if (attackNumber == 1) enemyAttacker1 = 1;
                else enemyAttacker2 = 1;
                break;
            
            case "Royal Knight Melee":
                if (GameManagerScript.currentScene == 4)
                {
                    if (attackNumber == 1) enemyAttacker1 = 2;
                    else enemyAttacker2 = 2;
                }
                else
                {
                    if (attackNumber == 1) enemyAttacker1 = 1;
                    else enemyAttacker2 = 1;
                }
                break;
            
            case "Royal Knight Ranged":
                if (attackNumber == 1) enemyAttacker1 = 2;
                else enemyAttacker2 = 2;
                break;
            
            case "Skull Grunt Melee":
                if (attackNumber == 1) enemyAttacker1 = 1;
                else enemyAttacker2 = 1;
                break;
            
            case "Skull Grunt Ranged":
                if (attackNumber == 1) enemyAttacker1 = 2;
                else enemyAttacker2 = 2;
                break;
            
            case "Tavern Brute":
                if (attackNumber == 1) enemyAttacker1 = 1;
                else enemyAttacker2 = 1;
                break;
            
            case "Barkeeper":
                if (attackNumber == 1) enemyAttacker1 = 2;
                else enemyAttacker2 = 2;
                break;
            
            case "Gatekeeper":
                if (attackNumber == 1) enemyAttacker1 = 3;
                else enemyAttacker2 = 3;
                break;
            
            case "Kaz 1":
                if (attackNumber == 1) enemyAttacker1 = 3;
                else enemyAttacker2 = 3;
                break;
            
            case "Kaz 2":
                if (attackNumber == 1) enemyAttacker1 = 3;
                else enemyAttacker2 = 3;
                break;
            
            case "Royal Guard 1":
                if (attackNumber == 1) enemyAttacker1 = 1;
                else enemyAttacker2 = 1;
                break;
            
            case "Royal Guard 2":
                if (attackNumber == 1) enemyAttacker1 = 2;
                else enemyAttacker2 = 2;
                break;
            
            case "Skull King":
                if (attackNumber == 1) enemyAttacker1 = 3;
                else enemyAttacker2 = 3;
                break;
            
            case "Royal King":
                if (attackNumber == 1) enemyAttacker1 = 3;
                else enemyAttacker2 = 3;
                break;
        }

        if (attackNumber == 1) enemy1stAttack = enemyAttack;
        else enemy2ndAttack = enemyAttack;
    }


    public static void PlayerAttackAnimation(string character, int targetEnemy)
    {
        // TODO: Animate Player attacks
        
        switch (targetEnemy)
        {
            case 0:
                // Attack no enemies
                break;
            
            case 1:
                // Attack Enemy 1
                break;
            
            case 2:
                // Attack Enemy 2
                break;
            
            case 3:
                // Attack Enemy 3
                break;
            
            case 4:
                // Attack Enemies 1 + 2
                break;
            
            case 5:
                // Attack Enemies 2 + 3
                break;
            
            case 6:
                // Attack Enemies 1 + 3
                break;
            
            case 7:
                // Attack All Enemies
                break;
            
            case 8:
                // Affect Netrixi
                break;
            
            case 9:
                // Affect Folkvar
                break;
            
            case 10:
                // Affect Iv
                break;
            
            case 11: 
                // Affect Everyone
                break;
        }
    }


    public static void EnemyAttackAnimation(int enemyNumber, string targetOfEnemy)
    {
        // TODO: Animate Enemy attacks
        
        switch (targetOfEnemy)
        {
            case "Netrixi":
                // Attack Netrixi
                break;
            
            case "Folkvar":
                // Attack Folkvar
                break;
            
            case "Iv":
                // Attack Iv
                break;
            
            case "All":
                // Attack everyone
                break;
            
            case "None":
                // Attack no one
                break;
            
            case "Folkvar + Iv":
                // Attack Folkvar + Iv
                break;
            
            case "Netrixi + Iv":
                // Attack Netrixi + Iv
                break;
            
            case "Folkvar + Netrixi":
                // Attack Folkvar + Netrixi
                break;
            
            case "Brute":
                // Attack Brute
                break;
            
            case "Enemy 1":
                // Affect Enemy 1
                break;
            
            case "Enemy 2":
                // Affect Enemy 2
                break;
            
            case "Enemy 3":
                // Affect Enemy 3
                break;
            
            case "All Enemies":
                // Affect all Enemies
                break;
            
            case "Everyone":
                // Affect everyone
                break;
        }
        
        
        // TODO: Animate countered Enemy attacks

        if (AttackScript.countered)
        {
            
        }
    }
}
