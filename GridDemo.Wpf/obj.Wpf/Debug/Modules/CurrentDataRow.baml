   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089M4clr-namespace:System.Globalization;assembly=mscorlibSystem.Globalization  5      D @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo 5      9!clr-namespace:DevExpress.DemoDataDevExpress.DemoData 5
      I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      1&clr-namespace:System;assembly=mscorlibSystem  5         GridDemo.CurrentDataRow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Gglobal4clr-namespace:System.Globalization;assembly=mscorlib     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      "localclr-namespace:GridDemo  5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5	      ,dxdd!clr-namespace:DevExpress.DemoData  5
      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5      0sys&clr-namespace:System;assembly=mscorlib  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5        � 	Resources  5       �� MergedDictionaries �� Source)  #DevExpress.DemoData.NWindDataLoader   NWindDataLoader�� 5   
    5      �� 5      $- '/GridDemo;component/Themes/generic.xaml?�6%   �� 5      $F @/GridDemo;component/ModuleResources/CurrentDataRowTemplates.xaml?�6%   %   &	          5      6#   5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl     Content. 5   
   � $��12q�6    � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      %  DevExpress.Xpf.Grid.GridControl  -   5        $
��grid��5      ) 
 #DevExpress.Xpf.Grid.DataControlBase   ItemsSource 6*   ��  �� Path$ 	Employees� �� Source# [     Columns 5       $  DevExpress.Xpf.Grid.GridColumn  5!      $ 
 DevExpress.Xpf.Grid.ColumnBase	   	FieldName$	 	FirstName��6%   $ 
 DevExpress.Xpf.Grid.BaseColumn
   MinWidth$	
 100��6;     5"      $	 LastName��6%   $	
 100��6:     5#      $	 	BirthDate��6%   $	
 100��6;      EditSettings 5$      6  0DevExpress.Xpf.Editors.Settings.DateEditSettings  5%      6	  0DevExpress.Xpf.Editors.Settings.BaseEditSettings 	  DisplayFormat$ d��63   5'        5(      $	 Title��6%   $	
 100��67      Header)
  #DevExpress.Xpf.Core.ObjectConverter$ Position
 6F     5)      $	 Notes��6%   $	
 100��67    5*      6  0DevExpress.Xpf.Editors.Settings.MemoEditSettings  5+         ShowIcon . ;  5DevExpress.Xpf.Editors.Settings.PopupBaseEditSettings   
PopupWidth$	 250Q�6C      MemoTextWrapping$
 Wrap=�6T   &   MemoVerticalScrollBarVisibility$
 Auto=�6l   5-         View 5/      #  DevExpress.Xpf.Grid.TableView  50      -   5        $
��view��50   >   &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupedColumns .    	AutoWidth . 51      53      �� 54      $��1��6     rowDetailTemplate#��[ 6d   $��Stretch=�6�   ��6,   �� $ SelectedItem� �� ElementName$
 grid��6�   55      56      57      