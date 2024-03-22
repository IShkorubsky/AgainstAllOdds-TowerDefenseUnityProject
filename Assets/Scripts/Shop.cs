using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]private TowerBlueprint arrowTower;
    [SerializeField]private TowerBlueprint cannonTower;
    [SerializeField]private TowerBlueprint mageTower;

    [SerializeField]private TextMeshProUGUI arrowTurretCostText;
    [SerializeField]private TextMeshProUGUI cannonTurretCostText;
    [SerializeField]private TextMeshProUGUI mageTurretCostText;
    
    [SerializeField]private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
        arrowTurretCostText.text = arrowTower.baseBuildCost.ToString();
        cannonTurretCostText.text = cannonTower.baseBuildCost.ToString();
        mageTurretCostText.text = mageTower.baseBuildCost.ToString();
    }

    /// <summary>
    /// Used to select the arrow tower
    /// </summary>
    public void SelectArrowTower()
    {
        buildManager.SelectTowerToBuild(arrowTower);
    }
    
    /// <summary>
    /// Used to select cannon tower
    /// </summary>
    public void SelectCannonTower()
    {
        buildManager.SelectTowerToBuild(cannonTower);
    }
    
    /// <summary>
    /// Used to select mage tower
    /// </summary>
    public void SelectMageTower()
    {
        buildManager.SelectTowerToBuild(mageTower);
    }
}
