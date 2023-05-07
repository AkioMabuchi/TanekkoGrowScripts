using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TanekkoBodySpritesByGrowth")]
    public class ScriptableObjectTanekkoBodySpritesByGrowth : ScriptableObject
    {
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByElement tanekkoBodySpritesNormal;
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByElement tanekkoBodySpritesFire;
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByElement tanekkoBodySpritesIce;
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByElement tanekkoBodySpritesPoison;

        public ScriptableObjectTanekkoBodySpritesByElement TanekkoBodySpritesNormal => tanekkoBodySpritesNormal;
        public ScriptableObjectTanekkoBodySpritesByElement TanekkoBodySpritesFire => tanekkoBodySpritesFire;
        public ScriptableObjectTanekkoBodySpritesByElement TanekkoBodySpritesIce => tanekkoBodySpritesIce;
        public ScriptableObjectTanekkoBodySpritesByElement TanekkoBodySpritesPoison => tanekkoBodySpritesPoison;
    }
}