   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DevExpress.DemoDataDevExpress.DemoData  5      #clr-namespace:GridDemoGridDemo  5	      #clr-namespace:GridDemoGridDemo  5
      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5      .    (GridDemo.GridCellMultiColumnLookupEditor   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      ,dxdd!clr-namespace:DevExpress.DemoData  5      "localclr-namespace:GridDemo  5	      #dxdemoclr-namespace:GridDemo  5
      0sys&clr-namespace:System;assembly=mscorlib  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5        � 	Resources  5       �� MergedDictionaries �� Source)   #DevExpress.DemoData.NWindDataLoader   NWindDataLoader�� 5   
    5      �� 5      $W Q/GridDemo;component/ModuleResources/GridCellMultiColumnLookupEditorResources.xaml?�6%   %   &	          5      6#   5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl     OptionsContent 5   
   �� 5      $	��250��6   .+�5      $  DevExpress.Xpf.Core.GroupFrame  $��
Properties��6    $��0,0,0,12q�64   .��5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6/   �� 5       � RowDefinitions 5!      �� 5"      �� 5#      �� 5$      �� 5%      .+�5'      &   DevExpress.Xpf.Editors.CheckEdit  -   5        $��chkShowSizeGrip��5'   s      Content)  #DevExpress.Xpf.Core.ObjectConverter$ Show size grip 6(   $��2��6A   $��0��6U   $��0,0,0,4q�6b      	IsChecked$
 TrueY�6�     5(      -   5        $��chkImmediatePopup��5(   u   $ Immediate pop-up 6(   $��2��6C   $��1��6W   $��0,0,0,4q�6d   $
 TrueY�6�     5)      -   5        $��chkAllowAutoComplete��5)   x   $ Allow auto complete 6(   $��2��6F   $��2��6Z   $��0,0,0,4q�6g   $
 TrueY�6�   5*      5+      5,         Content. 5.   
   %  DevExpress.Xpf.Grid.GridControl  -   5        $
��grid��5.      ) 
 #DevExpress.Xpf.Grid.DataControlBase	   ItemsSource	 6&   �� *Orders+
 �� Source#
 [     Columns 5/      $  DevExpress.Xpf.Grid.GridColumn  50      $	 
 DevExpress.Xpf.Grid.ColumnBase 	  	FieldName$ OrderID��6!   $
 
 DevExpress.Xpf.Grid.BaseColumn 
  Header$ ID 65    
  Width$ 50��6A    
  MinWidth$ 30��6L     51      $ 
CustomerID��6!   $ Customer 68   $ 50��6J   $	 180��6X     headerTemplate 
  HeaderTemplate# [ 6d    	  EditSettings 52      3  -DevExpress.Xpf.Grid.LookUp.LookUpEditSettings  53      <  6DevExpress.Xpf.Editors.Settings.LookUpEditSettingsBase   ValueMember$ 
CustomerID��61      DisplayMember$ CompanyName��6J     customerGridTemplate;  5DevExpress.Xpf.Editors.Settings.PopupBaseEditSettings   PopupContentTemplate# [ 6�      AutoPopulateColumns .     IsPopupAutoWidth .     ItemsSource 6f   �� *	Customers+#
 [     AutoComplete 54   1   ��  �� Path$ 	IsChecked� �� ElementName$ chkAllowAutoComplete�� �� Mode$ TwoWay=�   ImmediatePopup 55   1   �� $ 	IsChecked�$ chkImmediatePopup��$ TwoWay=�   ShowSizeGrip 56   1   �� $ 	IsChecked�$ chkShowSizeGrip��58      5:        5;      $ 
EmployeeID��6!   $ Employee 68   $ 50��6J   # [ 6X    5<        5=      $ 
EmployeeID��61   $ LastName��6J     employeeGridTemplate# [ 6�    .   .   6c   �� *	Employees+#
 [   5>   1   �� $ 	IsChecked�$ chkAllowAutoComplete��$ TwoWay=� 5?   1   �� $ 	IsChecked�$ chkImmediatePopup��$ TwoWay=� 5@   1   �� $ 	IsChecked�$ chkShowSizeGrip��5A      5C        5D      $ 	OrderDate��6!   $ 30��67     5E      $ RequiredDate��6!   $ 30��6:      View 5G      #  DevExpress.Xpf.Grid.TableView  5H      -   5        $
��view��5H          	AutoWidth . 6,   5J      5K      5L      