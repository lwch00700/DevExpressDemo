   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5	      1&clr-namespace:System;assembly=mscorlibSystem 5
      1clr-namespace:DXDemo.ControlsDXDemo.Controls  5          GridDemo.CollectionView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      "localclr-namespace:GridDemo  5      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5	      0sys&clr-namespace:System;assembly=mscorlib  5
      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      ,controlsclr-namespace:DXDemo.Controls  5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $> 8/GridDemo;component/ModuleResources/SharedResources.xaml?�6%   %    5       � DataContext 5      &    GridDemo.CollectionViewViewModel  5   
   .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl     OptionsContent 5   
   � 5      .+�5      �  6   �� *DataContext+.+�5      -   'DXDemo.Controls.CollectionViewNavigator  -   5          Name$ 	navigator��5   7      CurrentSortFieldName$ JobTitle��6J      CurrentGroupFieldName$ JobTitle��6j      CollectionView 6�   �� *CollectionView+5      5      	   Content.	 5!   
   %  DevExpress.Xpf.Grid.GridControl  -   5        
  Name$

 grid��5!      ) 
 #DevExpress.Xpf.Grid.DataControlBase   ItemsSource 6(   �� *CollectionView+   Columns 5"      $  DevExpress.Xpf.Grid.GridColumn  5#      $ 
 DevExpress.Xpf.Grid.ColumnBase   	FieldName$ JobTitle��6!     5$      $ 	FirstName��6!     5%      $ LastName��6!     5&      $ 	BirthDate��6!     5'      -   5        $ 
 DevExpress.Xpf.Grid.BaseColumn  Name$ 
colCountry��5'   !   $ CountryRegionName��63      Header)	  #DevExpress.Xpf.Core.ObjectConverter$ Country	 6Q      Width$	 130��6b      colCountryNameCellTemplate   CellTemplate# [  6n      View 5)      #
  DevExpress.Xpf.Grid.TableView
  5*      -   5        $
��View��5*       &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupedColumns .  
  	AutoWidth .  
  AllowScrollAnimation .  
  ScrollAnimationDuration$ 0Q�6�    
  ShowAutoFilterRow .  
  NewItemRowPosition$	 Top=�6  & 
  DevExpress.Xpf.Grid.DataViewBase   ShowTotalSummary . $   IsSynchronizedWithCurrentItem 6F   �� *IsSynchronizedWithCurrentItem+ �� ElementName$ 	navigator��6   5,      5-      5.      