using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jamesnet.Wpf.Controls;
using Jamesnet.Wpf.Mvvm;
using objective.Forms.Local.EventArgs;
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
                private void AreaClicked(TableCustomArgs args)
                {
                        if(args.mouseButton == System.Windows.Input.MouseButton.Left)
                        {
                                if(args.gridAddType == enums.GridAddType.Columns)
                                {
                                        this.TableObject.ColumnsAdd();
                                }
                                else
                                {
                                        this.TableObject.RowsAdd();
                                }
                        }
                        else if(args.mouseButton == System.Windows.Input.MouseButton.Right)
                        {
                                if (args.gridAddType == enums.GridAddType.Columns)
                                {
                                        this.TableObject.ColumnsDelete();
                                }
                                else
                                {
                                        this.TableObject.RowsDelete();
                                }
                        }
                }
        }
}
