   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089M4clr-namespace:System.Globalization;assembly=mscorlibSystem.Globalization  5      D @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo 5      #clr-namespace:GridDemoGridDemo 5
      I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      1&clr-namespace:System;assembly=mscorlibSystem  5      9!clr-namespace:DevExpress.DemoDataDevExpress.DemoData 5         GridDemo.CustomCardLayout   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Gglobal4clr-namespace:System.Globalization;assembly=mscorlib     5      )dxdemoclr-namespace:GridDemo     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5	      "localclr-namespace:GridDemo  5
      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5      0sys&clr-namespace:System;assembly=mscorlib  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      0demoData!clr-namespace:DevExpress.DemoData  5        � 	Resources  5       �� MergedDictionaries �� Source  GridDemo.CountriesData   CountriesData�� 5   
    5      �� 5      $H B/GridDemo;component/ModuleResources/CustomCardLayoutResources.xaml?�6%   %   &	          5      6"   5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl    5   
   )  #DevExpress.DemoData.NWindDataLoader  NWindDataLoader��%   &	         5      6'   5         Content. 5   
   %  DevExpress.Xpf.Grid.GridControl  -   5        $
��grid��5      $��0��6&   ) 
 #DevExpress.Xpf.Grid.DataControlBase   ItemsSource 66   ��  �� Source# [  �� Path$ 	Employees�   Columns 5      $  DevExpress.Xpf.Grid.GridColumn  5       $ 
 DevExpress.Xpf.Grid.ColumnBase   	FieldName$ 	FirstName��6!     5!      $ LastName��6!     5"      $ Photo��6!   	   AllowColumnFiltering] YDevExpress.Data.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$	 False 63     5#      $ 	BirthDate��6!   
   EditSettings
 67   E	  ?DevExpress.Xpf.Editors.Settings.Extension.DateSettingsExtension	  E
  ?DevExpress.Xpf.Editors.Settings.Extension.TextSettingsExtension 
  DisplayFormat$ d��  5$      -   5          Name$ colTitle��5$   !   $ Title��63   
 6E   I  CDevExpress.Xpf.Editors.Settings.Extension.ComboBoxSettingsExtension  G  ADevExpress.Xpf.Editors.Settings.Extension.ButtonSettingsExtension   IsTextEditable$ falseX�   Titles   ItemsSource# Z   5%      $ Address��6!     5&      $
 City��6!     5'      $ 
PostalCode��6!     5(      $ Country��6!   
 5)      :  4DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings  5*      <  6DevExpress.Xpf.Editors.Settings.LookUpEditSettingsBase   ValueMember$ 
ActualName��6�      DisplayMember$ 
ActualName��6�      ItemsSource 63   �� $ 
DataSource�# [  6�   5,        5-      $ 	HomePhone��6!     5.      $ Notes��6!   $ 
 DevExpress.Xpf.Grid.BaseColumn   MinWidth$	 100��63   
 6B   E  ?DevExpress.Xpf.Editors.Settings.Extension.MemoSettingsExtension     ShowIcon .  J  DDevExpress.Xpf.Editors.Settings.Extension.PopupBaseSettingsExtension   
PopupWidth$	 250Q�   MemoTextWrapping$
 Wrap=�&   MemoVerticalScrollBarVisibility$
 Auto=�   View 50      "  DevExpress.Xpf.Grid.CardView  51      -   5        $
��view��51   A   & 
  DevExpress.Xpf.Grid.DataViewBase   EditorButtonShowMode$ 
ShowAlways=�6   &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupedColumns .   headerTemplateFullName   CardHeaderTemplate# �  6g     cardTemplate2   CardTemplate# �  52         AllowColumnMoving .   & "CustomCardLayout_PrintCardTemplate   PrintCardTemplate# [ 6h    , (CustomCardLayout_PrintCardHeaderTemplate   PrintCardHeaderTemplate# [ 6�       PrintAutoCardWidth  . !   PrintMaximumCardColumns$! 2��6  54      55      56      