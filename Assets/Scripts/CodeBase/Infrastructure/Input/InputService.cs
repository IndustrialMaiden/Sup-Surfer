
using Input = UnityEngine.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
    public class InputService : IInputService
    {
        public virtual Vector2 Control()
        {
            return Vector2.zero;
        }
    }
}