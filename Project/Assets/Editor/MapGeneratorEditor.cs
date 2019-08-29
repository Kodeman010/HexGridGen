﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexGen
{
    [CustomEditor(typeof(GridGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GridGenerator gridGen = (GridGenerator)target;

            if (GUILayout.Button("Generate"))
            {
                gridGen.StartGen();
            }
        }
    }
}