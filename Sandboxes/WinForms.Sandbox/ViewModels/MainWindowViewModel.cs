using WinForms.Sandbox.Services;

namespace WinForms.Sandbox.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(LogService logService)
        {
            logService.Log();
        }
    }
}
