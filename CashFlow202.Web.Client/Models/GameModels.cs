namespace CashFlow202.Web.Client.Models;

// ─── Profession ────────────────────────────────────────────────────────────────

public class Profession
{
    public string Name { get; set; } = string.Empty;
    public int Salary { get; set; }
    public int Taxes { get; set; }
    public int MortgageOrRent { get; set; }
    public int SchoolLoanPayment { get; set; }
    public int CarPayment { get; set; }
    public int CreditCardPayment { get; set; }
    public int RetailDebtPayment { get; set; }
    public int OtherExpenses { get; set; }
    public int Savings { get; set; }
    public int SchoolLoanBalance { get; set; }
    public int CarLoanBalance { get; set; }
    public int CreditCardBalance { get; set; }
    public int RetailDebtBalance { get; set; }
    public int HomeMortgageBalance { get; set; }
    public int NumberOfChildren { get; set; }
    public int CostPerChild { get; set; }
}

// ─── Real-Estate / Small Deal ──────────────────────────────────────────────────

public class RealEstate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "Real Estate"; // "Real Estate", "Business"
    public int PurchasePrice { get; set; }
    public int DownPayment { get; set; }
    public int MortgageBalance { get; set; }
    public int MonthlyCashFlow { get; set; }
    public bool IsBigDeal { get; set; }

    public int Mortgage => MortgageBalance;
    public int Equity => PurchasePrice - MortgageBalance;
}

// ─── Stock / Fund / CD ────────────────────────────────────────────────────────

public class StockPosition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Shares { get; set; }
    public decimal PricePerShare { get; set; }
    public decimal DividendPerShare { get; set; }

    public decimal TotalValue => Shares * PricePerShare;
    public decimal MonthlyDividend => Shares * DividendPerShare;
}

// ─── Business ────────────────────────────────────────────────────────────────

public class BusinessIncome
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int MonthlyCashFlow { get; set; }
    public int DownPayment { get; set; }
    public int LiabilityBalance { get; set; }
}

// ─── Other passive income (interest, dividends from balance sheet) ─────────────

public class OtherIncome
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;
    public int MonthlyAmount { get; set; }
}

// ─── Options Worksheet ────────────────────────────────────────────────────────

public enum OptionType { Call, Put }
public enum OptionStatus { Open, Closed, Expired }

public class OptionPosition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; } = string.Empty;
    public OptionType Type { get; set; } = OptionType.Call;
    public int Contracts { get; set; }
    public decimal StrikePrice { get; set; }
    public decimal PremiumPaid { get; set; }
    public decimal? SellPrice { get; set; }
    public OptionStatus Status { get; set; } = OptionStatus.Open;
    public string Notes { get; set; } = string.Empty;

    public decimal TotalCost => Contracts * PremiumPaid * 100;
    public decimal TotalProceeds => SellPrice.HasValue ? Contracts * SellPrice.Value * 100 : 0;
    public decimal ProfitOrLoss => TotalProceeds - TotalCost;
}

// ─── Fast Track ───────────────────────────────────────────────────────────────

public class FastTrackState
{
    public string Dream { get; set; } = string.Empty;
    public int DreamCost { get; set; }
    public int DreamMonthlyExpense { get; set; }
    public List<BigDeal> BigDeals { get; set; } = new();
    public List<Charity> Charities { get; set; } = new();
}

public class BigDeal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int MonthlyCashFlow { get; set; }
    public int DownPayment { get; set; }
    public int TotalCost { get; set; }
}

public class Charity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int MonthlyDonation { get; set; }
}

// ─── Root Game State ──────────────────────────────────────────────────────────

public class GameState
{
    public string PlayerName { get; set; } = string.Empty;
    public Profession Profession { get; set; } = new();
    public int NumberOfChildren { get; set; }
    public int ExtraChildExpenses { get; set; }

    // Assets on balance sheet (not from profession card)
    public int BankSavings { get; set; }

    public List<RealEstate> RealEstateAssets { get; set; } = new();
    public List<StockPosition> Stocks { get; set; } = new();
    public List<BusinessIncome> Businesses { get; set; } = new();
    public List<OtherIncome> OtherIncomes { get; set; } = new();

    // Liabilities (profession card values, adjustable)
    public int SchoolLoanBalance { get; set; }
    public int CarLoanBalance { get; set; }
    public int CreditCardBalance { get; set; }
    public int RetailDebtBalance { get; set; }
    public int HomeMortgageBalance { get; set; }
    public int BankLoanBalance { get; set; }
    public int BankLoanPayment { get; set; }

    // Expenses overrides (for when you pay off debt)
    public int TaxesOverride { get; set; } = -1;   // -1 means use profession card
    public int MortgageOverride { get; set; } = -1;
    public int SchoolLoanPaymentOverride { get; set; } = -1;
    public int CarPaymentOverride { get; set; } = -1;
    public int CreditCardPaymentOverride { get; set; } = -1;
    public int RetailPaymentOverride { get; set; } = -1;

    // Options worksheet
    public List<OptionPosition> Options { get; set; } = new();

    // Fast Track
    public bool IsOnFastTrack { get; set; }
    public FastTrackState FastTrack { get; set; } = new();

    // ─── Computed income ────────────────────────────────────────────────────

    public int Salary => Profession.Salary;

    public int RealEstateCashFlow => RealEstateAssets.Sum(r => r.MonthlyCashFlow);
    public int StockDividends => (int)Stocks.Sum(s => s.MonthlyDividend);
    public int BusinessCashFlow => Businesses.Sum(b => b.MonthlyCashFlow);
    public int OtherPassiveIncome => OtherIncomes.Sum(o => o.MonthlyAmount);
    public int SavingsInterest => (int)(BankSavings * 0.001m); // ~1.2% APR

    public int TotalPassiveIncome =>
        RealEstateCashFlow + StockDividends + BusinessCashFlow + OtherPassiveIncome + SavingsInterest;

    public int TotalIncome => Salary + TotalPassiveIncome;

    // ─── Computed expenses ─────────────────────────────────────────────────

    public int Taxes => TaxesOverride >= 0 ? TaxesOverride : Profession.Taxes;
    public int MortgageOrRent => MortgageOverride >= 0 ? MortgageOverride : Profession.MortgageOrRent;
    public int SchoolLoanPayment => SchoolLoanPaymentOverride >= 0 ? SchoolLoanPaymentOverride : Profession.SchoolLoanPayment;
    public int CarPayment => CarPaymentOverride >= 0 ? CarPaymentOverride : Profession.CarPayment;
    public int CreditCardPayment => CreditCardPaymentOverride >= 0 ? CreditCardPaymentOverride : Profession.CreditCardPayment;
    public int RetailDebtPayment => RetailPaymentOverride >= 0 ? RetailPaymentOverride : Profession.RetailDebtPayment;
    public int ChildExpenses => NumberOfChildren * (Profession.CostPerChild > 0 ? Profession.CostPerChild : 0);
    public int OtherMonthlyExpenses => Profession.OtherExpenses + ExtraChildExpenses;

    public int TotalExpenses =>
        Taxes + MortgageOrRent + SchoolLoanPayment + CarPayment +
        CreditCardPayment + RetailDebtPayment + BankLoanPayment +
        ChildExpenses + OtherMonthlyExpenses;

    public int MonthlyCashFlow => TotalIncome - TotalExpenses;

    // ─── Fast Track gate ───────────────────────────────────────────────────

    public bool CanEnterFastTrack => TotalPassiveIncome >= TotalExpenses;

    // ─── Fast Track computed requirements ──────────────────────────────────
    // On fast track, you need to cover your existing expenses PLUS your dream monthly costs
    // The rule: passive income from big deals must cover total monthly needs
    public int FastTrackMonthlyNeeds =>
        TotalExpenses + (FastTrack?.DreamMonthlyExpense ?? 0);

    public int FastTrackTotalPassive =>
        TotalPassiveIncome + (FastTrack?.BigDeals.Sum(d => d.MonthlyCashFlow) ?? 0);

    public bool HasWon =>
        IsOnFastTrack && FastTrackTotalPassive >= FastTrackMonthlyNeeds;

    // ─── Balance Sheet Totals ──────────────────────────────────────────────

    public int TotalAssets =>
        BankSavings +
        (int)Stocks.Sum(s => s.TotalValue) +
        RealEstateAssets.Sum(r => r.PurchasePrice) +
        Businesses.Sum(b => b.DownPayment);

    public int TotalLiabilities =>
        HomeMortgageBalance + SchoolLoanBalance + CarLoanBalance +
        CreditCardBalance + RetailDebtBalance + BankLoanBalance +
        RealEstateAssets.Sum(r => r.MortgageBalance) +
        Businesses.Sum(b => b.LiabilityBalance);
}
