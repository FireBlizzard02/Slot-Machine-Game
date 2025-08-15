using UnityEngine;
using System;
using System.Collections;

public class SlotMachineController : MonoBehaviour
{
    public static event Action OnSpinComplete;

    public Reel[] reels;
    public float spinTime = 2f;
    public float delayBetweenReels = 0.5f;

    public void SpinAll()
    {
        if (!BetAndCurrencyManager.Instance.CanSpin())
        {
            Debug.Log("No Money To Continue!");
            return;
        }

        BetAndCurrencyManager.Instance.DeductBet();
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].Spin(spinTime + (i * delayBetweenReels));

            AudioManager.Instance.PlaySFX(AudioManager.Instance.spinRattle);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(spinTime + (reels.Length - 1) * delayBetweenReels);

        CheckReward();
    }

    private void CheckReward()
    {
        Sprite[] results = new Sprite[reels.Length];

        for (int i = 0; i < reels.Length; i++)
        {
            results[i] = reels[i].GetCenterSymbol();
        }

        // check for 3 symbol match
        if (results[0] == results[1] && results[1] == results[2])
        {
            Debug.Log("Big Win! 3 symbols match!");
            BetAndCurrencyManager.Instance.RewardBigWin();

            // Play big win sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.winBig);

            OnSpinComplete?.Invoke();  
            return;
        }

        // check for 2 symbol match
        if (results[0] == results[1] || results[1] == results[2] || results[0] == results[2])
        {
            Debug.Log("Small Win! 2 symbols match!");
            BetAndCurrencyManager.Instance.RewardSmallWin();

            // Play small win sound
            AudioManager.Instance.PlaySFX(AudioManager.Instance.winSmall);

            OnSpinComplete?.Invoke(); 
            return;
        }

        // lose
        Debug.Log("You Lose");

        // Play lose sound
        AudioManager.Instance.PlaySFX(AudioManager.Instance.lose);

        OnSpinComplete?.Invoke();  
    }
}
