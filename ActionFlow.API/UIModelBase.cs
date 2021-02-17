using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ActionFlow.API.Annotations;

namespace ActionFlow.API
{
    public class UIModelBase : INotifyPropertyChanged
    {
        private bool _isModified = false;

        public bool IsModified
        {
            get => _isModified;
            set
            {
                if (value.Equals(_isModified)) return;
                _isModified = value;
                FirePropertyChanged();
            }
        }

        /// <summary>
        /// Override this method to save this model to the database
        /// </summary>
        public virtual void Save()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            IsModified = true;
        }
    }
}
