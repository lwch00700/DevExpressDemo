   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      D @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo 5      9!clr-namespace:DevExpress.DemoDataDevExpress.DemoData 5	      I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5         GridDemo.FixedDataColumns   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6sys&clr-namespace:System;assembly=mscorlib     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      "localclr-namespace:GridDemo  5      ,dxdd!clr-namespace:DevExpress.DemoData  5	      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5
      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      $��Stretch=�5      $��Stretch=�6      � 	Resources  5       �� MergedDictionaries �� Source)  #DevExpress.DemoData.NWindDataLoader   NWindDataLoader�� 5   
    5      �� 5      $H B/GridDemo;component/ModuleResources/FixedDataColumnsTemplates.xaml?�6%   %   &	          5      6#   5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl  (   !SupressGroupFramePaddingInOptions .     OptionsContent 5   
   �� 5      .+�5      &   DevExpress.Xpf.Editors.CheckEdit     Content)  #DevExpress.Xpf.Core.ObjectConverter$ Enable Fixed Column Menu 6b   $��0,0,0,4q�6�    ^� ToolTip$O IAllows to fix columns to the left or right using a column's context menu.��6�      	IsChecked 6    ��  �� Path$ View.AllowFixedColumnMenu�	 �� Mode$	 TwoWay=�6�   +  %DevExpress.Xpf.DemoBase.SliderControl  5      
   Header$
 Fixed Line Width��5   !      Minimum$ 2Q�6;      Maximum$ 12Q�6G      	SmallStep$ 1Q�6T      	LargeStep$ 1Q�6b      ValueMinWidth$ 15Q�6p   $��0,0,0,4q�6�      	EditValue 5   %   �� *View.FixedLineWidth+$	 TwoWay=�  RoundValueConverter �� 	Converter# [ 5   �     5      $' !Extend ScrollBar To Fixed Columns 6k   $��0,0,0,4q�6�    6    �� $( "View.ExtendScrollBarToFixedColumns�$	 TwoWay=�6�   5         Content. 5!   
   %  DevExpress.Xpf.Grid.GridControl  -   5        $
��grid��5!      $��Stretch=�6&   $��Stretch=�6D   ) 
 #DevExpress.Xpf.Grid.DataControlBase   ItemsSource 6`   �� $ 	Customers� �� Source# [     View 5"      #  DevExpress.Xpf.Grid.TableView  5#      -   5          Name$ gridView��5#   m      AllowScrollAnimation . &	 
  DevExpress.Xpf.Grid.DataViewBase 	  AllowEditing .     AllowFixedColumnMenu .   columnOptionsTemplate, 	  %ColumnHeaderCustomizationAreaTemplate# [ 6      Columns 5%      $
  DevExpress.Xpf.Grid.GridColumn
  5&      $ 
 DevExpress.Xpf.Grid.ColumnBase   	FieldName$ CompanyName��6!   $ 
 DevExpress.Xpf.Grid.BaseColumn   Width$	 230��69      Fixed$
 Left=�6E   
  5'      $ Phone��6!   $	 100��63   $ Right=�6?   
  5(      $ ContactName��6!   $	 150��69   
  5)      $ ContactTitle��6!   $	 150��6:   
  5*      $ Region��6!   $	 100��64   
  5+      $ Country��6!   $ 90��65   
  5,      $
 City��6!   $	 100��62   
  5-      $ Address��6!   $	 260��65   
  5.      $ 
PostalCode��6!   $	 110��68   
  5/      $	 Fax��6!   $	 110��61   
  50      $ 
CustomerID��6!   $ 70��68   52      53      54      