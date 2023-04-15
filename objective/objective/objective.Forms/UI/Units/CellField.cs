using objective.Core;
using objective.Core.Enums;
using objective.Models;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace objective.Forms.UI.Units
{
        public class CellField : CellFieldBase
        {
                public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(CellType), typeof(CellField), new PropertyMetadata(CellType.Label));

                public CellType Type
                {
                        get { return (CellType)GetValue(TypeProperty); }
                        set { SetValue(TypeProperty, value); }
                }

                static CellField()
                {
                        DefaultStyleKeyProperty.OverrideMetadata(typeof(CellField), new FrameworkPropertyMetadata(typeof(CellField)));
                }

                public override ReportObjectModel GetProperties()
                {
                        ReportObjectModel obj = new();
                        obj.Width = Width;
                        obj.CellType = Type;
                        obj.FontWeight = FontWeight;
                        obj.FontSize = FontSize;
                        obj.Width = Width;
                        obj.Height = Height;
                        obj.Content = Content;

                        return obj;
                }

                public CellField SetProperties(ReportObjectModel obj)
                {
                        Width = obj.Width;
                        Height = obj.Height;
                        FontWeight = obj.FontWeight;
                        FontSize = obj.FontSize;
                        Content = obj.Content;
                        Type = obj.CellType;

                        return this;
                }
        }
}
