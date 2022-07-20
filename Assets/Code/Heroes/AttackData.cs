using System;

namespace Code.Heroes
{
    [Serializable]
    public struct AttackData
    {
        public float Speed;
        public float BeforeDelay;
        public float Delay;
        public float Recharge;
    }
}