﻿using HexGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Change when input system updates (Unity 2019.2+)
public class PlayerInput : MonoBehaviour
{
    Hex hex;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<GridGenerator>())
            {
                hex = HexInteraction.SelectHexagon(hit.point, hit.transform.GetComponent<GridGenerator>().Grid);
                //Debug.Assert(hex != null);
                //Instantiate(tmp, hex.WorldPosition, Quaternion.identity);
            }
        }
    }
}
