using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Main.Utils
{
    public class UnitOfWork : IWorkUnit
    {
        private Status statusOfWork;
        private BackgroundWorker worker;
        private Action action;
        private Task currentTask;
        private bool isElevated;

        public event EventHandler StatusChanged;

        public UnitOfWork()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            isElevated = false;
        }
        public UnitOfWork(Action action,bool isElevated):this()
        {
            this.action = action;
            this.isElevated = isElevated;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusOfWork = Status.Completed;
            OnWorkCompleted();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChanged();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            StatusOfWork = Status.Running;
            if(isElevated)
                Application.Current.Dispatcher.Invoke(()=>currentTask = Task.Run(action));
            else
                currentTask = Task.Run(action);
        }

        public Status StatusOfWork
        {
            get { return statusOfWork; }

         private
                set { statusOfWork = value;
                if (StatusChanged != null)
                    StatusChanged(this, null);
            }
        }

        public void CancelWork()
        {
        }

        public void DoWork()
        {
            StatusOfWork = Status.Started;
            worker.RunWorkerAsync();
        }
        
        public void OnWorkCompleted()
        {
           
        }

        public void ProgressChanged()
        {
           
        }

      
    }
}
