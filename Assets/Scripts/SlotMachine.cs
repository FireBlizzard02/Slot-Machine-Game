using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    public Reel[] reels;

    public void SpinAll()
    {
        foreach (var reel in reels)
        {
            reel.Spin();
        }
    }
}
