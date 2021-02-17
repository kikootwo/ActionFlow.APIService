using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ActionFlow.API.Controllers;

namespace ActionFlow.API.UIModels
{
    public class AFCustomer : UIModelBase
    {
        private Guid _guid;
        private string _name;
        private eCustomerStatus _status;

        public Guid Guid
        {
            get => _guid;
            set
            {
                if (value.Equals(_guid)) return;
                _guid = value;
                FirePropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                FirePropertyChanged();
            }
        }

        public eCustomerStatus Status
        {
            get => _status;
            set
            {
                if (value == _status) return;
                _status = value;
                FirePropertyChanged();
            }
        }

        public override async void Save()
        {
            if(!IsModified) return;

            await Customers.UpdateCustomerAsync(Converter.ToApiCustomer(this));

            IsModified = false;
        }

        public ObservableCollection<AFAction> Actions { get; set; } = new ObservableCollection<AFAction>();
        public ObservableCollection<AFJob> Jobs { get; set; } = new ObservableCollection<AFJob>();
    }
}
