using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TopEdgeCollider : MonoBehaviour
{
    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        AddTopEdgeColliders();
    }

    private void AddTopEdgeColliders()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPosition))
                {
                    
                    Vector3Int aboveCellPosition = new Vector3Int(x, y + 1, 0);
                    if (!tilemap.HasTile(aboveCellPosition))
                    {
                        GameObject colliderObject = new GameObject("TopEdgeCollider");
                        colliderObject.layer =7;
                        colliderObject.transform.parent = this.transform;
                        colliderObject.transform.position = tilemap.CellToWorld(cellPosition) + new Vector3(0.5f, 0.5f, 0);
                        EdgeCollider2D edgeCollider = colliderObject.AddComponent<EdgeCollider2D>();
                        edgeCollider.points = new Vector2[] { new Vector2(-0.5f, 0.5f), new Vector2(0.5f, 0.5f) };
                    }
                }
            }
        }
    }
}

