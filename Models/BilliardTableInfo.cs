namespace AlJamal.Models;

public sealed class BilliardTableInfo
{
    public int TableId { get; init; }
    public int TableTypeId { get; init; }
    public int TableNumber { get; init; }
    public string DisplayName { get; init; } = "";
    public string TypeName { get; init; } = "";
    public decimal HourlyRate { get; init; }
    public decimal FirstHourRate { get; init; }
    public decimal AdditionalHourRate { get; init; }
    public int ActivePlayers { get; init; }

    public bool IsBusy => ActivePlayers > 0;
}
