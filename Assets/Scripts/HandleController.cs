using UnityEngine;

public class HandleController : MonoBehaviour
{
    public SpriteRenderer handleImage;
    public Sprite handleUpSprite;
    public Sprite handleDownSprite;
    public SlotMachineController slotMachine;
    public float handleDownTime = 0.3f;

    private bool isPulled = false;

    public void PullHandle()           // Called when player pulls the handle
    {
        if (isPulled) return;
        isPulled = true;

        handleImage.sprite = handleDownSprite;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.spinStart);
        slotMachine.SpinAll();

        Invoke(nameof(ResetHandle), handleDownTime);
    }

    private void ResetHandle()      // Restores handle to default position
    {
        handleImage.sprite = handleUpSprite;
        isPulled = false;
    }
}
