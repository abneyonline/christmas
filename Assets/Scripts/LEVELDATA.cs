using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Here's where I store data about the level that I feel should be widely accessible.

Obviously most any public data is accessible from any other script if you've got the gumption,
but by using a static object that points to this script, we can easily access data from this
script by accessing LEVLEDATA.instance.variablename as well as modify the variables in the
unity editor like any public variable. */

public class LEVELDATA: MonoBehaviour
{
    public int CurrentPresents = 0;
    public int GoalPresents = 10;
    public int PresentsToSpawn = 15;
    // How many player blocks wide the level is, uses a 16/9 aspect ratio to determine
    // how many blocks tall. Don't recommend taking this under 16.
    public int LevelWidth = 16;
    public Vector3 LevelGravity = Vector3.down;
    // Scene to be loaded when the next level button is pressed.
    public string NextScene = "template_GravityFields";

    public static LEVELDATA instance;

    private void Start() {
        instance = this;
    }
}
