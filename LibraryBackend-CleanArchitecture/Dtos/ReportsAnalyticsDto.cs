namespace LibraryBackend_CleanArchitecture.Dtos
{
    public class ReportsAnalyticsDto
    {
        public int TotalTransactions { get; set; }
        public int ActiveMembers { get; set; }
        public int BooksInCollection { get; set; }
        public double AverageDailyCheckouts { get; set; }

        public List<KeyValueDto> MonthlyTrends { get; set; } = new(); 
        public List<KeyValueDto> CategoryDistribution { get; set; } = new();
        public List<KeyValueDto> MemberGrowth { get; set; } = new();

        public int OverdueBooksCount { get; set; }
        public List<KeyValueDto> PopularBooks { get; set; } = new();
        public List<KeyValueDto> TopMembers { get; set; } = new();

        public class KeyValueDto
        {
            public string Key { get; set; } = string.Empty;
            public int Value { get; set; }

            
        }
    }
}
