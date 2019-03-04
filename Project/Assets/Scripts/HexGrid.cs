﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    public class HexGrid : MonoBehaviour
    {
        public GridSettings Grid;

        private MeshGen meshGen;

        private void Awake()
        {
            Grid.Hexes = new Hex[Grid.WorldWidth * Grid.WorldHeight];
            GenerateGrid();
        }

        private void Start()
        {
            meshGen = GetComponent<MeshGen>();
            meshGen.Triangulate(Grid.Hexes);
        }

        private void GenerateGrid()
        {
            int i = 0;
            for(int x = 0; x < Grid.WorldWidth; ++x)
            {
                for (int z = 0; z < Grid.WorldHeight; ++z)
                {
                    InstantiateHex(x, z, i++);
                }
            }
        }

        private void InstantiateHex(int x, int z, int i)
        {
            Grid.Hexes[i] = new Hex(new Vector2Int(x, z), CalcHexPos(x, z));
        }

        private Vector3 CalcHexPos(int x, int z)
        {
            Vector3 pos = Vector3.zero;
            pos.x = x * Hex.InnerRadius * 2;
            pos.z = z * Hex.InnerRadius * Mathf.Sqrt(3);

            if (z % 2 != 0)
                pos.x += Hex.InnerRadius;

            pos.x *= Grid.offsetMultiplier;
            pos.z *= Grid.offsetMultiplier;

            return pos;
        }
    }
}

