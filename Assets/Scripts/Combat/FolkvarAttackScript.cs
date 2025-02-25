using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolkvarAttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DoesFolkvarAttack(int playerAttack, int attackNumber)
    {
        // Check if Folkvar is still alive
        if (!CombatManagerScript.folkvarAlive)
        {
            // Skip the attack
            if (attackNumber == 1) CombatSimulationScript.attack1Delay = DamageValues.standardDelay;
            else CombatSimulationScript.attack2Delay = DamageValues.standardDelay;
            return;
        }
        else
        {
            float original;
            float burnRate;
            
            
            // ATTACK 1
            // Folkvar's Swing Sword
            if (playerAttack == 4)
            {
                original = DamageValues.swingSword * AttackScript.damageModifier;
                burnRate = DamageValues.swingSwordBurn;
                AttackScript.delayRate = DamageValues.swingSwordDelay;
                int damageValue = (int) original;

                // Chance of a Critical Strike
                int randomValue = UnityEngine.Random.Range(0, DamageValues.critChance);

                // Attack the first enemy
                if (CombatManagerScript.enemy1Alive)
                {
                    // TODO: Play Folkvar Swing Sword animation
                    
                    if (randomValue <= 1)
                    {
                        // Critical strike
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 2, burnRate);

                        print("Critical strike!");
                    }
                    else
                    {
                        // Regular attack
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                    }
                }
                else
                {
                    // Attack the second enemy
                    if (CombatManagerScript.enemy2Alive)
                    {
                        // TODO: Play Folkvar Swing Sword animation
                        
                        if (randomValue <= 1)
                        {
                            // Critical strike
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 2, burnRate);
                            
                            print("Critical strike!");
                        }
                        else
                        {
                            // Regular attack
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                        }
                    }
                    else
                    {
                        // Attack the third enemy
                        if (CombatManagerScript.enemy3Alive)
                        {
                            // TODO: Play Folkvar Swing Sword animation
                            
                            if (randomValue <= 1)
                            {
                                // Critical strike
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 2, burnRate);
                                
                                print("Critical strike!");
                            }
                            else
                            {
                                // Regular attack
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                            }
                        }
                    }
                }
            }
            
            
            // ATTACK 2
            // Folkvar's Holy Smite
            if (playerAttack == 5)
            {
                original = DamageValues.smite * AttackScript.damageModifier;
                burnRate = DamageValues.smiteBurn;
                AttackScript.delayRate = DamageValues.smiteDelay;
                int damageValue = (int) original;

                int lowestEnemyHP = 500;
                int targetEnemy = 0;

                // Determine which enemy has the lowest HP;
                if (CombatManagerScript.enemy1Alive)
                {
                    targetEnemy = 1;
                    lowestEnemyHP = CombatManagerScript.enemy1HP;
                }
                if (CombatManagerScript.enemy2Alive)
                    if (CombatManagerScript.enemy2HP < lowestEnemyHP)
                    {
                        targetEnemy = 2;
                        lowestEnemyHP = CombatManagerScript.enemy2HP;
                    }
                if (CombatManagerScript.enemy3Alive)
                    if (CombatManagerScript.enemy3HP < lowestEnemyHP)
                    {
                        targetEnemy = 3;
                        lowestEnemyHP = CombatManagerScript.enemy3HP;
                    }


                switch (targetEnemy)
                {
                    // Enemy 1
                    case 1:
                        if (lowestEnemyHP <= CombatManagerScript.enemy1StartingHP * DamageValues.executeThreshold)
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            
                            // Enemy is below the threshold for Execution
                            HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 2, burnRate);

                            print("Enemy 1 will be executed");
                        }
                        else
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            
                            // Enemy is not below the threshold for Execution
                            HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                        }
                        break;
                    
                    // Enemy 2
                    case 2:
                        if (lowestEnemyHP <= CombatManagerScript.enemy2StartingHP * DamageValues.executeThreshold)
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            
                            // Enemy is below the threshold for Execution
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 2, burnRate);
                            
                            print("Enemy 2 will be executed");
                        }
                        else
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            
                            // Enemy is not below the threshold for Execution
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                        }
                        break;
                    
                    // Enemy 3
                    case 3:
                        if (lowestEnemyHP <= CombatManagerScript.enemy3StartingHP * DamageValues.executeThreshold)
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            
                            // Enemy is below the threshold for Execution
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 2, burnRate);
                            
                            print("Enemy 3 will be executed");
                        }
                        else
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            
                            // Enemy is not below the threshold for Execution
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                        }
                        break;
                }
            }
            
            
            // ATTACK 3
            // Folkvar's Grand Slam
            if (playerAttack == 6)
            {
                original = DamageValues.grandSlam * AttackScript.damageModifier;
                burnRate = DamageValues.grandSlamBurn;
                AttackScript.delayRate = DamageValues.grandSlamDelay;
                int damageValue = (int) original;

                // If Enemy 1 is alive
                if (CombatManagerScript.enemy1Alive)
                {
                    // If Enemy 2 is alive
                    if (CombatManagerScript.enemy2Alive)
                    {
                        // If Enemy 2 is close to Enemy 1
                        if (EnemyManagerScript.enemy1Position + 1 == EnemyManagerScript.enemy2Position)
                        {
                            // If Enemy 3 is alive
                            if (CombatManagerScript.enemy3Alive)
                            {
                                // If Enemy 3 is close to Enemies 1 + 2
                                if (EnemyManagerScript.enemy2Position + 1 == EnemyManagerScript.enemy3Position)
                                {
                                    // TODO: Play Folkvar Grand Slam animation
                                    
                                    // Attack Enemy 2 and deal splash damage to Enemies 1 + 3
                                    HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                                    HealthManagerScript.ChangeHealth("Enemy 1", damageValue / 2, burnRate);
                                    HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);
                                }
                                else
                                {
                                    // TODO: Play Folkvar Grand Slam animation
                                    
                                    // Attack Enemy 1 and deal splash damage to Enemy 2
                                    HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                                    HealthManagerScript.ChangeHealth("Enemy 2", damageValue / 2, burnRate);
                                }
                            }
                            else
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                    
                                // Attack Enemy 1 and deal splash damage to Enemy 2
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue / 2, burnRate);
                            }
                        }
                        else
                        {
                            // If Enemy 2 is close to Enemy 3
                            if (EnemyManagerScript.enemy2Position + 1 == EnemyManagerScript.enemy3Position)
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                    
                                // Attack Enemy 2 and deal splash damage to Enemy 3
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);
                            }
                            else
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                    
                                // Attack Enemy 1 and deal no splash damage to other enemies
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            }
                        }
                    }
                    else
                    {
                        // If Enemy 3 is alive
                        if (CombatManagerScript.enemy3Alive)
                        {
                            // If Enemy 1 is close to Enemy 3
                            if (EnemyManagerScript.enemy1Position + 1 == EnemyManagerScript.enemy3Position)
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                    
                                // Attack Enemy 1 and deal splash damage to Enemy 3
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);
                            }
                            else
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                    
                                // Attack Enemy 1 and deal no splash damage to other enemies
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            }
                        }
                        else
                        {
                            // TODO: Play Folkvar Grand Slam animation
                                    
                            // Attack Enemy 1 and deal no splash damage to other enemies
                            HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                        }
                    }
                }
                
                // If Enemy 1 isn't alive
                else
                {
                    // If Enemy 2 is alive
                    if (CombatManagerScript.enemy2Alive)
                    {
                        // If Enemy 2 is close to Enemy 3
                        if (EnemyManagerScript.enemy2Position + 1 == EnemyManagerScript.enemy3Position)
                        {
                            // TODO: Play Folkvar Grand Slam animation
                                    
                            // Attack Enemy 2 and deal splash damage to Enemy 3
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);
                        }
                        else
                        {
                            // TODO: Play Folkvar Grand Slam animation
                                    
                            // Attack Enemy 2 and deal no splash damage to other enemies
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                        }
                    }
                    else
                    {
                        // TODO: Play Folkvar Grand Slam animation
                                    
                        // Attack Enemy 3 and deal no splash damage to other enemies
                        HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                    }
                }
            }
        }
    }
}
