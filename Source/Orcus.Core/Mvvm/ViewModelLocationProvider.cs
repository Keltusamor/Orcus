using System;
using System.Diagnostics;

namespace Orcus.Core.Mvvm
{
    public static class ViewModelLocationProvider
    {
        public static Type LocateViewModelType(object view)
        {
            Debugger.Break();
            throw new NotSupportedException("ViewModelType location is not needed for GtkSharp.");
        }
    }
}
