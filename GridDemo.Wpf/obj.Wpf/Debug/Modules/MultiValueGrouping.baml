   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5
      1&clr-namespace:System;assembly=mscorlibSystem 5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49atPclr-namespace:DevExpress.Xpf.Editors.Controls;assembly=DevExpress.Xpf.Core.v15.2DevExpress.Xpf.Editors.Controls 5      !    GridDemo.MultiValueGrouping   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      l hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Jdxet?http://schemas.devexpress.com/winfx/2008/xaml/editors/themekeys  5      "localclr-namespace:GridDemo  5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5	      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5
      0sys&clr-namespace:System;assembly=mscorlib  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      \dxconPclr-namespace:DevExpress.Xpf.Editors.Controls;assembly=DevExpress.Xpf.Core.v15.2  5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $J D/GridDemo;component/ModuleResources/MultiValueGroupingTemplates.xaml?�6%   %    5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl  (   !SupressGroupFramePaddingInOptions .     OptionsContent 5   
   �� 5      .+�5      �� -   5        $��groupButton��5   Q   $��Stretch=�6   $	��Top=�67   $	��160��6~   $��22��6�   5         Content. 5   
   %  DevExpress.Xpf.Grid.GridControl  -   5        $
��grid��5         AutoExpandAllGroups . ) 
 #DevExpress.Xpf.Grid.DataControlBase   DesignTimeDataObjectType   GridDemo.Task"  6A      Columns 5      $  DevExpress.Xpf.Grid.GridColumn  5      $ 
 DevExpress.Xpf.Grid.ColumnBase	   	FieldName$	 Complete��6!   $ 
 DevExpress.Xpf.Grid.BaseColumn
   Width$
 80��66      AllowResizing] YDevExpress.Data.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$ False 6A      
FixedWidth . 6W     5       $	 TaskName��6!   $
 80��66      displayTemplate   DisplayTemplate# [  6A     editTemplate   EditTemplate# [ 6t     5!      $	 CategoryName��6!      
GroupIndex$ 0��6:      AllowGrouping$
 True 6I   $	
 150��6^      Header)	  #DevExpress.Xpf.Core.ObjectConverter$ Category	 6j   # [  6|   # [ 6�      ReadOnly . 6�     5"      $	 DueDate��6!   # [  65   # [ 6h     5#      $	 PercentComplete��6!   $
 80��6=   $ 
% Complete	 6H   # [  6\   # [ 6�      EditSettings 5$      6
  0DevExpress.Xpf.Editors.Settings.SpinEditSettings
  5%       
  MaxValue$	 100X�6/    
  MinValue$ 0X�5&      6  0DevExpress.Xpf.Editors.Settings.TextEditSettings   MaskType$ Numeric=�5'         Mask$ P0��5(         MaskUseAsDisplayFormat . 5*      5,        5-      $
	 Note��6!      Visible .   5.      6  0DevExpress.Xpf.Editors.Settings.MemoEditSettings  5/         ShowIcon . ;  5DevExpress.Xpf.Editors.Settings.PopupBaseEditSettings   
PopupWidth$	 250Q�6?      MemoTextWrapping$
 Wrap=�6P   &   MemoVerticalScrollBarVisibility$
 Auto=�6h   51         View 53      #  DevExpress.Xpf.Grid.TableView  54      -   5        $
��view��54   1      	AutoWidth . &   DevExpress.Xpf.Grid.GridViewBase    ShowGroupedColumns  . !   AllowGrouping! .  55      57      58      59      