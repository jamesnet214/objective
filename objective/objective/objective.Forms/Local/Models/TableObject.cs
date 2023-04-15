using CommunityToolkit.Mvvm.ComponentModel;
using Jamesnet.Wpf.Mvvm;
using objective.Forms.Local.enums;

namespace objective.Forms.Local.Models
{
        public partial class TableObject :ObservableBase
        {
                private TableObjectEnum _tableObjectEnum;
                [ObservableProperty]
                private string data;

                public TableObject(TableObjectEnum tableObject)
                {
                        this._tableObjectEnum = tableObject;
                }
        }
}