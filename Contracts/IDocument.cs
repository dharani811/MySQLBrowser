using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;


namespace Contracts
{
    public interface IDocument
    {
        ICommand Connect { get; }
    }
}
