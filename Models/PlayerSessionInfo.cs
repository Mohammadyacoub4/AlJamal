namespace AlJamal.Models;

public sealed class PlayerSessionInfo
{
    public int PlayerSessionId { get; init; }
    public int TableId { get; init; }
    public string? PlayerName { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public decimal HourlyRate { get; init; }
    public decimal FirstHourRate { get; init; }
    public decimal AdditionalHourRate { get; init; }
    public bool IsActive { get; init; }
    public bool IsInvoiced { get; init; }
    public string TableDisplayName { get; init; } = "";

    public string DisplayLabel =>
        string.IsNullOrWhiteSpace(PlayerName)
            ? $"زبون #{PlayerSessionId}"
            : PlayerName;

    public TimeSpan Elapsed =>
        (IsActive ? DateTime.Now : (EndTime ?? DateTime.Now)) - StartTime;

    public string ElapsedText =>
        $"{(int)Elapsed.TotalHours:D2}:{Elapsed.Minutes:D2}:{Elapsed.Seconds:D2}";
}
