using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts
{
    public enum Status{
        Stopped,
        Started,
        Running,
        Completed
    }
  public  interface IWorkUnit
    {
        Status StatusOfWork { get; }
        void OnWorkCompleted();
        void ProgressChanged();
        void DoWork();
        void CancelWork();
        event EventHandler StatusChanged;

    }
}
