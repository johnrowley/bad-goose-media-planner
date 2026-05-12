namespace MediaPlanApp.Models;

public class ClientConfig
{
    public string Name { get; set; } = "";
    public string SheetUrl { get; set; } = "";

    public string Slug => Name.ToLowerInvariant().Replace(' ', '-');

    public string SpreadsheetId
    {
        get
        {
            // Extract from: https://docs.google.com/spreadsheets/d/{id}/edit...
            var segments = new Uri(SheetUrl).AbsolutePath.Split('/');
            var idx = Array.IndexOf(segments, "d");
            return idx >= 0 && idx + 1 < segments.Length ? segments[idx + 1] : "";
        }
    }

    public string Gid
    {
        get
        {
            var query = new Uri(SheetUrl).Query;
            var part = query.TrimStart('?').Split('&')
                .FirstOrDefault(p => p.StartsWith("gid="));
            return part?["gid=".Length..] ?? "0";
        }
    }
}
