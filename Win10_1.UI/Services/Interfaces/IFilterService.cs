using System.Collections.ObjectModel;
using Win10_1.Domain.Enums;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.UI.Services.Interfaces;

public interface IFilterService
{
    ObservableCollection<ProjectVM> Filter(
        ObservableCollection<ProjectVM> projects, 
        string projectNameFilter, 
        string themeFilter, 
        Status statusFilter, 
        Priority priorityFilter);
}
