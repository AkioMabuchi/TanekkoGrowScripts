// 適当にPureC#なclassやinterface、enumやstructを定義するファイル

using System.Collections.Generic;
using UnityEngine;

namespace Structs
{
    public struct TanekkoBodySpritesGroup
    {
        public Sprite spriteIdle;
        public IEnumerable<Sprite> spritesRunning;
        public IEnumerable<Sprite> spritesJumping;
        public Sprite spriteAttackingA;
        public Sprite spriteAttackingB;
        public Sprite spriteAttackingC;
        public Sprite spriteAttackingD;
        public Sprite spriteAbsorbingA;
        public Sprite spriteAbsorbingB;
        public Sprite spriteAbsorbingC;
        public Sprite spriteAbsorbingD;
        public Sprite spriteAbsorbingE;
        public Sprite spriteDamaged;
    }
}