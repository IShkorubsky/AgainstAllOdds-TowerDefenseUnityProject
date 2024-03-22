using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 savedPosition;
    [SerializeField] private RectTransform buildUI;
    [SerializeField] private Texture2D[] cursorChoice;
    
    private RectTransform _buildUIInstance;
    
    private bool _uiIsOpen;

    private void Start()
    {
        savedPosition = buildUI.position;
    }

    private void Update()
    {
        if (!_uiIsOpen)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MoveInBuildUI();
            }
        }
        else if(_uiIsOpen)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MoveOutBuildUI();
            }
        }
    }

    /// <summary>
    /// Used to move in the Building UI to mouse position
    /// </summary>
    private void MoveInBuildUI()
    {
        mousePosition = Input.mousePosition;
        buildUI.position = mousePosition;
        _uiIsOpen = true;
    }

    /// <summary>
    /// Used to move the building UI to a saved position
    /// </summary>
    public void MoveOutBuildUI()
    {
        buildUI.position = savedPosition;
        _uiIsOpen = false;
    }

    /// <summary>
    /// Used to change the mouse cursor to the arrow icon
    /// </summary>
    public void ChooseArrowCursor()
    {
        Cursor.SetCursor(cursorChoice[0], Vector2.zero, CursorMode.Auto);
    }
    
    /// <summary>
    /// Used to change the mouse cursor to the bomb icon
    /// </summary>
    public void ChooseBombCursor()
    {
        Cursor.SetCursor(cursorChoice[1], Vector2.zero, CursorMode.Auto);
    }
    
    /// <summary>
    /// Used to change the mouse cursor to the book icon
    /// </summary>
    public void ChooseBookCursor()
    {
        Cursor.SetCursor(cursorChoice[2], Vector2.zero, CursorMode.Auto);
    }
}
