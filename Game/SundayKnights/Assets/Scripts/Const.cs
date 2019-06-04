/*
    Constant parameters used throughout the game.
*/

public class Const
{
    // Game grid
    public static readonly int Rows = 8;
    public static readonly int Columns = 6;

    // Animations
    public static readonly float AnimationDuration = 0.2f;
    public static readonly float MoveAnimationMinDuration = 0.05f;
    public static readonly float ExplosionDuration = 0.3f;
    public static readonly float WaitBeforePotentialMatchesCheck = 2f;
    public static readonly float OpacityAnimationFrameDelay = 0.05f;

    // Logic
    public static readonly int MinimumMatches = 3;
    public static readonly int MinimumMatchesForBonus = 4;

    // Scoring
    public static readonly int Match3Score = 60;
    public static readonly int SubsequentMatchScore = 1000;
}
