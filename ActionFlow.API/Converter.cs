using System;
using System.Collections.Generic;
using System.Text;
using ActionFlow.API.Models;
using ActionFlow.API.UIModels;
using Action = ActionFlow.API.Models.Action;

namespace ActionFlow.API
{
    public static class Converter
    {
        #region Customer Converters
        public static Customer ToApiCustomer(AFCustomer uiCustomer)
        {
            Customer customer = new Customer();
            customer.Guid = uiCustomer.Guid;
            customer.Name = uiCustomer.Name;
            customer.Status = uiCustomer.Status;

            return customer;
        }

        public static AFCustomer ToUiCustomer(Customer customer)
        {
            AFCustomer uiCustomer = new AFCustomer();
            uiCustomer.Guid = customer.Guid;
            uiCustomer.Name = customer.Name;
            uiCustomer.Status = customer.Status;
            uiCustomer.IsModified = false;

            return uiCustomer;
        }

        #endregion
        #region Action Converter

        public static Action ToApiAction(AFAction uiAction)
        {
            Action action = new Action();
            action.Guid = uiAction.Guid;
            action.CustomerGuid = uiAction.CustomerGuid;
            action.JobGuid = uiAction.JobGuid;
            action.ActionName = uiAction.ActionName;
            action.DateCompleted = uiAction.DateCompleted;

            return action;
        }

        public static AFAction ToUiAction(Action action)
        {
            AFAction uiAction = new AFAction();
            uiAction.Guid = action.Guid;
            uiAction.CustomerGuid = action.CustomerGuid;
            uiAction.JobGuid = action.JobGuid;
            uiAction.ActionName = action.ActionName;
            uiAction.DateCompleted = action.DateCompleted;
            uiAction.IsModified = false;

            return uiAction;
        }

        #endregion
        #region Job Converter

        public static Job ToApiJob(AFJob uiJob)
        {
            Job job = new Job();
            job.Guid = uiJob.Guid;
            job.Description = uiJob.Description;
            job.CustomerGuid = uiJob.CustomerGuid;

            return job;
        }

        public static AFJob ToUiJob(Job job)
        {
            AFJob uiJob = new AFJob();
            uiJob.Guid = job.Guid;
            uiJob.Description = job.Description;
            uiJob.CustomerGuid = job.CustomerGuid;
            uiJob.IsModified = false;

            return uiJob;
        }

        #endregion

    }
}
