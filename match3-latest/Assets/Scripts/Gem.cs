
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemSo gemSo;
    public GemSo.GemType gemType;

    private void Awake()
    {
        gemType = gemSo.gemColor;
    }
}
