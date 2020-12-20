using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerPresents : MonoBehaviour
{
    public chimneyPresents ChimneyScript;
    public treePresents TreeScript;
    public GameObject NextLevelButton;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(ChimneyScript.StartSpawn())
            {
                TreeScript.clearPresents();
            }
        }

        if(TreeScript.hasPassed)
        {
            NextLevelButton.SetActive(true);
        }

    }
}
