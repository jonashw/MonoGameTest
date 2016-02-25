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
        public readonly GuyDuckingState Ducking;
        public readonly GuyCannonballState Cannonball;
        public readonly GuyCannonballCrashState CannonballCrash;
        public readonly GuyCannonballCrashRecoveryState CannonballCrashRecovery;

        internal GuyStates(
            GuyIdleState idle,
            GuyRunningState running,
            GuyJumpingState jumping,
            GuyDuckingState ducking,
            GuyCannonballState cannonball,
            GuyCannonballCrashState cannonballCrash,
            GuyCannonballCrashRecoveryState cannonballCrashRecovery)
        {
            Idle = idle;
            Running = running;
            Jumping = jumping;
            Ducking = ducking;
            Cannonball = cannonball;
            CannonballCrash = cannonballCrash;
            CannonballCrashRecovery = cannonballCrashRecovery;
        }
    }
}