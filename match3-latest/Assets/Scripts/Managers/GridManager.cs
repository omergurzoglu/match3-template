
using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]private int width,height;
        public List<GameObject> gemPrefabs;

        public GameObject[,] _gridElements;
        public  Dictionary<Vector2, GameObject> _gemPositionDictionary;
        

        private void Awake()
        {
            _gridElements = new GameObject[width, height];
            _gemPositionDictionary=new Dictionary<Vector2, GameObject>();
            InitializeGrid();
        }

        private void OnEnable()
        {
            SwapManager.SwapCompleteUpdateGrid += UpdateGemInArray;
        }

        private void OnDisable()
        {
            SwapManager.SwapCompleteUpdateGrid -= UpdateGemInArray;
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var randomGem = gemPrefabs[Random.Range(0, gemPrefabs.Count)];
                    _gemPositionDictionary.Add(new Vector2Int(x, y), _gridElements[x, y]);
                    _gridElements[x, y] = Instantiate(randomGem,new Vector3(x,y,0),Quaternion.identity);

                }
            }
        }

        private GameObject GetElementAtXY(int x,int y)
        {
            return _gridElements[x, y];
        }

        private Vector2Int ToVector2Int(int x, int y)
        {
            return new Vector2Int(x, y);
        }
        private void UpdateGemInArray(GameObject gem1,GameObject gem2)
        {
            var gem1Pos = gem1.transform.position;
            var gem2Pos = gem2.transform.position;
            (_gridElements[(int)gem1Pos.x, (int)gem1Pos.y], _gridElements[(int)gem2Pos.x, (int)gem2Pos.y]) = (_gridElements[(int)gem2Pos.x, (int)gem2Pos.y], _gridElements[(int)gem1Pos.x, (int)gem1Pos.y]);
            
        }
        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Debug.Log(_gridElements[x, y].name +"is at position"+_gridElements[x,y].transform.position);
                    }
                }
            }
            
        }
        
    }
}