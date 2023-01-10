using UnityEngine;

[CreateAssetMenu]
public class GemSo : ScriptableObject
{
    public enum GemType { Red,Green,Blue,White}

    public GemType gemColor;

}