using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class House : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Transform[] _housesPosition;
    
    private List<List<Vector3Int>> _houses;
    private Vector3Int _lastPosition;
    
    private void Awake()
    {
        _houses = new List<List<Vector3Int>>();

        foreach (Transform house in _housesPosition)
        {
            Vector3Int collisionPointInt = new Vector3Int(
                Mathf.RoundToInt(house.position.x),
                Mathf.RoundToInt(house.position.y),
                0
            );
            
            HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
            List<Vector3Int> nonEmptyTiles = new List<Vector3Int>();

            SearchAdjacentNonEmptyTiles(collisionPointInt, _tilemap, visited, nonEmptyTiles);
            
            _houses.Add(nonEmptyTiles);
        }
    }

    void SearchAdjacentNonEmptyTiles(Vector3Int position, Tilemap tilemap, HashSet<Vector3Int> visited, List<Vector3Int> nonEmptyTiles)
    {
        if (visited.Contains(position) || !tilemap.cellBounds.Contains(position))
        {
            return;
        }

        visited.Add(position);

        if (tilemap.HasTile(position))
        {
            nonEmptyTiles.Add(position);

            SearchAdjacentNonEmptyTiles(position + Vector3Int.up, tilemap, visited, nonEmptyTiles);
            SearchAdjacentNonEmptyTiles(position + Vector3Int.down, tilemap, visited, nonEmptyTiles);
            SearchAdjacentNonEmptyTiles(position + Vector3Int.left, tilemap, visited, nonEmptyTiles);
            SearchAdjacentNonEmptyTiles(position + Vector3Int.right, tilemap, visited, nonEmptyTiles);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.GetContact(0);
        Vector2 collisionPoint = contactPoint.point;

        Vector3Int collisionPointInt = new Vector3Int(
            Mathf.RoundToInt(collisionPoint.x),
            Mathf.RoundToInt(collisionPoint.y),
            0
        );
        
        _lastPosition = collisionPointInt;
        
        foreach (List<Vector3Int> house in _houses)
        {
            if (house.Contains(collisionPointInt))
            {
                foreach (Vector3Int position in house)
                {
                    TileBase tile = _tilemap.GetTile(position);

                    if (tile != null)
                    {
                        _tilemap.SetTileFlags(position, TileFlags.None);
                        _tilemap.SetColor(position, new Color(1f, 1f, 1f, 0f));
                        _tilemap.SetTileFlags(position, TileFlags.LockAll);
                    }
                }
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        foreach (List<Vector3Int> house in _houses)
        {
            if (house.Contains(_lastPosition))
            {
                foreach (Vector3Int position in house)
                {
                    TileBase tile = _tilemap.GetTile(position);

                    if (tile != null)
                    {
                        _tilemap.SetTileFlags(position, TileFlags.None);
                        _tilemap.SetColor(position, new Color(1f, 1f, 1f, 1f));
                        _tilemap.SetTileFlags(position, TileFlags.LockAll);
                    }
                }
            }
        }
    }
}
