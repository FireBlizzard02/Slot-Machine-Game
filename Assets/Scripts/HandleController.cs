using UnityEngine;
using UnityEngine.UI;

public class HandleController : MonoBehaviour
{
    public SpriteRenderer handleImage;              
    public Sprite handleUpSprite;
    public Sprite handleDownSprite;

    public SlotMachineController slotMachine; 

    public float handleDownTime = 0.3f;      

    private bool isPulled = false;

    public void PullHandle()
    {
        if (isPulled) return;
        isPulled = true;

        handleImage.sprite = handleDownSprite;

        slotMachine.SpinAll();

        Invoke(nameof(ResetHandle), handleDownTime );
    }

    private void ResetHandle()
    {
        handleImage.sprite = handleUpSprite;
        isPulled = false;
    }
}
