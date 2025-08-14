using UnityEngine;
using UnityEngine.Events;

public class HandleController : MonoBehaviour
{
    public SpriteRenderer handleRenderer;
    public Sprite handleUpSprite;
    public Sprite handleDownSprite;
    public UnityEvent OnHandlePulled; // Assigned in Inspector

    private bool isPulled = false;

    private void OnMouseDown()
    {
        if (!isPulled)
        {
            isPulled = true;
            handleRenderer.sprite = handleDownSprite;

            // Trigger slot machine
            OnHandlePulled?.Invoke();
        }
    }

    private void OnMouseUp()
    {
        handleRenderer.sprite = handleUpSprite;
        isPulled = false;
    }
}
