using System;
using System.Collections.Generic;
using System.Text;
using ActionFlow.API.Controllers;

namespace ActionFlow.API.UIModels
{
    public class AFAction : UIModelBase
    {
        private Guid _guid;
        private Guid? _customerGuid;
        private Guid? _jobGuid;
        private string _actionName;
        private DateTime? _dateCompleted;

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

        public Guid? CustomerGuid
        {
            get => _customerGuid;
            set
            {
                if (Nullable.Equals(value, _customerGuid)) return;
                _customerGuid = value;
                FirePropertyChanged();
            }
        }

        public Guid? JobGuid
        {
            get => _jobGuid;
            set
            {
                if (Nullable.Equals(value, _jobGuid)) return;
                _jobGuid = value;
                FirePropertyChanged();
            }
        }

        public string ActionName
        {
            get => _actionName;
            set
            {
                if (value == _actionName) return;
                _actionName = value;
                FirePropertyChanged();
            }
        }

        public DateTime? DateCompleted
        {
            get => _dateCompleted;
            set
            {
                if (value.Equals(_dateCompleted)) return;
                _dateCompleted = value;
                FirePropertyChanged();
            }
        }

        public override async void Save()
        {
            if (!IsModified) return;

            await Actions.UpdateActionAsync(Converter.ToApiAction(this));

            IsModified = false;
        }
    }
}
