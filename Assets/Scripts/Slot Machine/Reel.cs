using UnityEngine;

public class Reel : MonoBehaviour
{
    [Header("Spin Settings")]
    public float maxSpinSpeed = 15f;        // Max speed
    public float acceleration = 25f;        // Speed up rate
    public float deceleration = 20f;        // Slow down rate
    public float symbolHeight = 2f;         // space between symbols
    public Sprite[] symbols;                // all symbols

    private bool isSpinning = false;
    private float currentSpeed = 0f;
    private float spinTimer = 0f;
    private float spinDuration = 0f;
    private bool slowingDown = false;

    private void Update()
    {
        if (!isSpinning) return;

        // Adjust speed 
        if (!slowingDown)
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpinSpeed, acceleration * Time.deltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);

        foreach (Transform child in transform)
        {
            child.localPosition += Vector3.down * currentSpeed * Time.deltaTime;

            // Rotating symbols from bottom to top
            if (child.localPosition.y < -symbolHeight * 1.5f)
            {
                float topY = GetHighestSymbolY() + symbolHeight;
                child.localPosition = new Vector3(0, topY, 0);
                child.GetComponent<SpriteRenderer>().sprite = symbols[Random.Range(0, symbols.Length)];
            }
        }

        spinTimer += Time.deltaTime;

        // Start slowing at the end
        if (!slowingDown && spinTimer >= spinDuration * 0.8f)
            slowingDown = true;

        // Stop completely when speed=zero
        if (slowingDown && currentSpeed <= 0.01f)
        {
            SnapToGrid();
            isSpinning = false;
        }
    }

    public void Spin(float duration)
    {
        spinDuration = duration;
        spinTimer = 0f;
        isSpinning = true;
        slowingDown = false;
        currentSpeed = 0f;
    }

    private float GetHighestSymbolY()
    {
        float maxY = float.MinValue;
        foreach (Transform child in transform)
        {
            if (child.localPosition.y > maxY)
                maxY = child.localPosition.y;
        }
        return maxY;
    }

    private void SnapToGrid()
    {
        foreach (Transform child in transform)
        {
            float newY = Mathf.Round(child.localPosition.y / symbolHeight) * symbolHeight;
            child.localPosition = new Vector3(child.localPosition.x, newY, child.localPosition.z);
        }
    }

    public Sprite GetCenterSymbol()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform child in transform)
        {
            float dist = Mathf.Abs(child.localPosition.y);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = child;
            }
        }

        return closest != null ? closest.GetComponent<SpriteRenderer>().sprite : null;
    }

    public bool IsSpinning()
    {
        return isSpinning;
    }

}
