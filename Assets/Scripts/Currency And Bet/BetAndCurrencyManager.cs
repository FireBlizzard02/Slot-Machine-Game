using UnityEngine;

public class BetAndCurrencyManager : MonoBehaviour
{
    public static BetAndCurrencyManager Instance; 

    [Header("Currency Settings")]
    public int startingBalance = 1000;
    public int currentBalance;

    [Header("Bet Settings")]
    public int currentBet = 0;
    public int smallWinMultiply = 2; 
    public int bigWinMultiply = 5;   
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentBalance = startingBalance;
    }

    public bool CanSpin()
    {
        return currentBalance >= currentBet;
    }

    public void DeductBet()
    {
        currentBalance -= currentBet;
        Debug.Log($"Bet placed: {currentBet}. Balance now: {currentBalance}");
    }

    public void RewardSmallWin()
    {
        int reward = currentBet * smallWinMultiply;
        currentBalance += reward;
        Debug.Log($"Small win! +{reward}. Balance now: {currentBalance}");
    }

    public void RewardBigWin()
    {
        int reward = currentBet * bigWinMultiply;
        currentBalance += reward;
        Debug.Log($"BIG WIN! +{reward}. Balance now: {currentBalance}");
    }
}
