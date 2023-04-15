using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using objective.Core;
using objective.Forms.Local.Models;
using objective.Models;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace objective.Forms.Local.ViewModels
{
        public partial class MainContentViewModel : ObservableBase, IViewLoadable
        {
                [ObservableProperty]
                private ReportObject _selectedObject;

                [ObservableProperty]
                private List<ToolItem> _tools;

                [ObservableProperty]
                private ObservableCollection<ReportObject> _reportSource;

                private readonly IEventAggregator _eh;
                public MainContentViewModel(IEventAggregator eh)
                {
                        _eh = eh;
                        //_eh.GetEvent<ReportLoadEvent>().Subscribe(ReportLoad);
                        Tools = GetTools();
                        this.ReportSource = new();
                }

                public void OnLoaded(IViewable smartWindow)
                {
                }

                private List<ToolItem> GetTools()
                {
                        List<ToolItem> source = new();
                        source.Add(new ToolItem("Title"));
                        source.Add(new ToolItem("Table"));
                        source.Add(new ToolItem("Horizontal Line"));
                        source.Add(new ToolItem("Image"));
                        return source;
                }

                [RelayCommand]
                private void SelectItem(ReportObject item)
                {
                        SelectedObject = item;
                }
        }
}
