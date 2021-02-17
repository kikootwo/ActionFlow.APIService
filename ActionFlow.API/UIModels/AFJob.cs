using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ActionFlow.API.Controllers;

namespace ActionFlow.API.UIModels
{
    public class AFJob : UIModelBase
    {
        private Guid _guid;
        private string _description;
        private Guid _customerGuid;

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

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                FirePropertyChanged();
            }
        }

        public Guid CustomerGuid
        {
            get => _customerGuid;
            set
            {
                if (value.Equals(_customerGuid)) return;
                _customerGuid = value;
                FirePropertyChanged();
            }
        }

        public override async void Save()
        {
            if (!IsModified) return;

            await Jobs.UpdateJobAsync(Converter.ToApiJob(this));

            IsModified = false;
        }

        public ObservableCollection<AFAction> Actions { get; set; } = new ObservableCollection<AFAction>();
    }
}
