using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    private Node _selectedNode;
    [SerializeField] private TowerBlueprint towerToBuild;
    [SerializeField] private NodeUI nodeUI;
    
    
    /// <summary>
    /// Checks if player can build on this node
    /// </summary>
    public bool CanBuild => towerToBuild != null;

    /// <summary>
    /// Checks if player has enough money
    /// </summary>
    public bool HasMoney => PlayerStats.Coins >= towerToBuild.baseBuildCost;

    /// <summary>
    /// Used to select the node
    /// </summary>
    /// <param name="node"></param>
    public void SelectNode(Node node)
    {
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }
        _selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    /// <summary>
    /// Used to deselect the node
    /// </summary>
    public void DeselectNode()
    {
        _selectedNode = null;
        nodeUI.HideUI();
    }
    
    /// <summary>
    /// used to assign what tower to build on this node
    /// </summary>
    /// <param name="tower"></param>
    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
        DeselectNode();
    }

    /// <summary>
    /// Used to get information on what tower is stored in towerToBuild variable
    /// </summary>
    /// <returns></returns>
    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
