namespace MediaPlanApp.Services;

public class GoogleSheetsService(HttpClient httpClient)
{
    private const string SpreadsheetId = "1ejHCx4UsT6ZodKtH-ruNI1jEUxbR6uwoQIt8jP-LDig";

    public async Task<List<List<string>>> GetSheetDataAsync(int rowCount = 3, int columnCount = 3)
    {
        var url = $"https://docs.google.com/spreadsheets/d/{SpreadsheetId}/export?format=csv&gid=0";
        var csv = await httpClient.GetStringAsync(url);

        var result = new List<List<string>>();
        var lines = csv.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < Math.Min(rowCount, lines.Length); i++)
        {
            var cells = ParseCsvLine(lines[i]);
            var row = cells.Take(columnCount).ToList();

            while (row.Count < columnCount)
                row.Add(string.Empty);

            result.Add(row);
        }

        return result;
    }

    public async Task<string> GetNamedRangeValueAsync(string rangeName)
    {
        var url = $"https://docs.google.com/spreadsheets/d/{SpreadsheetId}/export?format=csv&range={rangeName}";
        var csv = await httpClient.GetStringAsync(url);
        var cells = ParseCsvLine(csv.Trim());
        return cells.Count > 0 ? cells[0] : string.Empty;
    }

    private static List<string> ParseCsvLine(string line)
    {
        var cells = new List<string>();
        var current = new System.Text.StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                cells.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        cells.Add(current.ToString().Trim());
        return cells;
    }
}
