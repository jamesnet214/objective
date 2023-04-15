using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using objective.Forms.Local.Models;

namespace objective.Forms.Local.ViewModels
{
        public partial class MainContentViewModel : ObservableBase, IViewLoadable
        {
                [ObservableProperty]
                private TableObjectCollection tableObject;
                public void OnLoaded(IViewable smartWindow)
                {
                        this.TableObject = new();
                        this.TableObject.Init();
                }

                [RelayCommand]
                private void AreaClicked(string arrow)
                {
                        if (arrow == "LeftArea")
                        {
                                this.TableObject.ColumnsAdd();
                        }
                        else if (arrow == "BottomArea")
                        {
                                this.TableObject.RowsAdd();
                        }
                }
        }
}
