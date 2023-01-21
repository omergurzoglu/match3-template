
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Objects;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int width, height;
        public List<GameObject> gemPrefabs;
        private Camera _camera;
        public GameObject[,] _gridElements;
        public static bool GridIsUpdating=false;

        private void Awake()
        {
            _camera = Camera.main;
            _gridElements = new GameObject[width, height];
            if (_camera != null) _camera.transform.position = new Vector3((width - 1) / 2f, (height - 1) / 2f, -1);
            InitializeGrid();
        }
        private void OnEnable() => SwapManager.SwapCompleteUpdateGrid += UpdateGemInArray;
        private void OnDisable() => SwapManager.SwapCompleteUpdateGrid -= UpdateGemInArray;

        private void InitializeGrid() {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    GemSo.GemType type;
                    do {
                        type = (GemSo.GemType)Random.Range(0, 4);
                    } while ((x > 0 && type == GetGemColor(_gridElements[x - 1, y])) ||
                             (y > 0 && type == GetGemColor(_gridElements[x, y - 1])));
                    _gridElements[x, y] = Instantiate(GetGemPrefab(type), new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
        public GameObject FindIn2DArray(GameObject[,] array, GameObject gem)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    if (array[x, y] == gem)
                    {
                        return array[x, y];
                    }
                }
            }
            return null;
        }
        
        private GameObject GetElementAtXY(int x, int y) => _gridElements[x, y];

        public static Vector2Int GetVector2OfElement(GameObject gem)
        {
            var position = gem.transform.position;
            return new Vector2Int((int)position.x, (int)position.y);
        }

        private Vector2Int Vector3Converter(Vector3 vector3)
        {
            var x = Mathf.FloorToInt(vector3.x);
            var y = Mathf.FloorToInt(vector3.y);
            return new Vector2Int(x, y);
        }

        private GemSo.GemType GetGemColor(GameObject gem)
        {
            gem.TryGetComponent<Gem>(out var thisGem);
            return thisGem.gemType;
        }
        private Vector2Int ToVector2Int(int x, int y) => new Vector2Int(x, y);

        private void UpdateGemInArray(GameObject gem1, GameObject gem2)
        {
            var gem1Pos = gem1.transform.position;
            var gem2Pos = gem2.transform.position;
            (_gridElements[(int)gem1Pos.x, (int)gem1Pos.y], _gridElements[(int)gem2Pos.x, (int)gem2Pos.y]) =
                (_gridElements[(int)gem2Pos.x, (int)gem2Pos.y], _gridElements[(int)gem1Pos.x, (int)gem1Pos.y]);
            StartCoroutine(UpdateGrid());
            
        }
        IEnumerator UpdateGrid()
        {
            GridIsUpdating = true;
            yield return new WaitForSeconds(0.4f);
            DetectMatchesAndDestroy();
            yield return new WaitForSeconds(0.4f);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_gridElements[x, y] == null)
                    {
                        FillEmptySlots(x,y);
                        
                    }
                }
            }
            yield return new WaitForSeconds(0.4f);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_gridElements[x, y] == null)
                    {
                        FillNewEmptySlots(x,y);
                        
                    }
                }
            }
            
            
            GridIsUpdating = false;
        }
        private void FillEmptySlots(int x , int y)
        {
            for (int i = y; i < height; i++)
            {
                if (_gridElements[x, i + 1] != null)
                {
                    _gridElements[x, i] = _gridElements[x, i + 1];
                    _gridElements[x, i].transform.DOMoveY(i, 0.2f).SetEase(Ease.OutBounce);
                }
            }
        }

        private void FillNewEmptySlots(int x , int y )
        {
            for (int i = y; i < height; i++)
            {
                if (i == height - 1)
                {
                    var newGem = Instantiate(GetGemPrefab((GemSo.GemType)Random.Range(0, 4)), new Vector3(x, i +5, 0), Quaternion.identity);
                    _gridElements[x, i] = newGem;
                    newGem.transform.DOMoveY(i, 0.2f).SetEase(Ease.OutBounce);
                }
            }
        }

       
        
        
        private GameObject GetGemPrefab(GemSo.GemType type) {
            foreach (var gem in gemPrefabs) {
                if (gem.GetComponent<Gem>().gemType == type) {
                    return gem;
                }
            }
            return null;
        }
        
        
        // private void ReplaceGem(int x, int y)  
        // {
        //     for (int i = y; i < height; i++)
        //     {
        //         if (i == height - 1)
        //         {
        //             var newGem = Instantiate(GetGemPrefab((GemSo.GemType)Random.Range(0, 4)), new Vector3(x, i +5, 0), Quaternion.identity);
        //             _gridElements[x, i] = newGem;
        //             newGem.transform.DOMoveY(i, 0.2f).SetEase(Ease.OutBounce);
        //         }
        //         else if (_gridElements[x, i + 1] != null)
        //         {
        //             _gridElements[x, i] = _gridElements[x, i + 1];
        //             _gridElements[x, i].transform.DOMoveY(i, 0.2f).SetEase(Ease.OutBounce);
        //         }
        //         
        //     }
        // }
        
        
        private void DetectMatchesAndDestroy()
        {
            GridIsUpdating = true;
            List<List<GameObject>> matches = new List<List<GameObject>>(); 
            
            for (int x = 0; x < _gridElements.GetLength(0); x++)
            {
                for (int y = 0; y < _gridElements.GetLength(1); y++)
                {
                    GameObject currentGem = _gridElements[x, y];
                    if (currentGem == null) continue;
                    
                    List<GameObject> horizontalMatch = CheckHorizontalMatches(x, y);
                    if (horizontalMatch.Count >= 3)
                    {
                        matches.Add(horizontalMatch);
                    }
                    
                    List<GameObject> verticalMatch = CheckVerticalMatches(x, y);
                    if (verticalMatch.Count >= 3)
                    {
                        matches.Add(verticalMatch);
                    }
                }
            }
            if (matches.Count > 0)
            {
                DestroyMatches(matches);
                DetectMatchesAndDestroy();
            }
            else
            {
                GridIsUpdating = false;
            }
        }
        

        private List<GameObject> CheckHorizontalMatches(int x, int y)
        {
            List<GameObject> horizontalMatch = new List<GameObject>();
            GameObject currentGem = _gridElements[x, y];
            GemSo.GemType currentGemColor = GetGemColor(currentGem);
            
            for (int i = x - 1; i >= 0; i--)
            {
                GameObject leftGem = _gridElements[i, y];
                if (leftGem != null && GetGemColor(leftGem) == currentGemColor)
                {
                    horizontalMatch.Add(leftGem);
                }
                else
                {
                    break;
                }
            }
            for (int i = x + 1; i < _gridElements.GetLength(0); i++)
            {
                GameObject rightGem = _gridElements[i, y];
                if (rightGem != null && GetGemColor(rightGem) == currentGemColor)
                {
                    horizontalMatch.Add(rightGem);
                }
                else
                {
                    break;
                }
            }
            horizontalMatch.Add(currentGem);
            return horizontalMatch;
        }

        private List<GameObject> CheckVerticalMatches(int x, int y)
        {
            List<GameObject> verticalMatch = new List<GameObject>();
            GameObject currentGem = _gridElements[x, y];
            GemSo.GemType currentGemColor = GetGemColor(currentGem);
            
            for (int i = y + 1; i < _gridElements.GetLength(1); i++)
            {
                GameObject upGem = _gridElements[x, i];
                if (upGem != null && GetGemColor(upGem) == currentGemColor)
                {
                    verticalMatch.Add(upGem);
                }
                else
                {
                    break;
                }
            }
            for (int i = y - 1; i >= 0; i--)
            {
                GameObject downGem = _gridElements[x, i];
                if (downGem != null && GetGemColor(downGem) == currentGemColor)
                {
                    verticalMatch.Add(downGem);
                }
                else
                {
                    break;
                }
            }

            verticalMatch.Add(currentGem);
            return verticalMatch;
        }
        
        
        private void DestroyMatches(List<List<GameObject>> matches)
        {
            foreach (var match in matches)
            {
                if (match.Count >= 3)
                {
                    foreach (var gem in match)
                    {
                        if (gem != null)
                        {
                            var position = gem.transform.position;
                            _gridElements[(int)position.x, (int)position.y] = null;
                            Destroy(gem);
                        }
                    }
                }
            }
            matches.Clear();
        }
        
    }
}