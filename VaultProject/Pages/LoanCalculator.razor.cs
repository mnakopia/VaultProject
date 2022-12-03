using Microsoft.AspNetCore.Components;
using MudBlazor;
using VaultProject.Data;
using VaultProject.Forms;

namespace VaultProject.Pages
{
    public partial class LoanCalculator
    {
        private bool _isSelfEmployed;

        private double _loanAmount { get; set; }
        private int _loanPeriod { get; set; }
        private int _selfEmployedIncome { get; set; }
        private Professions _profession { get; set; }
        private double? _loanTax { get; set; }
        private double _rate { get; set; }
        private double _efectiveRate { get; set; }
        private double _index { get; set; }

        private int _loanMaxAmount;



        protected override void OnInitialized()
        {

            _loanAmount = 30000;
            _loanPeriod = 36;
            _rate = 11.54;
            GetLoanTax();
            GetMaxLoanAmount();
        }



        public void GetLoanAmount(int loanAmount)
        {
            _loanAmount = loanAmount;
            GetRate();
            GetLoanTax();
        }

        public void GetSelfEmployedIncome(int selfEmployedIncome)
        {
            _selfEmployedIncome = selfEmployedIncome;
            GetMaxLoanAmount();
        }

        public void GetLoanTerm(int loanPeriod)
        {
            _loanPeriod = loanPeriod;
            GetRate();
            GetLoanTax();
        }
        public void GetProfessions(Professions profession)
        {
            _profession = profession;
            GetIndex();
            GetMaxLoanAmount();
        }

        public static double PMT(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
        {
            var rate = yearlyInterestRate / 12;
            var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
            return (rate + (rate / denominator)) * loanAmount;
        }
        public void GetLoanTax()
        {
            if (_loanAmount > 0 && _loanPeriod > 0 && !_isSelfEmployed)
            {
                _efectiveRate = 18.12;
                _loanTax = Math.Round(PMT(_rate / 100, _loanPeriod, _loanAmount), 2);
                StateHasChanged();

            }



        }
        public void GetMaxLoanAmount()
        {


            if (_selfEmployedIncome > 0 && _isSelfEmployed)
            {
                _rate = 13.45;
                _efectiveRate = 15.85;
                _loanMaxAmount = (int)(_selfEmployedIncome * _index);
                StateHasChanged();

            }


        }
        private void OpenLoanForm()
        {
            DialogOptions closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };

            DialogService.Show<MakeLoanForm>("", closeOnEscapeKey);
        }


        public void GetIndex()
        {

            _index = _profession switch
            {
                Professions.Driver => 5.2,
                Professions.Nanny => 2.36,
                _ => 3.25
            };


        }

        public void GetRate()
        {
            if (_loanAmount < 2000)
            {
                _rate = 35.2;

            }
            else if (_loanAmount < 4000)
            {
                _rate = 25.6;
            }
            else if (_loanAmount < 10000)
            {
                _rate = 15.67;

            }
            else
            {
                _rate = 11.54;
            }


            _efectiveRate = _rate + 3.27;

        }
    }
}