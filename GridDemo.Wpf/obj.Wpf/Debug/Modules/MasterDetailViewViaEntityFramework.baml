   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5
      1&clr-namespace:System;assembly=mscorlibSystem 5      1    +GridDemo.MasterDetailViewViaEntityFramework   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      &localclr-namespace:GridDemo  	 
 5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  5	      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5
      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      ] YDevExpress.Mvvm.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      2sys&clr-namespace:System;assembly=mscorlib   5      $��Stretch=�5      $��Stretch=�6    .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl      Content.  5   
   %  DevExpress.Xpf.Grid.GridControl  -   5        $
��grid��5   '   $��1��6   ) 
 #DevExpress.Xpf.Grid.DataControlBase   AutoGenerateColumns$ KeepOld=�63      DetailDescriptor 5      5 
 /DevExpress.Xpf.Grid.DataControlDetailDescriptor  5      -   5         �Name$ gridDetailDescriptor��5   2   . 
 (DevExpress.Xpf.Grid.DetailDescriptorBase   
ShowHeader .     ItemsSourceBinding 6N   ��  �� Path$ Orders�   DataControl. 5        -   5        $��
detailGrid��5   &    5        5       .   6>   �� $ OrderDetails�. 5   "     -   5        $��subDetailGrid��5   2      Columns 5   &   $  DevExpress.Xpf.Grid.GridColumn  5   *   $ 
 DevExpress.Xpf.Grid.ColumnBase	   	FieldName$	 ProductName��69     5   *   $	 	UnitPrice��69     5   *   $	 Quantity��69     5   *   $	 
TotalPrice��69   
   UnboundType] YDevExpress.Data.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a8  2DevExpress.Utils.Design.UnboundColumnTypeConverter$
 Decimal 6Q      UnboundExpression$ [UnitPrice]*[Quantity]��6h     5   *   $	 Discount��69      TotalSummary 5    &   )	  #DevExpress.Xpf.Grid.GridSummaryItem	  5!   *   )
 
 #DevExpress.Xpf.Grid.SummaryItemBase 
  SummaryType5  /DevExpress.Data.Design.SummaryItemTypeConverter$	 Sum 6>    
  	FieldName$ 
TotalPrice��6P    
  ShowInColumn$ 
TotalPrice��6g   	  5"   *   $	 Sum 6>   $ Quantity��6P   $ Quantity��6e   	  5#   *   $ Count 6>   $ ProductName��6R      View 5%   &   #  DevExpress.Xpf.Grid.TableView  5&   *   -   5        $��subDetailView��5&   8   & 
  DevExpress.Xpf.Grid.DataViewBase   DetailHeaderContent$ Order details��6M      	AutoWidth . &   DevExpress.Xpf.Grid.GridViewBase    UseAnimationWhenExpanding .    ShowColumnHeaders .    AllowFixedColumnMenu .    ShowTotalSummary . 5(   T   5*   #   5+       5-        5.      $	 	OrderDate��6-     5/      $	 ShippedDate��6-     50      $	 ShipPostalCode��6-     51      $	 Freight��6-     52      $	 ShipCountry��6-    54      	  55      $	 Sum 62   $ Freight��6D   $ Freight��6X   	  56      $ Count 62   $ 	OrderDate��6F    58        59      -   5        $��
detailView��59   ,   $ Orders��6>    .  .  .  .  . 5;   ,   5=      5>       5@        5A      $	 ContactName��6!     5B      $	 Country��6!     5C      $
	 City��6!     5D      $	 Address��6!     5E      $	 
PostalCode��6!     5F      $	 
CustomerID��6!    5H      	  5I      $ Count 6&   $ ContactName��6:    5K        5L      -   5        $��
masterView��5L       $ 	Customers��62      AllowScrollAnimation .    AllowFixedGroups5  /DevExpress.Utils.Design.DefaultBooleanConverter$
 true 6n      NavigationStyle$
 Cell=�5M        .  .  . 2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   Triggers 5N      '  !DevExpress.Mvvm.UI.EventToCommand  5O      �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ Loaded��64   +  %DevExpress.Mvvm.UI.EventToCommandBase   CommandParameter$ 1��6G   *   #ProcessEventsFromDisabledEventOwner .    Command 5P   4   �� *Commands.ExpandMasterRow+ �� ElementName$ 
masterView��5R      5T      5U      5V      