 using System;
using System.Collections.Generic;

namespace WebApplication10.Models
{
    public partial class LoginUsers
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Accountnumber { get; set; }
        public string IFSCCODE { get; set; }
        public string MobileNumber { get; set; }
        public string AccountHoldersName { get; set; }
        public string BettingValue { get; set; }
        public string BetValue2 { get; set; }
        public string BetValue3 { get; set; }
        public string BettingAmount { get; set; }
        public string WalletBalance { get; set; }
        public int? DepositedBalance { get; set; }
        public DateTime? CreatedDate { get; set; }
         public int? RemainingBalance { get; set; }
        public int? BettingPercentage { get; set; }
         public string papel { get; set; }
    }
}
