using ExamenFinal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenFinal.ViewModels
{
    class DateDetailViewModel : BaseViewModel {

        public int _IdDate;
        public int IdDate

        {
            get => _IdDate;
            set => SetProperty(ref _IdDate, value);
        }

        public DateTime _DayDate;
        public DateTime DayDate

        {
            get => _DayDate;
            set => SetProperty(ref _DayDate, value);
        }

        public float _Cost;
        public float Cost

        {
            get => _Cost;
            set => SetProperty(ref _Cost, value);
        }

        public DateDetailViewModel() {}

        public DateDetailViewModel(DateConsult date)
        {
            if (date != null)
            {
                IdDate = date.IdDate;
                DayDate = date.DayDate;
                Cost = date.Cost;
            }
        }
    }  
}
