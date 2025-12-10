using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite fogSprite;
    [SerializeField] private Color hoverColor;

    [Header("Attributes")]
    [SerializeField] public bool isHive;
    [SerializeField] public bool isBuildable;
    [SerializeField] public bool isResourceNode;
    [SerializeField] public bool visionObstruction;
    [SerializeField] public bool isDestructible;
    [SerializeField] public float health;
    [SerializeField] public float armor = 0f;
    [SerializeField] private GameObject noObstructionPrefab;

    private Sprite originalSprite;
    public GameObject towerObj;
    public StructureUIHandler plotUI;
    public StructureUIHandler towerUI;
    private Color startColor;
    public bool fog = true;
    private Color originalColor;
    private Color fogColor = Color.grey;
    private bool isDestroyed = false;
    public bool isSapped = false;
    public bool hasBloomed = false;

    private void Start()
    {
        plotUI = gameObject.GetComponent<StructureUIHandler>();
        originalColor = sr.color;
        startColor = sr.color;
        if (hasBloomed == false)
        {
            originalSprite = sr.sprite;
        }
        if (isHive == true)
        {
            fog = false;
        }
        if (fog == true)
        {
            if (Vector2.Distance(transform.position, LevelManager.main.influenceCenter.position) <= GlobalValues.main.startingRadius)
            {
                fog = false;
                sr.sprite = originalSprite;
                Found();
            }
            else
            {
                sr.sprite = fogSprite;
                sr.color = fogColor;
                startColor = fogColor;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (!(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) || isBuildable == false || fog == true)
        {
            return;
        }

        if (towerObj != null)
        {
            towerUI.OpenUI();
            return;
        }
        else 
        {
            plotUI.OpenUI();
        }
    }

    public void Build(GameObject towerPrefab)
    {
        int targetingIndex = 0;
        string targetSetting = GlobalValues.main.targetingOptionDefault;
        if (towerObj != null)
        {
            towerUI.CloseUI();
            //Save Tower Info
            targetingIndex = towerObj.GetComponent<Attributes>().targetingIndex;
            targetSetting = towerObj.GetComponent<Attributes>().targetSetting;
            //Remove Old Tower
            towerObj.GetComponent<Attributes>().isDestroyed = true;
            Destroy(towerObj);
        }
        else
        {
            plotUI.CloseUI();
        }

        towerObj = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        towerUI = towerObj.GetComponent<StructureUIHandler>();
        //pass info to new tower
        //towerUI.OpenUI();
        towerObj.GetComponent<Attributes>().targetingIndex = targetingIndex;
        towerObj.GetComponent<Attributes>().targetSetting = targetSetting;
    }

    public void Found()
    {
        if (originalSprite == null)
        {
            originalSprite = sr.sprite;
        }
        sr.sprite = originalSprite;
        sr.color = originalColor;
        startColor = originalColor;
        if (fog == true && (((1 << gameObject.layer) & GlobalValues.main.flowerMask) != 0))
        {
            LevelManager.main.FoundFlower(gameObject);
        }
        fog = false;
    }

    public void Hit(float dmg, float armorPierce)
    {
        if (isDestructible == true && isDestroyed == false)
        {
            float armorBlock = (armor - armorPierce) / 100f;
            if (armorBlock < 0)
            {
                armorBlock = 0f;
            }
            health -= (1f - armorBlock) * dmg;
            if (health <= 0)
            {
                Destruct();
            }
        }
    }

    public void Destruct()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            GameObject newPlot = Instantiate(noObstructionPrefab, transform.position, Quaternion.identity);
            newPlot.GetComponent<Plot>().fog = false;
            Destroy(gameObject);
        }
    }

    public void SapFlower(float duration)
    {
        if ((((1 << gameObject.layer) & GlobalValues.main.flowerMask) != 0))
        {
            isSapped = true;
            sr.sprite = fogSprite;
            StartCoroutine(UnsapFlower(duration));
        }
        else
        {
            Debug.Log("Trying to Sap flower that is not a flower");
        }
    }

    private IEnumerator UnsapFlower(float duration)
    {
        yield return new WaitForSeconds(duration);
        isSapped = false;
        if (fog == false)
        {
            sr.sprite = originalSprite;
        }
    }

    public void Bloom (int index)
    {
        hasBloomed = true;
        gameObject.GetComponent<Identify>().ID = index;
        originalSprite = GlobalValues.main.FLOWERSprite[index];
        if (fog == false)
        {
            sr.sprite = originalSprite;
        }
    }

}
