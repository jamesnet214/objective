using objective.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace objective.Forms.Local.Datas
{
        public sealed class StretchModes
    {
        private static readonly Lazy<StretchModes> lazy = new Lazy<StretchModes>(() => new StretchModes());

        public static StretchModes Instance { get { return lazy.Value; } }

        public ObservableCollection<ModeModel> List { get; set; }

        private StretchModes()
        {
            List = new ObservableCollection<ModeModel>
            {
                new ModeModel("None", "None"),
                new ModeModel("Fill", "Fill"),
                new ModeModel("Uniform", "Uniform"),
                new ModeModel("UniformToFill", "UniformToFill"),
            };
        }
    }
}
