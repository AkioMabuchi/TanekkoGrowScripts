using System.Collections.Generic;
using Structs;
using UnityEngine;
using Views;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TanekkoBodySpritesByElement")]
    public class ScriptableObjectTanekkoBodySpritesByElement : ScriptableObject
    {
        [SerializeField] private Sprite spritesIdle;
        [SerializeField] private List<Sprite> spritesRunning;
        [SerializeField] private List<Sprite> spritesJumping;
        [SerializeField] private Sprite spriteAttackingA;
        [SerializeField] private Sprite spriteAttackingB;
        [SerializeField] private Sprite spriteAttackingC;
        [SerializeField] private Sprite spriteAttackingD;
        [SerializeField] private Sprite spriteAbsorbingA;
        [SerializeField] private Sprite spriteAbsorbingB;
        [SerializeField] private Sprite spriteAbsorbingC;
        [SerializeField] private Sprite spriteAbsorbingD;
        [SerializeField] private Sprite spriteAbsorbingE;
        [SerializeField] private Sprite spriteDamaged;

        public TanekkoBodySpritesGroup SpritesGroup => new TanekkoBodySpritesGroup
        {
            spriteIdle = spritesIdle,
            spritesRunning = spritesRunning,
            spritesJumping = spritesJumping,
            spriteAttackingA = spriteAttackingA,
            spriteAttackingB = spriteAttackingB,
            spriteAttackingC = spriteAttackingC,
            spriteAttackingD = spriteAttackingD,
            spriteAbsorbingA = spriteAbsorbingA,
            spriteAbsorbingB = spriteAbsorbingB,
            spriteAbsorbingC = spriteAbsorbingC,
            spriteAbsorbingD = spriteAbsorbingD,
            spriteAbsorbingE = spriteAbsorbingE,
            spriteDamaged = spriteDamaged
        };
    }
}
