   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a,   &DevExpress.Xpf.Core.ImageDataConverter   ImageDataConverter'   !GridDemo.CustomerDetailsConverter  CustomerDetailsConverter  employeeDetailTemplate-  'DevExpress.Xpf.Core.MeasurePixelSnapper  � ColumnDefinitions&   DevExpress.Xpf.Editors.ImageEdit   ShowMenu%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly   Source �� Path �� 	Converter � RowDefinitions �� Orientation  employeeGridRowTemplate  employeeGridTemplate%  DevExpress.Xpf.Grid.GridControl  Name	   Columns$  DevExpress.Xpf.Grid.GridColumn$ 	 DevExpress.Xpf.Grid.ColumnBase
   	FieldName   View#  DevExpress.Xpf.Grid.TableView   	AutoWidth&	 	  DevExpress.Xpf.Grid.DataViewBase 	  ShowColumnHeaders   RowDetailsVisibilityMode   RowDetailsTemplate  customerDetailTemplate  customerGridRowTemplate ��Name3
  -DevExpress.Xpf.Grid.RowDetailContainerControl �� 
TargetType  customerGridTemplate$ 	 DevExpress.Xpf.Grid.BaseColumn   Header)  #DevExpress.Xpf.Core.ObjectConverter   MinWidth   DataRowTemplate   UseLightweightTemplates1 	 +DevExpress.Xpf.Grid.UseLightweightTemplates  headerTemplate�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5       localclr-namespace:GridDemo  5      %x	  &	        &	      &	 &     7   &	 v    7  &	 �    7  &	 �    7  &	 s    7  &	     7  &	 �       5	      6     5
      6%   �� 5      .R�5   
     ._�5      �   5      �� 5      $
��Auto �6'   �� 5      $��12 �6'   �� 5      .+�5        $��80��6    $
��Left=�6.   $	��Top=�6I   $��0��6�    .   .  6a   �� $ 	Row.Photo�8   ��6�   �� $( "CellData[0].Column.ActualDataWidth�6  � 5      $��2��6   $	��400��6'     5      �� 5      $��8 �6+   �� 5       5      �� 5      $
��Auto �6(   �� 5      .+�5      �� $ 
Horizontal=�6!   $��1��6:   $��0��6J   .+�5      �� $
��Bold"�6C   ��6$   �� *Row.FirstName+6C   �� 5      $�� ��6$   �� 5       $
��Bold"�6B   ��6$   �� *Row.LastName+6B   5!      �� 5"      $��1��6    $��1��60   $
��Left=�6=   $
��Wrap=�6s   ��6X   �� *	Row.Notes+6s   5#      5$      5%      5&      �� 5'      .R�5(   
   �� 8��  ��6   �� 6-   5)      �� 5*      .R�5+   
     $ PART_GridControl��6   	 5,        5-      $
 	FirstName��6!     5.      $
 LastName��6!    50        51      $
��view��6     .   .  $ Visible=�6X   8   6{   53      54      �� 55      .R�56   
     ._�57      �   58      �� 59      $��12 �6'   �� 5:      .+�5<      �� $��1��6   $
��Wrap=�6q   ��6,   �� 8   6q   5=      5>      5?      �� 5@      .R�5A   
   �� $ Vertical=�6   .+�5B        ._�5C      �� $ defaultRowPresenter��6#   ��6@   �� ��6T   �� *View.DefaultDataRowTemplate+5D      
  5E      8��  ��6,   �� ��5F      �� 5G      " ��6&   .R�5H      �� $��0,1,0,0q�6!   #��z��6;   $��Stretch=�6g   $��Stretch=�6�   #��z��6�   ._�5I      �� 5J      5K      5M      5N      5O      �� 5P      .R�5Q   
     $ PART_GridControl��6   	 5S        5T      $
 CompanyName��6!   $ Company Name 69   $	 170��6O     5U      $
 ContactName��6!   $ Contact Name 69   $	 150��6O     5V      $
 ContactTitle��6!   $ Contact Title 6:   $	 150��6Q    5X        5Y      $
��view��6     .  8    5Z       5[      None=�6<   5]      5_      5`      �� 5a      .R�5b   
   �� $
��Bold"�6%   ��6   �� 6%   5c      5d      