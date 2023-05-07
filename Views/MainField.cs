using System;
using Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Views
{
    [RequireComponent(typeof(Tilemap))]
    public class MainField : MonoBehaviour
    {
        [SerializeField] private Tilemap tileMap;

        [SerializeField] private TileBase tileBaseDesertSurface;
        [SerializeField] private TileBase tileBaseDesert;
        [SerializeField] private TileBase tileBaseIceSurface;
        [SerializeField] private TileBase tileBaseIce;
        [SerializeField] private TileBase tileBasePoisonSurface;
        [SerializeField] private TileBase tileBasePoison;

        [SerializeField] private int tilePositionYSurface;
        [SerializeField] private int minTilePositionY;
        [SerializeField] private int maxTilePositionY;
        private void Reset()
        {
            tileMap = GetComponent<Tilemap>();
        }

        public void DrawTiles(int tilePositionX, MainFieldType mainFieldType)
        {
            var tileBaseSurface = mainFieldType switch
            {
                MainFieldType.Desert => tileBaseDesertSurface,
                MainFieldType.Ice => tileBaseIceSurface,
                MainFieldType.Poison => tileBasePoisonSurface,
                _ => null
            };
            
            var tileBase = mainFieldType switch
            {
                MainFieldType.Desert => tileBaseDesert,
                MainFieldType.Ice => tileBaseIce,
                MainFieldType.Poison => tileBasePoison,
                _ => null
            };

            tileMap.SetTile(new Vector3Int(tilePositionX, tilePositionYSurface, 0), tileBaseSurface);
            for (var tilePositionY = minTilePositionY; tilePositionY <= maxTilePositionY; tilePositionY++)
            {
                tileMap.SetTile(new Vector3Int(tilePositionX, tilePositionY, 0), tileBase);
            }
        }
    }
}