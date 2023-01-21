using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class GemSo : ScriptableObject
    {
        public enum GemType { Red,Green,Blue,White}

        public GemType gemType;

    }
}