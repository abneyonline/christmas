using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeColor : MonoBehaviour
{
    Color[] colors = { Color.red, new Color(1f, 0.5f, 0f), Color.yellow, Color.green, new Color(0f, 1f, 1f), Color.blue };

    void Start()
    {
        List<int> selected = new List<int>();
        for (int a = 0; a < GetComponent<Renderer>().materials.Length; a++)
        {
            int generated = Random.Range(0, 6);
            while( selected.Contains(generated) )
            {
                generated = Random.Range(0, 6);
            }
            selected.Add(generated);
            GetComponent<Renderer>().materials[a].color = colors[generated];
        }
    }
}
