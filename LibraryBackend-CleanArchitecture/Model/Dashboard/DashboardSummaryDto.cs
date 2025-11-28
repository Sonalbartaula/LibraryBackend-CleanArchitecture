using System.Collections.Generic;
using LibraryBackend_CleanArchitecture.Model;


namespace LibraryBackend_CleanArchitecture.Model.Dashboard
{
    public class DashboardSummaryDto
    {
        public int TotalBooks { get; set; }
        public int ActiveMembers { get; set; }
        public int BooksIssued { get; set; }
        public int OverdueBooks { get; set; }
        public int BooksAddedThisMonth { get; set; }
        public int MembersJoinedThisMonth { get; set; }
        public int DueSoonCount { get; set; }        // Books due in next 7 days
        public int RemindersSent { get; set; }

        public IEnumerable<Activity> RecentActivities { get; set; } = new List<Activity>();
        //public IEnumerable<Transaction> RecentTransactions { get; set; } = new List<Transaction>();

        public IEnumerable<Book> PopularBooks { get; set; } = new List<Book>();
    }

}

