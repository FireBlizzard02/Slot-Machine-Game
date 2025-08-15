using UnityEngine;

public class Reel : MonoBehaviour
{
    public float spinSpeed = 5f;
    public float symbolHeight = 2f;
    public Sprite[] symbols;

    private bool isSpinning = false;
    private float spinTimer = 0f;
    private float spinDuration = 0f;

    private void Update()
    {
        if (isSpinning)
        {
            foreach (Transform child in transform)
            {
                child.localPosition += Vector3.down * spinSpeed * Time.deltaTime;

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

    public void Spin(float duration)
    {
        spinTimer = 0f;
        spinDuration = duration;
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

        if (closest != null)
            return closest.GetComponent<SpriteRenderer>().sprite;

        return null;
    }

}