using UnityEngine;

public class Reel : MonoBehaviour
{
    [Header("Spin Settings")]
    public float maxSpinSpeed = 15f;        // Peak speed while spinning
    public float acceleration = 25f;        // How quickly the reel speeds up
    public float deceleration = 20f;        // How quickly the reel slows down
    public float symbolHeight = 2f;         // Distance between symbols
    public Sprite[] symbols;                // All possible symbols

    private bool isSpinning = false;
    private float currentSpeed = 0f;        // Current spin speed
    private float spinTimer = 0f;           // Time spent spinning
    private float spinDuration = 0f;        // Total spin time before stopping

    private bool slowingDown = false;       // Flag to start deceleration

    private void Update()
    {
        if (!isSpinning) return;

        // Smooth acceleration at start
        if (!slowingDown)
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpinSpeed, acceleration * Time.deltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);

        // Move each symbol
        foreach (Transform child in transform)
        {
            child.localPosition += Vector3.down * currentSpeed * Time.deltaTime;

            // If symbol goes off the bottom, move it to the top with a new random symbol
            if (child.localPosition.y < -symbolHeight * 1.5f)
            {
                float topY = GetHighestSymbolY() + symbolHeight;
                child.localPosition = new Vector3(0, topY, 0);
                child.GetComponent<SpriteRenderer>().sprite = symbols[Random.Range(0, symbols.Length)];
            }
        }

        // Track total spin time
        spinTimer += Time.deltaTime;

        // Start slowing down before final stop
        if (!slowingDown && spinTimer >= spinDuration * 0.8f)
            slowingDown = true;

        // When speed reaches zero, snap to grid and stop completely
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
        currentSpeed = 0f; // start from zero for acceleration effect
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
}
