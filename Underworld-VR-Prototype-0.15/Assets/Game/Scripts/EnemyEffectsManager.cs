using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectsManager : MonoBehaviour {

    private GameManager gameManager;

    public float[] enemyEffectsProbability; // the probability to pick one of the effets after each round
    //public float[] enemyEffects; // the total float of effects for each effect
    //public float[] enemyEffectsAmount; // the amount added to each effect
    public int randomEffectInt = 4;

    private int round = 0;
    // 0 = more enemy health
    // 1 = more enemy damage
    // 2 = better enemy accuracy
    // 3 = better fire rate
    // 4 = new enemy type

    public float addEnemyHealth;
    public float addEnemyDamage;
    public float addEnemyAccuracy;
    public float addEnemyFireRate;

    public float addEnemyHealthAmount;
    public float addEnemyDamageAmount;
    public float addEnemyAccuracyAmount;
    public float addEnemyFireRateAmount;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyEffectsProbability = new float[5];
        CheckRound();
        ApplyEnemyEffects();
        //enemyEffectsProbability = new float[5];
        //enemyEffectsProbability = new float[5];
    }

    // Update is called once per frame
    void Update () {

        if (round != gameManager.roundCurrent)
        {
            round = gameManager.roundCurrent;
            CheckRound();
            ApplyEnemyEffects();
        }
		
	}

    void ApplyEnemyEffects()
    {
        // gets int of random effect to apply
        randomEffectInt = Mathf.RoundToInt(EnemyEffectProbability(enemyEffectsProbability));
        //enemyEffects[randomEffect] += enemyEffectsAmount[randomEffect];

        //print("Enemy effects manager: " + randomEffectInt);


        switch (randomEffectInt)
        {
            case 0: addEnemyHealth += addEnemyHealthAmount;
                break;
            case 1: addEnemyDamage += addEnemyDamageAmount;
                break;
            case 2: addEnemyAccuracy += addEnemyAccuracyAmount;
                break;
            case 3: addEnemyFireRate += addEnemyFireRateAmount;
                break;
        }
        
    }

    void CheckRound()
    {
        // Resets enemy effects probabilities
        ResetEnemyEffectsProbability();

        switch (gameManager.roundCurrent)
        {
            case 0:
                enemyEffectsProbability[4] = 100;
                break;
            case 1:
                enemyEffectsProbability[4] = 100;
                break;
            case 2:
                enemyEffectsProbability[0] = 25;
                enemyEffectsProbability[1] = 25;
                enemyEffectsProbability[2] = 25;
                enemyEffectsProbability[3] = 25;
                break;
            case 3:
                enemyEffectsProbability[4] = 100;
                break;
            case 4:
                enemyEffectsProbability[0] = 25;
                enemyEffectsProbability[1] = 25;
                enemyEffectsProbability[2] = 25;
                enemyEffectsProbability[3] = 25;
                break;
            case 5:
                enemyEffectsProbability[0] = 25;
                enemyEffectsProbability[1] = 25;
                enemyEffectsProbability[2] = 25;
                enemyEffectsProbability[3] = 25;
                break;
            case 6:
                enemyEffectsProbability[4] = 100;
                break;
            case 7:
                enemyEffectsProbability[0] = 25;
                enemyEffectsProbability[1] = 25;
                enemyEffectsProbability[2] = 25;
                enemyEffectsProbability[3] = 25;
                break;
            case 8:
                enemyEffectsProbability[4] = 100;
                break;
            case 9:
                enemyEffectsProbability[0] = 25;
                enemyEffectsProbability[1] = 25;
                enemyEffectsProbability[2] = 25;
                enemyEffectsProbability[3] = 25;
                break;
            case 10:
                enemyEffectsProbability[4] = 100;
                break;
        }
        

    }


    float EnemyEffectProbability(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint <= probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }

    void ResetEnemyEffectsProbability()
    {
        enemyEffectsProbability[0] = 0; // 
        enemyEffectsProbability[1] = 0; // 
        enemyEffectsProbability[2] = 0; // 
        enemyEffectsProbability[3] = 0; // 
        enemyEffectsProbability[4] = 0; // 
    }
}
