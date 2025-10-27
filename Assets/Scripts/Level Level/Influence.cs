using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Influence : MonoBehaviour
{

    public static Influence main;

    public LineRenderer circleRenderer;
    public float startRadius = 50f;
    public float endRadius = 50f;
    public float currentRadius;
    private float xPos;
    private float yPos;
    private GameObject[] root;

    // Start is called before the first frame update
    void Start()
    {
        xPos = LevelManager.main.influenceCenter.position.x;
        yPos = LevelManager.main.influenceCenter.position.y;
        DrawCircle(startRadius);
        currentRadius = startRadius;
        root = UnityEngine.Object.FindObjectsOfType<GameObject>();
        //To reduce initial load time
        StartCoroutine(FindEndRadius());
    }

    // Update is called once per frame
    void Awake()
    {
        main = this;
    }

    public void Update()
    {
        currentRadius = startRadius * 100 + LevelManager.main.honeyGeneratedRatio * (endRadius - startRadius) / GlobalValues.main.influenceModifier;
        DrawCircle(currentRadius);
    }

    void DrawCircle(float radius)
    {
        int steps = 100;
        circleRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / (steps - 1);

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xPos + radius * xScaled;
            float y = yPos + radius * yScaled;
            float z = 0;

            Vector3 currentPosition = new Vector3(x, y, z);

            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }

    private IEnumerator FindEndRadius()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject obj in root)
        {
            if ((GlobalValues.main.plotMask | (1 << obj.layer)) == GlobalValues.main.plotMask)
            {
                if (Vector2.Distance(LevelManager.main.influenceCenter.position, obj.transform.position) > endRadius)
                {
                    endRadius = Vector2.Distance(LevelManager.main.influenceCenter.position, obj.transform.position);
                }
            }
        }
    }
}
