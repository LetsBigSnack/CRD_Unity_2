using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int health;
    [SerializeField] private int collectedCoins;
    
    public int GetHealth() => health;

    public void DecreaseHealth(int decreaseBy) => health -= decreaseBy;
    public void IncreaseHealth(int increaseBy) => health += increaseBy;
    
    public int GetCollectedCoins() => collectedCoins;

    public void DecreaseCollectedCoins(int decreaseBy) => collectedCoins -= decreaseBy;
    public void IncreaseCollectedCoins(int increaseBy) => collectedCoins += increaseBy;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            return;
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Collected Coins: " + collectedCoins);
        Debug.Log("Current Health: " + health);
    }
}
