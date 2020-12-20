using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treePresents : MonoBehaviour
{
    public int desiredPresents = 3;
    public bool hasPassed = false;
    public TMPro.TMP_Text TreeText;

    private void Start()
    {
        TreeText.text = LEVELDATA.instance.CurrentPresents + " / " + desiredPresents;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "present")
        {
            GameObject.Destroy(collision.gameObject);
            LEVELDATA.instance.CurrentPresents += 1;

            transform.GetChild((int)Mathf.Floor(LEVELDATA.instance.CurrentPresents / (float)desiredPresents * transform.childCount)  % transform.childCount).gameObject.SetActive(true);

            if(LEVELDATA.instance.CurrentPresents >= LEVELDATA.instance.GoalPresents)
            {
                hasPassed = true;
            }
            TreeText.text = LEVELDATA.instance.CurrentPresents + " / " + LEVELDATA.instance.GoalPresents;
        }
    }

    public void clearPresents()
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
            TreeText.text = LEVELDATA.instance.CurrentPresents + " / " + LEVELDATA.instance.GoalPresents;
        }
    }

}
