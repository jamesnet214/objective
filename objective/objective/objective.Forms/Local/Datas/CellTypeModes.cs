using objective.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace objective.Forms.Local.Datas
{
        public sealed class CellTypeModes
    {
        private static readonly Lazy<CellTypeModes> lazy = new Lazy<CellTypeModes>(() => new CellTypeModes());

        public static CellTypeModes Instance { get { return lazy.Value; } }

        public ObservableCollection<ModeModel> List { get; set; }

        private CellTypeModes()
        {
            List = new ObservableCollection<ModeModel>
            {
                new ModeModel("Label", "Label"),
                new ModeModel("Value", "Value"),
            };
        }
    }
}
