using UnityEngine;
using System.Collections;

public class SlotMachineController : MonoBehaviour
{
    public Reel[] reels;
    public float spinTime = 2f;
    public float delayBetweenReels = 0.5f;

    public void SpinAll()
    {
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].Spin(spinTime + (i * delayBetweenReels));
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
            return;
        }

        // check for 2 symbol match
        if (results[0] == results[1] || results[1] == results[2] || results[0] == results[2])
        {
            Debug.Log("Small Win! 2 symbols match!");
            return;
        }

        // lose
        Debug.Log("You Lose");
    }

}
