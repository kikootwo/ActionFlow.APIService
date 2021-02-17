using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using ActionFlow.API;
using ActionFlow.API.Controllers;
using ActionFlow.API.Models;
using ActionFlow.API.UIModels;
using ActionFlow.HelloWorld.Annotations;
using Action = ActionFlow.API.Models.Action;

namespace ActionFlow.HelloWorld
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Dictionary<Guid, AFCustomer> _customers= new Dictionary<Guid, AFCustomer>();
        private Dictionary<Guid, AFJob> _jobs = new Dictionary<Guid, AFJob>();
        private Dictionary<Guid, AFAction> _actions = new Dictionary<Guid, AFAction>();
        private bool _dataLoading = false;
        private AFCustomer _selectedCustomer;
        private AFJob _selectedJob;
        private AFAction _selectedCustomerAction;
        private AFAction _selectedJobAction;


        public AFAction SelectedCustomerAction
        {
            get => _selectedCustomerAction;
            set
            {
                if (Equals(value, _selectedCustomerAction)) return;
                _selectedCustomerAction = value;
                FirePropertyChanged();
            }
        }

        public AFAction SelectedJobAction
        {
            get => _selectedJobAction;
            set
            {
                if (Equals(value, _selectedJobAction)) return;
                _selectedJobAction = value;
                FirePropertyChanged();
            }
        }

        public AFJob SelectedJob
        {
            get => _selectedJob;
            set
            {
                if (Equals(value, _selectedJob)) return;
                _selectedJob = value;
                FirePropertyChanged();
            }
        }

        public bool DataLoading
        {
            get => _dataLoading;
            set
            {
                if (value == _dataLoading) return;
                _dataLoading = value;
                FirePropertyChanged();
            }
        }

        public AFCustomer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (Equals(value, _selectedCustomer)) return;
                _selectedCustomer = value;
                FirePropertyChanged();
            }
        }

        public ObservableCollection<AFCustomer> Customers { get; set; } = new ObservableCollection<AFCustomer>();
        public string[] CustomerStatuses => Enum.GetNames(typeof(eCustomerStatus));
        

        public Command AddCustomerCommand { get; set; }
        public Command DeleteCustomerCommand { get; set; }
        public Command AddJobCommand { get; set; }
        public Command DeleteJobCommand { get; set; }
        public Command AddCustomerActionCommand { get; set; }
        public Command DeleteCustomerActionCommand { get; set; }
        public Command AddJobActionCommand { get; set; }
        public Command DeleteJobActionCommand { get; set; }
        public MainWindowViewModel()
        {
            AddCustomerCommand = new Command(AddCustomer);
            DeleteCustomerCommand = new Command(DeleteCustomer);
            AddJobCommand = new Command(AddJob);
            DeleteJobCommand = new Command(DeleteJob);
            AddCustomerActionCommand = new Command(AddCustomerAction);
            DeleteCustomerActionCommand = new Command(DeleteCustomerAction);
            AddJobActionCommand = new Command(AddJobAction);
            DeleteJobActionCommand = new Command(DeleteJobAction);

            Stopwatch sw = Stopwatch.StartNew();
            GetAllData();

            sw.Stop();
            Debug.WriteLine($"Data Fetched and Processed in {sw.ElapsedMilliseconds} ms");
        }

        private async void DeleteJobAction()
        {
            if (ShowMessageBoxYesNo(
                    $"Are you sure you want to delete the action {SelectedJobAction.ActionName}?\nThis action cannot be undone.", "Confirm") ==
                MessageBoxResult.Yes)
            {
                DataLoading = true;

                await Actions.DeleteActionAsync(SelectedJobAction.Guid);
                _actions.Remove(SelectedJobAction.Guid);

                SelectedJob.Actions.Remove(SelectedJobAction);
                SelectedJobAction = null;

                DataLoading = false;
            }
        }

        private async void AddJobAction()
        {
            DataLoading = true;
            AFAction action = new AFAction();
            action.Guid = Guid.NewGuid();
            action.DateCompleted = null;
            action.ActionName = "New Action";
            action.JobGuid = SelectedJob.Guid;

            action.PropertyChanged += EntityPropertyChanged;

            _actions.Add(action.Guid, action);
            SelectedJob.Actions.Add(action);
            SelectedJobAction = action;

            await Actions.AddActionAsync(Converter.ToApiAction(action));
            DataLoading = false;
        }

        private async void DeleteCustomerAction()
        {
            if (ShowMessageBoxYesNo(
                    $"Are you sure you want to delete the action {SelectedCustomerAction.ActionName}?\nThis action cannot be undone.", "Confirm") ==
                MessageBoxResult.Yes)
            {
                DataLoading = true;
                
                await Actions.DeleteActionAsync(SelectedCustomerAction.Guid);
                _actions.Remove(SelectedCustomerAction.Guid);

                SelectedCustomer.Actions.Remove(SelectedCustomerAction);
                SelectedCustomerAction = null;

                DataLoading = false;
            }
        }

        private async void AddCustomerAction()
        {
            DataLoading = true;
            AFAction action = new AFAction();
            action.Guid = Guid.NewGuid();
            action.DateCompleted = null;
            action.ActionName = "New Action";
            action.CustomerGuid = SelectedCustomer.Guid;

            action.PropertyChanged += EntityPropertyChanged;

            _actions.Add(action.Guid, action);
            SelectedCustomer.Actions.Add(action);
            SelectedCustomerAction = action;

            await Actions.AddActionAsync(Converter.ToApiAction(action));
            DataLoading = false;
        }

        private async void DeleteJob()
        {
            if (ShowMessageBoxYesNo(
                    $"Are you sure you want to delete the job {SelectedJob.Description}?\nThis action cannot be undone.", "Confirm") ==
                MessageBoxResult.Yes)
            {
                DataLoading = true;

                foreach (AFAction action in SelectedJob.Actions)
                {
                    await Actions.DeleteActionAsync(action.Guid);
                    _actions.Remove(action.Guid);
                }

                await Jobs.DeleteJobAsync(SelectedJob.Guid);
                _jobs.Remove(SelectedJob.Guid);

                SelectedCustomer.Jobs.Remove(SelectedJob);
                SelectedJob = null;

                DataLoading = false;
            }
        }

        private async void AddJob()
        {
            DataLoading = true;
            AFJob job = new AFJob();
            job.Guid = Guid.NewGuid();
            job.Description = "New Job";
            job.CustomerGuid = SelectedCustomer.Guid;
            job.PropertyChanged += EntityPropertyChanged;

            _jobs.Add(job.Guid, job);
            SelectedCustomer.Jobs.Add(job);
            SelectedJob = job;

            await Jobs.AddJobAsync(Converter.ToApiJob(job));
            DataLoading = false;
        }

        private async void DeleteCustomer()
        {
            if (ShowMessageBoxYesNo(
                    $"Are you sure you want to delete the customer {SelectedCustomer.Name}?\nThis action cannot be undone.", "Confirm") ==
                MessageBoxResult.Yes)
            {
                DataLoading = true;

                foreach (AFAction action in SelectedCustomer.Actions)
                {
                    await Actions.DeleteActionAsync(action.Guid);
                    _actions.Remove(action.Guid);
                }

                foreach (AFJob job in SelectedCustomer.Jobs)
                {
                    foreach (AFAction action in job.Actions)
                    {
                        await Actions.DeleteActionAsync(action.Guid);
                        _actions.Remove(action.Guid);
                    }

                    await Jobs.DeleteJobAsync(job.Guid);
                    _jobs.Remove(job.Guid);
                }

                await API.Controllers.Customers.DeleteCustomerAsync(SelectedCustomer.Guid);
                _customers.Remove(SelectedCustomer.Guid);
                Customers.Remove(SelectedCustomer);

                SelectedCustomer = null;

                DataLoading = false;
            }
        }

        private async void AddCustomer()
        {
            DataLoading = true;
            AFCustomer customer = new AFCustomer();
            customer.Guid = Guid.NewGuid();
            customer.Name = "New Customer";
            customer.PropertyChanged += EntityPropertyChanged;
            customer.Status = eCustomerStatus.Prospect;
            _customers.Add(customer.Guid, customer);
            Customers.Add(customer);
            SelectedCustomer = customer;

            await API.Controllers.Customers.AddCustomerAsync(Converter.ToApiCustomer(customer));
            DataLoading = false;
        }

        private async void GetAllData()
        {
            DataLoading = true;
            _customers.Clear();
            _actions.Clear();
            _jobs.Clear();
            Customers.Clear();

            List<Customer> allCustomers = await API.Controllers.Customers.GetCustomersAsync();
            List<Job> allJobs = await Jobs.GetJobsAsync();
            List<Action> allActions = await Actions.GetActionsAsync();

            foreach (Customer customer in allCustomers)
            {
                AFCustomer uiCustomer = Converter.ToUiCustomer(customer);
                uiCustomer.PropertyChanged += EntityPropertyChanged;
                _customers.Add(customer.Guid, uiCustomer);
            }

            foreach (Job job in allJobs)
            {
                AFJob uiJob = Converter.ToUiJob(job);
                uiJob.PropertyChanged += EntityPropertyChanged;
                _jobs.Add(job.Guid, uiJob);
            }

            foreach (Action action in allActions)
            {
                AFAction uiAction = Converter.ToUiAction(action);
                uiAction.PropertyChanged += EntityPropertyChanged;
                _actions.Add(action.Guid, uiAction);
            }

            foreach (AFAction action in _actions.Values)
            {
                if (action.CustomerGuid.HasValue)
                {
                    if (_customers.ContainsKey(action.CustomerGuid.Value))
                    {
                        _customers[action.CustomerGuid.Value].Actions.Add(action);
                    }
                }

                if (action.JobGuid.HasValue)
                {
                    if (_jobs.ContainsKey(action.JobGuid.Value))
                    {
                        _jobs[action.JobGuid.Value].Actions.Add(action);
                    }
                }
            }

            foreach (AFJob job in _jobs.Values)
            {
                if (_customers.ContainsKey(job.CustomerGuid))
                {
                    _customers[job.CustomerGuid].Jobs.Add(job);
                }
            }

            foreach (AFCustomer customer in _customers.Values)
            {
                Customers.Add(customer);
            }

            DataLoading = false;
        }

        private void EntityPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is UIModelBase modelBase)
            {
                modelBase.Save();
            }
        }

        public void ShowMessageBox(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }

        public MessageBoxResult ShowMessageBoxYesNo(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButton.YesNo);
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
