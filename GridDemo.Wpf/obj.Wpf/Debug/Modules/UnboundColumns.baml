   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      9!clr-namespace:DevExpress.DemoDataDevExpress.DemoData  5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5
      1&clr-namespace:System;assembly=mscorlibSystem 5          GridDemo.UnboundColumns   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      "localclr-namespace:GridDemo  5      ,dxdd!clr-namespace:DevExpress.DemoData  5	      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5
      0sys&clr-namespace:System;assembly=mscorlib  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5        � 	Resources  5       �� MergedDictionaries �� Source)   #DevExpress.DemoData.NWindDataLoader   NWindDataLoader�� 5   
    5      �� 5      $F @/GridDemo;component/ModuleResources/UnboundColumnsTemplates.xaml?�6%   %   &	          5      6#   5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl     OptionsContent 5   
   �� 5      $	��160��6   .+�5      $  DevExpress.Xpf.Core.GroupFrame  $��Column:��6    .��5      (  "DevExpress.Xpf.Editors.ListBoxEdit  -   5        $��columnsList��5   O   %  DevExpress.Xpf.Editors.BaseEdit   
ShowBorder .     SelectedIndex$ 0��6b   ��6&   Z�    StyleSettings 5      :  4DevExpress.Xpf.Editors.RadioListBoxEditStyleSettings  5         Items 5      ,  &DevExpress.Xpf.Editors.ListBoxEditItem  5       
 � Tag$ DiscountAmount��62   .��6G   Discount Amount6X     5!      $ Total��62   .��6>   Total6E     5"      $ 
TotalScale��62   .��6C   Total Scale6P   5$      5%      �� 5&      -   5        $ ��showExpressionEditorButton��5&   +   $��0,12,0,0q�6   .��6v   Show Expression Editor6�   5'      	   Content.	 5)   
   %  DevExpress.Xpf.Grid.GridControl  -   5        $
��grid��5)      $��0��6&   )	 
 #DevExpress.Xpf.Grid.DataControlBase
 	  ItemsSource
 66   ��  �� Path$ Invoices� �� Source# [     Columns 5*      $
  DevExpress.Xpf.Grid.GridColumn
  5+      -   5         
 Name$ 
colOrderID��5+   !   $ 
 DevExpress.Xpf.Grid.ColumnBase   	FieldName$ OrderID��65   $ 
 DevExpress.Xpf.Grid.BaseColumn   Width$ 80��6I      
FixedWidth .  
  
GroupIndex$ 0��6f   
  5,      -   5        $ 
colProduct��5,   !   $ 	ProductID��65      MinWidth$	 200��6K   $	 200��6Z      EditSettings 5-      :  4DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings  5.      <  6DevExpress.Xpf.Editors.Settings.LookUpEditSettingsBase   AutoComplete .    ImmediatePopup .    ValueMember$ 	ProductID��6]      DisplayMember$ ProductName��6u      ItemsSource 6�   �� $ Products�# [  50      
  51      -   5        $ colUnitPrice��51   !   $ 	UnitPrice��67   $ 30��6M    52      6  0DevExpress.Xpf.Editors.Settings.TextEditSettings  53         MaskType$ Numeric=�6/      MaskUseAsDisplayFormat .    Mask$ c��6`      MaskCulture$ en-US��6i   55      
  56      -   5        $ colDiscount��56   !   $ Discount��66   $ 30��6K    57        58      $ Numeric=�6/    . $ p0��6`   5:      
  5;      -   5        $ colQuantity��5;   !   $ Quantity��66   $ 30��6K   
  5<      -	   5        $ colDiscountAmount��5<   !   $ DiscountAmount��6<      UnboundType] YDevExpress.Data.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a8  2DevExpress.Utils.Design.UnboundColumnTypeConverter$ Decimal 6W      UnboundExpression$/ )Round([UnitPrice] * [Quantity] - [Total])��6m   $	 100��6�       AllowEditing5  /DevExpress.Utils.Design.DefaultBooleanConverter$  False 6�   #!   AllowUnboundExpressionEditor! .  5=        5>      6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings"   DisplayFormat$" $0.00��6/   5@      
  5A      -
   5        $ colTotal��5A   !   $ Total��63   $ Decimal 6E   $3 -([UnitPrice] * [Quantity]) * (1 - [Discount])��6[     totalCellTemplateSelector#   CellTemplateSelector## [ 6�   $	 100��6�   $  False 6�   ! .  5B        5C      $" $0.00��6/   5E      
  5F      -   5        $ colTotalScale��5F   !   $ 
TotalScale��68   $   Visible$ .  %   ShowInColumnChooser% .  $ Decimal 6{   $I CIif([Total] < 1000, 0.4, Min((0.5 + ([Total] - 1000) / 1000), 1.2))��6�   ! . 6�   &   GroupSummary& 5H      )  #DevExpress.Xpf.Grid.GridSummaryItem  5I      ) 
 #DevExpress.Xpf.Grid.SummaryItemBase'   	FieldName$' 	ProductID��6&   (   SummaryType5  /DevExpress.Data.Design.SummaryItemTypeConverter$( Count 6<     5J      $' Total��6&   $	( Sum 68   )   TotalSummary) 5L        5M      $( Count 6&   *   	Alignment$* Right=�6:     5N      $' Total��6&   $	( Sum 68   $
* Left=�6J   +   View+ 5P      #  DevExpress.Xpf.Grid.TableView  5Q      ,   	AutoWidth, . & 
  DevExpress.Xpf.Grid.DataViewBase-   ShowFixedTotalSummary- . 61   5S      5T      5U      