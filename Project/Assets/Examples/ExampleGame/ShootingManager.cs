﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;
using System.Linq;

namespace HexGenExampleGame1
{
    [RequireComponent(typeof(LineRenderer))]
    public class ShootingManager : MonoBehaviour
    {
        private BoardManager boardManager;
        private LineRenderer lineRenderer;
        private List<Hex> hexLine = new List<Hex>();

        private bool shooting = false;
        private int interval = 3;

        private Unit shooter;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && boardManager.IsSelectionPlayerUnit() && boardManager.SelectedObject.GetComponent<Unit>().Shot == false)
            {
                shooting = !shooting;
                lineRenderer.positionCount = 0;

                shooter = boardManager.SelectedObject.GetComponent<Unit>();
            }

            if (shooting == true && shooter.Shot == false)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    shooting = false;
                    lineRenderer.positionCount = 0;

                    List<Unit> enemyUnits = boardManager.GetEnemyUnits();
                    for (int i = enemyUnits.Count - 1; i >= 0 ; --i)
                    {
                        if (hexLine.Contains(enemyUnits[i].OccupiedHex))
                        {
                            enemyUnits[i].gameObject.SetActive(false);
                            boardManager.ExistingUnits.Remove(enemyUnits[i]);
                            ScoreView.Score += 50;
                            shooter.Shot = true;
                        }
                    }
                }
            }

            if (shooting == true)
            {
                if (Time.frameCount % interval == 0)
                {
                    HandleInput();
                }
            }
        }

        void HandleInput()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Generator>())
                {
                    // Which hex was clicked
                    Hex hitHex = HexData.PixelToHex(hit.point, hit.transform.GetComponent<Generator>().MapData.Hexes);
                    
                    // Line of hexes from selected tank to hitHex
                    hexLine = GetComponent<HexLine>().GetHexLine(boardManager.SelectedObject.GetComponent<Unit>().OccupiedHex, hitHex);

                    // Restricting length of the line, so tanks have certain shooting range
                    List<Vector3> maxPositions = hexLine.Select(x => x.WorldPos).ToList();
                    Vector3[] positions = new Vector3[6];
                    positions = maxPositions.Take(6).ToArray();
                    hexLine = hexLine.Take(6).ToList();

                    lineRenderer.positionCount = positions.Length;

                    for (int i = 0; i < positions.Length; i++)
                    {
                        positions[i].y += 1f; // To avoid clipping into the ground
                    }

                    // Draws the line
                    lineRenderer.SetPositions(positions);
                }
            }
        }
    }

}