using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TanekkoBodySprites")]
    public class ScriptableObjectTanekkoBodySprites : ScriptableObject
    {
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByGrowth tanekkoBodySpritesSeed;
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByGrowth tanekkoBodySpritesBud;
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByGrowth tanekkoBodySpritesFlowerA;
        [SerializeField] private ScriptableObjectTanekkoBodySpritesByGrowth tanekkoBodySpritesFlowerB;

        public ScriptableObjectTanekkoBodySpritesByGrowth TanekkoBodySpritesSeed => tanekkoBodySpritesSeed;
        public ScriptableObjectTanekkoBodySpritesByGrowth TanekkoBodySpritesBud => tanekkoBodySpritesBud;
        public ScriptableObjectTanekkoBodySpritesByGrowth TanekkoBodySpritesFlowerA => tanekkoBodySpritesFlowerA;
        public ScriptableObjectTanekkoBodySpritesByGrowth TanekkoBodySpritesFlowerB => tanekkoBodySpritesFlowerB;
    }
}