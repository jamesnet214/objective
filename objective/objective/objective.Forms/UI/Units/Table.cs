using Jamesnet.Wpf.Controls;
using objective.Core;
using objective.Core.Enums;
using objective.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace objective.Forms.UI.Units
{
        public class Table : DragMoveContent
        {
                public static readonly DependencyProperty RowsProperty = DependencyProperty.Register("Rows", typeof(string), typeof(Table), new PropertyMetadata(""));
                public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof(string), typeof(Table), new PropertyMetadata(""));
                private JamesGrid _grid;
                private List<ReportObjectModel> _cellFields;

                public string Rows
                {
                        get { return (string)GetValue(RowsProperty); }
                        set { SetValue(RowsProperty, value); }
                }

                public string Columns
                {
                        get { return (string)GetValue(ColumnsProperty); }
                        set { SetValue(ColumnsProperty, value); }
                }

                static Table()
                {
                        DefaultStyleKeyProperty.OverrideMetadata(typeof(Table), new FrameworkPropertyMetadata(typeof(Table)));
                }
                public Table()
                {
                        MouseLeftButtonDown += Table_MouseLeftButtonDown;
                }

                private void Table_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {
                        UIElement clickedElement = e.Source as UIElement;

                        if (clickedElement is CellField)
                        {

                        }
                }

                protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
                {
                        base.OnPreviewMouseLeftButtonDown(e);

                }

                public override ReportObjectModel GetProperties()
                {
                        ReportObjectModel obj = new();
                        obj.Type = this.GetType();
                        obj.Width = Width;
                        obj.Height = Height;
                        obj.FontWeight = FontWeight;
                        obj.FontSize = FontSize;
                        obj.Top = Canvas.GetTop(this);
                        obj.Left = Canvas.GetLeft(this);
                        obj.Rows = Rows;
                        obj.Columns = Columns;
                        obj.CellFields = new();

                        if (_grid != null)
                        {
                                foreach (ReportObject item in _grid.Children)
                                {
                                        var cellField = item.GetProperties();
                                        obj.CellFields.Add(cellField);
                                }
                        }

                        return obj;
                }

                public Table SetProperties(ReportObjectModel obj)
                {
                        Width = obj.Width;
                        Height = obj.Height;
                        FontWeight = obj.FontWeight;
                        FontSize = obj.FontSize;
                        Rows = obj.Rows;
                        Columns = obj.Columns;
                        Canvas.SetTop(this, obj.Top);
                        Canvas.SetLeft(this, obj.Left);

                        _cellFields = obj.CellFields;
                        return this;
                }

                public override void OnApplyTemplate()
                {
                        base.OnApplyTemplate();
                        _grid = GetTemplateChild("PART_JamesGrid") as JamesGrid;
                        _grid.Rows = Rows;
                        _grid.Columns = Columns;

                        if (_cellFields != null)
                        {
                                foreach (var item in _cellFields)
                                {
                                        _grid.Children.Add(new CellField().SetProperties(item));
                                }
                        }
                        else
                        {
                                SetCellField();
                        }
                }

                private void SetCellField()
                {
                        List<CellField> fields = new();
                        foreach (CellField item in _grid.Children)
                        {
                                fields.Add(item);
                        }
                        _grid.Children.Clear();

                        for (int i = 0; i < GetAllCell(); i++)
                        {
                                if (fields.FirstOrDefault() is CellField cf)
                                {
                                        fields.Remove(cf);
                                        _grid.Children.Add(cf);
                                }
                                else
                                {
                                        _grid.Children.Add(new CellField { Type = CellType.Label });
                                }
                        }
                }
                protected override void OnMouseDown(MouseButtonEventArgs e)
                {
                        if (e.OriginalSource is FrameworkElement source)
                        {
                                var tag = (string)source.Tag;

                                if (tag == null)
                                        return;
                                if (tag == "RightArea")
                                {
                                        if (e.ChangedButton == MouseButton.Left)
                                        {
                                                this.Columns += ", *";
                                                SetCellField();
                                        }
                                        else
                                        {
                                                var ListData = this.Columns.Replace(" ", "").Split(',').ToList();

                                                ListData.Remove(ListData.Last());
                                                this.Columns = String.Join(", ", ListData);
                                                SetCellField();
                                        }
                                     
                                }
                                else
                                {
                                        if (e.ChangedButton == MouseButton.Left)
                                        {
                                                this.Rows += ", *";
                                                SetCellField();
                                        }

                                        if (e.ChangedButton == MouseButton.Right)
                                        {
                                                var ListData = this.Rows.Replace(" ", "").Split(',').ToList();

                                                ListData.Remove(ListData.Last());
                                                this.Rows = String.Join(", ", ListData);
                                                SetCellField();
                                        }
                                }
                        }
                }

                private int GetAllCell()
                {
                        int columnsCnt = this.Columns.Count(x => (x == ',')) + 1;
                        int rowCnt = this.Rows.Count(x => (x == ',')) + 1;

                        return columnsCnt * rowCnt;
                }
        }
}
