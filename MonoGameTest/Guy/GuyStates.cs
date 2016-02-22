using MonoGameTest.Guy.States;

namespace MonoGameTest.Guy
{
    /// <summary>
    /// A place to store a single instance of each state, for re-use at run-time.
    /// This class is meant to be used by IGuyState instances via Guy to transition to a new IGuyState.
    /// </summary>
    internal class GuyStates
    {
        public readonly GuyIdleState Idle;
        public readonly GuyRunningState Running;
        public readonly GuyJumpingState Jumping;

        internal GuyStates(GuyIdleState idle, GuyRunningState running, GuyJumpingState jumping)
        {
            Idle = idle;
            Running = running;
            Jumping = jumping;
        }
    }
}