using UnityEngine;

public class Reel : MonoBehaviour
{
    public float spinSpeed = 5f;          // Speed of reel movement
    public float symbolHeight = 2f;       // Height of one symbol sprite
    public float spinDuration = 2f;       // How long the reel spins before stopping
    public Sprite[] symbols;              // Your 4 symbol sprites

    private float spinTimer = 0f;
    private bool isSpinning = false;

    private void Update()
    {
        if (isSpinning)
        {
            foreach (Transform child in transform)
            {
                // Move symbol down
                child.localPosition += Vector3.down * spinSpeed * Time.deltaTime;

                // If it’s below the threshold, wrap it to top
                if (child.localPosition.y < -symbolHeight * 1.5f)
                {
                    float topY = GetHighestSymbolY() + symbolHeight;
                    child.localPosition = new Vector3(0, topY, 0);
                    child.GetComponent<SpriteRenderer>().sprite = symbols[Random.Range(0, symbols.Length)];
                }
            }

            spinTimer += Time.deltaTime;
            if (spinTimer >= spinDuration)
            {
                isSpinning = false;
                SnapToGrid();
            }
        }
    }

    public void Spin()
    {
        spinTimer = 0f;
        isSpinning = true;
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
}
