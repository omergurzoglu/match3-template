
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width,height;
    [SerializeField]private List<Gem> gemPrefabs;
    
    private Gem[,] _gridArray;
    private Dictionary<Vector2, Gem> _gemPositionDictionary;

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
                _gridArray[x, y] = gemPrefabs[Random.Range(0, gemPrefabs.Count)];
                _gemPositionDictionary.Add(new Vector2(x,y),_gridArray[x,y]);
                Instantiate( _gridArray[x, y], new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKey(KeyCode.Space))
    //     {
    //         foreach ( KeyValuePair<Vector2,Gem> keyValuePair in _dictionary)
    //         {
    //            Debug.Log("key"+keyValuePair.Key+"value"+keyValuePair.Value);
    //         }
    //     }
    // }
}