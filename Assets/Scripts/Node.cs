using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Color notEnoughCoinsColor;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private BuildManager buildManager;
    [SerializeField] private AudioSource buildTowerSfx;
    [SerializeField] private GameObject buildFX;
    [SerializeField] private GameObject sellFX;
    
    private Material _startMaterial;
    private Renderer _thisRenderer;
    
    public GameObject tower;
    public int towerCurrentLevel;
    public TowerBlueprint towerBlueprint;
    private void Start()
    {
        buildManager = BuildManager.Instance;
        _thisRenderer = GetComponent<Renderer>();
        _startMaterial = _thisRenderer.material;
    }

    /// <summary>
    /// Used to get a Vector3 that gets the information of where the tower should be built
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    /// <summary>
    /// Handles what happens when mouse enters the collider of the node
    /// </summary>
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            _thisRenderer.material = hoverMaterial;
        }
        else if(!buildManager.HasMoney)
        {
            _thisRenderer.material.color = notEnoughCoinsColor;
        }

    }

    /// <summary>
    /// Handles what happens on mouse left button click on the node
    /// </summary>
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (tower != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        
        if (!buildManager.CanBuild)
        {
            return;
        }
        
        BuildTower(buildManager.GetTowerToBuild());
    }

    /// <summary>
    /// Used to build the tower on this node
    /// </summary>
    /// <param name="towerBlueprint"></param>
    private void BuildTower(TowerBlueprint towerBlueprint)
    {
        if (PlayerStats.Coins < towerBlueprint.baseBuildCost)
        {
            return;
        }

        if (buildManager.GetTowerToBuild() == null)
        {
            return;
        }
        
        buildTowerSfx.Play();
        PlayerStats.Coins -= towerBlueprint.baseBuildCost;
        
        var tower = Instantiate(towerBlueprint.level1, GetBuildPosition(), Quaternion.identity);
        this.tower = tower;
        this.towerBlueprint = towerBlueprint;
        towerCurrentLevel = 1;
        var tempBuildFX = Instantiate(buildFX, GetBuildPosition(), Quaternion.identity);
        Destroy(tempBuildFX,1.5f);
    }

    /// <summary>
    /// Used to upgrade the level of the current tower built on the node
    /// </summary>
    public void UpgradeTower()
    {
        switch (towerCurrentLevel)
        {
            case 1 when PlayerStats.Coins < towerBlueprint.upgradeCostLevel2:
                return;
            case 1:
            {
                buildTowerSfx.Play();
                PlayerStats.Coins -= towerBlueprint.upgradeCostLevel2;
        
                Destroy(this.tower);
        
                var tower = Instantiate(towerBlueprint.level2, GetBuildPosition(), Quaternion.identity);
                this.tower = tower;
                towerCurrentLevel = 2;
                var tempBuildFX = Instantiate(buildFX, GetBuildPosition(), Quaternion.identity);
                Destroy(tempBuildFX,1.5f);
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }
            case 2 when PlayerStats.Coins < towerBlueprint.upgradeCostLevel3:
                return;
            case 2:
            {
                buildTowerSfx.Play();
                PlayerStats.Coins -= towerBlueprint.upgradeCostLevel3;
        
                Destroy(this.tower);
        
                var tower = Instantiate(towerBlueprint.level3, GetBuildPosition(), Quaternion.identity);
                this.tower = tower;
                towerCurrentLevel = 3;
                var tempBuildFX = Instantiate(buildFX, GetBuildPosition(), Quaternion.identity);
                Destroy(tempBuildFX,1.5f);
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }
        }
    }

    /// <summary>
    /// Used to handle the selling tower mechanic that destroys the turret and gives the player some coins back based on the tower value
    /// </summary>
    public void SellTower()
    {
        switch (towerCurrentLevel)
        {
            case 1:
                PlayerStats.Coins += Mathf.RoundToInt(towerBlueprint.baseBuildCost * 0.5f);
                break;
            case 2:
                PlayerStats.Coins += Mathf.RoundToInt((towerBlueprint.baseBuildCost + towerBlueprint.upgradeCostLevel2) * 0.5f);
                break;
            case 3:
                PlayerStats.Coins += Mathf.RoundToInt((towerBlueprint.baseBuildCost + towerBlueprint.upgradeCostLevel3) * 0.5f);
                break;
        }

        Destroy(tower);
        var tempSellFX = Instantiate(sellFX, transform.position, Quaternion.identity);
        Destroy(tempSellFX,2f);
        towerBlueprint = null;
    }

    /// <summary>
    /// Handles what happens when the mouse cursor exits the node collider
    /// </summary>
    private void OnMouseExit()
    {
        _thisRenderer.material = _startMaterial;
        _thisRenderer.material.color = Color.white;
    }
}
