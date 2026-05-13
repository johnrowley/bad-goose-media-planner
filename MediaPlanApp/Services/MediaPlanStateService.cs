namespace MediaPlanApp.Services;

public record ChannelCardItem(string Name, string Category, decimal Budget, string Description = "");

public class MediaPlanStateService
{
    public string PlanName { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public List<ChannelCardItem> Channels { get; set; } = [];
    public bool HasPlan => Channels.Any();
}
