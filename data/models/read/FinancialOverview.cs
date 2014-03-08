using System;

namespace data.models.read
{
    public class FinancialOverview : NotifyPropertyChangedBase
    {
        int _id;
        public int Id
        {
            get { return _id; }
            protected set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            protected set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        decimal? _income;
        public decimal? Income
        {
            get { return _income; }
            protected set
            {
                _income = value;
                RaisePropertyChanged(() => Income);
            }
        }

        decimal? _spending;
        public decimal? Spending
        {
            get { return _spending; }
            protected set
            {
                _spending = value;
                RaisePropertyChanged(() => Spending);
            }
        }

        decimal? _savings;
        public decimal? Savings
        {
            get { return _savings; }
            protected set
            {
                _savings = value;
                RaisePropertyChanged(() => Savings);
            }
        }

        decimal _dividends;
        public decimal Dividends
        {
            get { return _dividends; }
            protected set
            {
                _dividends = value;
                RaisePropertyChanged(() => Dividends);
            }
        }

        decimal _appreciation;
        public decimal Appreciation
        {
            get { return _appreciation; }
            protected set
            {
                _appreciation = value;
                RaisePropertyChanged(() => Appreciation);
            }
        }

        decimal _changeInNetWorth;
        public decimal ChangeInNetWorth
        {
            get { return _changeInNetWorth; }
            protected set
            {
                _changeInNetWorth = value;
                RaisePropertyChanged(() => ChangeInNetWorth);
            }
        }

        decimal _netWorth;
        public decimal NetWorth
        {
            get { return _netWorth; }
            protected set
            {
                _netWorth = value;
                RaisePropertyChanged(() => NetWorth);
            }
        }
    }
}