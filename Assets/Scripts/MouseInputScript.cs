using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MouseInputScript : MonoBehaviour
{
    public GameObject MouseCursor;
    public GameObject BlockToPlace;
    public List<GameObject> BlockOptions;
    public GameObject SelectedBlock;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    int toolIndex = 0;

    GameObject MenuUI;

    private void Start() {
        BlockToPlace = BlockOptions[toolIndex];
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    int blockRotation = 0;

    void Update()
    {

        if(Input.mouseScrollDelta.y > 0)
        {
            blockRotation += 270;
            blockRotation %= 360;

            MouseCursor.transform.rotation = Quaternion.Euler(0f, 0f, blockRotation);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            blockRotation += 90;
            blockRotation %= 360;

            MouseCursor.transform.rotation = Quaternion.Euler(0f, 0f, blockRotation);
        }

        // Here's where we determine, using math, the location that the player generated
        // blocks should be at, snapped to the grid. If we ensured the player generated
        // blocks were the same size, this would be easier, unfortunately I am a silly
        // goose and the levelwidth variable decides how many player generated blocks
        // a level is wide.

        float gridSize = Screen.width / (float) LEVELDATA.instance.LevelWidth;
        // print(gridSize);
        int xGrid = Mathf.FloorToInt(Input.mousePosition.x / gridSize);
        int yGrid = Mathf.FloorToInt(Input.mousePosition.y / gridSize);
        // print(xGrid + " " + yGrid);
        float xPos = xGrid * (16f / LEVELDATA.instance.LevelWidth);
        float yPos = yGrid * (16f / LEVELDATA.instance.LevelWidth);
        // print(LEVELDATA.instance.LevelWidth / 16f);
        // print(xPos + " " + yPos);

        Vector3 pos = new Vector3(xPos + 0.5f, yPos + 0.5f, 0f);

        MouseCursor.transform.position = pos;

        if(Input.GetMouseButtonDown(0))
        {
            // Without this bit of code, the game would allow you to click a button and place a block behind it at the same time.

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            if(results.Count == 0)
            {
                GameObject BlockToSpawn = Instantiate(BlockToPlace, pos, Quaternion.Euler(0f, 0f, blockRotation + toolIndex == 2 ? 180 : 0));
                float BlockSize = 16f / LEVELDATA.instance.LevelWidth;
                BlockToSpawn.transform.localScale = new Vector3(BlockSize, BlockSize, BlockSize);
            }
        }

        else if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                // You can theoretically create blocks in levels that the player
                // cannot remove, simply by not naming them "pg_x"
                if(hit.transform.name.StartsWith("pg_"))
                {
                    GameObject.Destroy(hit.transform.gameObject);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTool(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTool(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetTool(2);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(MenuUI == null)
            {
                MenuUI = GameObject.Find("MenuUI");
            }
            MenuUI.SetActive(!MenuUI.activeSelf);
        }
    }

    public void SetTool(int index)
    {
        toolIndex = index;
        BlockToPlace = BlockOptions[toolIndex];
        SelectedBlock.transform.position = new Vector3(50f * toolIndex, 0f, -1f);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(LEVELDATA.instance.NextScene);
    }
}
