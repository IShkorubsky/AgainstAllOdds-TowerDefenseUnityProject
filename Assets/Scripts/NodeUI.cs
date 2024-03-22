using TMPro;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private GameObject myUI;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private TextMeshProUGUI sellForGoldAmountText;
    
    private Node _target;
    
    /// <summary>
    /// Used to set the target node where the player wants to build a tower + set the cost of the upgrades on the upgrade button
    /// </summary>
    /// <param name="targetNode"></param>
    public void SetTarget(Node targetNode)
    {
        _target = targetNode;

        transform.position = _target.GetBuildPosition();

        switch (_target.towerCurrentLevel)
        {
            case 1:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                upgradeButton.SetActive(true);
                upgradeCostText.text = _target.towerBlueprint.upgradeCostLevel2.ToString();
                sellForGoldAmountText.text = Mathf.RoundToInt(_target.towerBlueprint.baseBuildCost * 0.5f).ToString();
                break;
            case 2:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                upgradeButton.SetActive(true);
                upgradeCostText.text = _target.towerBlueprint.upgradeCostLevel3.ToString();
                sellForGoldAmountText.text = Mathf.RoundToInt((_target.towerBlueprint.baseBuildCost + _target.towerBlueprint.upgradeCostLevel2) * 0.5f).ToString();
                break;
            case 3:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                upgradeButton.SetActive(false);
                sellForGoldAmountText.text = Mathf.RoundToInt((_target.towerBlueprint.baseBuildCost + _target.towerBlueprint.upgradeCostLevel3) * 0.5f).ToString();
                break;
        }
        
        myUI.SetActive(true);
    }

    /// <summary>
    /// Used to Hide the UI
    /// </summary>
    public void HideUI()
    {
        myUI.SetActive(false);
        upgradeButton.SetActive(true);
    }

    /// <summary>
    /// Used to upgrade the tower level on the target node
    /// </summary>
    public void Upgrade()
    {
        _target.UpgradeTower();
        BuildManager.Instance.DeselectNode();
    }

    /// <summary>
    /// Used to sell the tower on the target node
    /// </summary>
    public void Sell()
    {
        _target.SellTower();
        BuildManager.Instance.DeselectNode();
    }
}
