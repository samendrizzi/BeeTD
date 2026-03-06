using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public enum Priority
{
    VERYLOW,
    LOW,
    NORMAL,
    HIGH,
    VERYHIGH
}

public class Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite fogSprite;
    [SerializeField] private Sprite honeySprite;
    [SerializeField] private Color hoverColor;

    [Header("Attributes")]
    [SerializeField] public FlowerType flowerType = FlowerType.EMPTY;
    [SerializeField] public bool isHive;
    [SerializeField] public bool isBuildable;
    [SerializeField] public bool isResourceNode;
    [SerializeField] public bool visionObstruction;
    [SerializeField] public bool isDestructible;
    [SerializeField] public float health = 0f;
    [SerializeField] public float armor = 0f;
    [SerializeField] private GameObject noObstructionPrefab;

    //trackers
    public GameObject UIObject;
    private Sprite originalSprite;
    public GameObject towerObj;
    public StructureUIHandler UI;
    private Color startColor;
    public bool fog = true;
    private Color originalColor;
    private Color fogColor = Color.grey;
    private bool isDestroyed = false;
    public bool isSapped = false;
    public bool hasBloomed = false;
    public float pause = 0f;
    public int honeyTicks = 0;
    public float honeyPerTick = 0f;
    public Priority priority = Priority.NORMAL;

    private void Awake()
    {
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
        else
        {
            fog = true;
            sr.sprite = fogSprite;
            sr.color = fogColor;
            startColor = fogColor;
        }
    }

    private void Update()
    {
        if (pause > 0)
        {
            pause -= Time.deltaTime;
            if (pause <= 0 && isSapped)
            {
                UnsapFlower();
            }
        }
        if (honeyTicks > 0 && pause <= 0)
        {
            pause = 1f;
            LevelManager.main.IncreaseHoney(honeyPerTick);
            honeyTicks--;
            if (honeyTicks <= 0)
            {
                HoneyEmpty();
            }
        }
    }

    private void CreateUI()
    {
        UIObject = Instantiate(GlobalValues.main.UIPrefab, gameObject.transform);
        UI = UIObject.GetComponent<StructureUIHandler>();
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
        if (fog == true || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        OpenUI();
    }

    public void OpenUI()
    {
        if (UIObject == null && (isBuildable || isResourceNode))
        {
            CreateUI();
        }
        else if (UIObject != null)
        {
            UI.OpenUI();
        }
    }

    public void CloseUI()
    {
        if (UI != null)
        {
            UI.CloseUI();
        }
        else
        {
            Debug.Log(gameObject.name + "doesn't have UI to close");
        }
    }

    public IEnumerator RevealFog(float range, bool ignoreObstructions)
    {
        if (range != 0f)
        {
            //find plots
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, GlobalValues.main.plotMask | GlobalValues.main.flowerMask);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.GetComponent<Plot>().fog == true)
                    {
                        if (!Physics2D.Linecast(transform.position, hits[i].transform.position, GlobalValues.main.obstructionMask) || ignoreObstructions)
                        {
                            hits[i].transform.GetComponent<Plot>().Found();
                        }
                    }
                }
            }
            //find obstructions
            hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, GlobalValues.main.obstructionMask);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.GetComponent<Plot>().fog == true)
                    {
                        hits[i].transform.GetComponent<Plot>().Found();
                    }
                }
            }
        }
        yield return new WaitForSeconds(0f);
    }

    public void Build(GameObject towerPrefab)
    {
        int targetingIndex = 0;
        string targetSetting = GlobalValues.main.targetingOptionDefault;
        if (towerObj != null)
        {
            CloseUI();
            //Save Tower Info
            targetingIndex = towerObj.GetComponent<Attributes>().targetingIndex;
            targetSetting = towerObj.GetComponent<Attributes>().targetSetting;
            //Remove Old Tower
            towerObj.GetComponent<Attributes>().isDestroyed = true;
            Destroy(towerObj);
        }
        else
        {
            CloseUI();
        }

        towerObj = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        //pass info to new tower
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
            PollenManager.main.AddFlower(gameObject);
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
            newPlot.GetComponent<Plot>().Found();
            Destroy(gameObject);
        }
    }

    public void SapFlower(float duration)
    {
        if ((((1 << gameObject.layer) & GlobalValues.main.flowerMask) != 0))
        {
            isSapped = true;
            sr.sprite = fogSprite;
            pause += duration / BuffManager.main.nectarGenerationRate;
        }
        else
        {
            Debug.Log("Trying to Sap flower that is not a flower");
        }
    }

    public void UnsapFlower()
    {
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

    public void HoneyFill()
    {
        honeyTicks = BuffManager.main.honeycombTicks;
        honeyPerTick = BuffManager.main.honeycombGeneration;
        sr.sprite = honeySprite;
    }

    public void HoneyEmpty()
    {
        honeyTicks = 0;
        honeyPerTick = 0f;
        sr.sprite = originalSprite;
    }

    public void GetAllHoney()
    {
        if (honeyTicks > 0)
        {
            LevelManager.main.IncreaseHoney(honeyPerTick * honeyTicks);
        }
        HoneyEmpty();
    }

    public void ChangeResourcePriority()
    {
        if ((int)priority >= Priority.GetValues(typeof(Priority)).Length - 1)
        {
            priority = (Priority)0;
        }
        else
        {
            priority = (Priority)((int)priority + 1);
        }
        LevelManager.main.OrganizeBees();
    }
}
