   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      !    GridDemo.RealtimeDataSource   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:GridDemo     5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid    5	      a	 ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors 	 5
      c
 _DevExpress.Xpf.Charts.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxc4http://schemas.devexpress.com/winfx/2008/xaml/charts 
 5        � 	Resources  5         NumericGridColumnStyle �� 
TargetType$  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.ColumnBase �� Property �� Value6 	 0DevExpress.Xpf.Editors.Settings.TextEditSettings   MaskType   Mask   MaskUseAsDisplayFormat$  DevExpress.Xpf.Grid.BaseColumn��%�   &	        �� 5   
   "  6/   .�5      ��  �@ EditSettings 5        5      $ Numeric=�6+   $ f��6>    . 6G   5      �� 5       �@ Width$ 80��6&   5      5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl  (   !SupressGroupFramePaddingInOptions .     OptionsContent 5   
   �� 5      .+�5      �� -   5        $��	groupBox1��5   ^   $��Speed of change��6   $��50��64   $��Stretch=�6@   $	��Top=�6o   $��14(�6�   .��5      �� -   5        $��slider��5   G   $��30��6   $��Stretch=�6)   $��Center=�6U   	 � Maximum$	 34Q�6�   
 � Value$
 34Q�6�    �� TickPlacement$ BottomRight=�6�   5      �� 5      -   5        $��0,10,0,0q�5      $��14(�6-   $��Use RealTimeSource��6;    d� 	IsChecked$
 TrueY�6�   5         Content. 5!   
   �  � ColumnDefinitions 5"      �� 5#      $
��0.3* �6#   �� 5$      $
��0.7* �6#   .+�5&      ( 
 "DevExpress.Xpf.Charts.ChartControl     CrosshairEnabled$ FalseX�6   $��0,0,0,0q�68      Diagram 5'      ' 
 !DevExpress.Xpf.Charts.XYDiagram2D  5(      # 
 DevExpress.Xpf.Charts.Diagram   Series 5)      /	 
 )DevExpress.Xpf.Charts.LineScatterSeries2D	  5*      -   5         	 Name$ 
UPSDiagram��5*   6   "
 
 DevExpress.Xpf.Charts.Series 
  ArgumentScaleType$ 	Numerical=�6J   ( 
 "DevExpress.Xpf.Charts.LineSeries2D   
MarkerSize$ 2��6h    
  Label 5+   "   ' 
 !DevExpress.Xpf.Charts.SeriesLabel  5,   &      Visible .    ResolveOverlappingMode$ HideOverlapped=�6E    
  PointOptions 5.   "   ( 
 "DevExpress.Xpf.Charts.PointOptions  5/   &      ValueNumericOptions 50   *   * 
 $DevExpress.Xpf.Charts.NumericOptions  51   .      Format$ 
FixedPoint=�6A   53   '   55         AxisX 57      # 
 DevExpress.Xpf.Charts.AxisX2D  58      $ 
 DevExpress.Xpf.Charts.AxisBase   GridLinesVisible .  " 
 DevExpress.Xpf.Charts.Axis2D   TickmarksVisible .     TickmarksMinorVisible .     Visible$ FalseX�6z       Label  59   "   % 
 DevExpress.Xpf.Charts.AxisLabel  5:   &   5<      !   AxisY! 5>      # 
 DevExpress.Xpf.Charts.AxisY2D  5?       .  .   
 DevExpress.Xpf.Charts.Axis"   Logarithmic" . 6_   5A      #   Titles# 5C      ! 
 DevExpress.Xpf.Charts.Title  5D      $   Dock$	$ Top=�6    % 
 DevExpress.Xpf.Charts.TitleBase%   HorizontalAlignment$% Center=�6+   &   Content$& Update per second��6H   $��16(�6d   5F      %  DevExpress.Xpf.Grid.GridControl  5G      -   5        $
��grid��5G      $��0,0,0,0q�6*   $��1��6;   '   View' 5H      #  DevExpress.Xpf.Grid.TableView  5I      (   	AutoWidth( . &   DevExpress.Xpf.Grid.DataViewBase)   AllowEditing) .  65   *   Columns* 5K        5L      +   	FieldName$
+ Date��6%   ,   Width$, 80��67     5M      $+ Ticker��6%     5N      $
+ Last��6%   #��[  66     5O      $+ 
ChgPercent��6%   -   Header) 	 #DevExpress.Xpf.Core.ObjectConverter$
- Chg% 6<   .   EditSettings. 5P        5Q      $ Numeric=�63   $ p��6F    . 6O   5S        5T      $
+ Open��6%   #��[  66     5U      $
+ High��6%   #��[  66     5V      $	+ Low��6%   #��[  65     5W      $+ DayVal��6%   $, 80��69   . 5X      = 	 7DevExpress.Xpf.Editors.Settings.ProgressBarEditSettings  5Y      ; 	 5DevExpress.Xpf.Editors.Settings.RangeBaseEditSettings/   Minimum$	/ 300Q�6:   0   Maximum$
0 1000Q�6H   5[      5]      5^      5_      5`      