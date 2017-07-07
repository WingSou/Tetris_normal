using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_v2 : MonoBehaviour {

    public bool c_swich;
    public int cube_type;
    public int pos_type;
    public bool control_cube;
    public bool can_make;
    public int color_number;

    void Awake()
    {
        c_swich = false;
        cube_type = 0;
        pos_type = 0;
        control_cube = false;
        can_make = true;
        color_number = 0;
    }

    void Update()
    {
        switch(c_swich)
        {
            //ブロックがある
            case true:

                switch(color_number)
                {
                    case 0:

                        
                        GetComponent<Renderer>().material.color = Color.gray;

                        break;

                    case 1:

                        GetComponent<Renderer>().material.color = Color.blue;

                        break;

                    case 2:

                        GetComponent<Renderer>().material.color = Color.green;

                        break;

                    case 3:

                        GetComponent<Renderer>().material.color = Color.yellow;

                        break;

                    case 4:

                        GetComponent<Renderer>().material.color = Color.red;

                        break;

                    case 5:

                        GetComponent<Renderer>().material.color = Color.cyan;

                        break;

                    case 6:

                        GetComponent<Renderer>().material.color = Color.magenta;

                        break;

                    case 7:

                        GetComponent<Renderer>().material.color = Color.black;

                        break;
                }

                break;

            //ブロック無し
            case false:

                GetComponent<Renderer>().material.color = Color.white;
                color_number = 0;

                break;
        }
    }
}
