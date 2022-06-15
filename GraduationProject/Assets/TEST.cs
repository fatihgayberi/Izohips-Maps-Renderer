using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public int length_1 = 2;
    public int length_2 = 5;

    // Start is called before the first frame update
    async void Start()
    {
        for (int i = 0; i < length_1; i++)
        {
            for (int j = 0; j < length_2; j++)
            {
                if (j == 3)
                {
                    break;
                }

                Debug.Log(i + "::::" + j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
