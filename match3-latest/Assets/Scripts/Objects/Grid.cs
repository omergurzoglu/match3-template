using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    public class Grid :Singleton<Grid>
    {
        
        [SerializeField] private int width,height;
        [SerializeField] private List<Gem> gemPrefabs;
    
        public Gem[,] _gridArray;
        public  Dictionary<Vector2, Gem> _gemPositionDictionary;

        private void OnEnable()
        {
            GameManager.Instance.UpdateGem += UpdateGemInArray;
        }

        private void Awake()
        {
            
            _gridArray = new Gem[width, height];
            _gemPositionDictionary=new Dictionary<Vector2, Gem>();
        }
        private void Start()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var randomGem = gemPrefabs[Random.Range(0, gemPrefabs.Count)];
                    _gemPositionDictionary.Add(new Vector2Int(x, y), _gridArray[x, y]);
                    _gridArray[x, y] = Instantiate(randomGem, new Vector2(x, y), Quaternion.identity);
                }
            }
        }
    
        

        private void UpdateGemInArray(Gem gem1,Gem gem2)
        {
            
            var gem1Pos = gem1.transform.position;
            var gem2Pos = gem2.transform.position;
            (_gridArray[(int)gem1Pos.x, (int)gem1Pos.y], _gridArray[(int)gem2Pos.x, (int)gem2Pos.y]) = (_gridArray[(int)gem2Pos.x, (int)gem2Pos.y], _gridArray[(int)gem1Pos.x, (int)gem1Pos.y]);
            
        }

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Debug.Log(_gridArray[x, y].gemType +"is at position"+_gridArray[x,y].thisGemsTransform.position);
                    }
                }
            }
            
        }

        private void OnDisable()
        {
            GameManager.Instance.UpdateGem-= UpdateGemInArray;
        }
    }
}