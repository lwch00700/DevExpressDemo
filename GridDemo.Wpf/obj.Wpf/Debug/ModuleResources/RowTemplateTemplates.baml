   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a/   )DevExpress.Xpf.Core.FormatStringConverter   FormatStringConverter     FormatString,  &DevExpress.Xpf.Core.ImageDataConverter  ImageDataConverter  eMailTemplate   GridDemo.SendEmailButton � Cursor �� 	ClickMode �� 
TargetType � ColumnDefinitions F�Name �� Stretch �� Triggers �� Value	 �� Binding
 �� Path �� 
TargetName �� Property �� Value ^� 
Conditions �� Binding �� Value �� RelativeSource  DetailCoreTemplate-  'DevExpress.Xpf.Core.MeasurePixelSnapper&   DevExpress.Xpf.Editors.ImageEdit   ShowMenu%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly   Source �� 	Converter � RowDefinitions  detailContainerTemplate �� Padding  rowDetailTemplate �� Orientation ��Name3  -DevExpress.Xpf.Grid.RowDetailContainerControl  expandableRowDetailTemplate$  DevExpress.Xpf.Core.DXExpander   HorizontalExpand   VerticalExpand   
IsExpanded  rowToolTipTemplate ^� InitialShowDelay ^� HasDropShadow ^� ToolTip
  � Tag�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 5       localclr-namespace:GridDemo  5      %  &	        &	      &	 .     &	 �    7  7   &	 �	    &	 p
    7  &	 m    7  &	 �       5	      $  D��6=     5
      6   �� 5      .R�5   
     $	��4,0q�6    $��Center=�6-   $
 Hand��6H   $ Press=�6�   ��6V   �� *Value+��6p   �� *
View.Tag+��6�   �� *Value+��5      �� 5      " ��6"   .R�5      � 
������  5      �� 5      $
��Auto �6/   �� 5      $��19 �6/   .+�5      �� $
��text��6$   $��0,0,0,1q�60   #��z��6A   $��Center=�6b   F� 5      $
 path��6   $
 Fill=�6-   #��z��6<   ����� ��&���� �b&�����fS&@V�р-�&�����fS&��[� �b&��L� ��&���� ��&���� ��&�����o�&@V�р6�&�����o�&��[� ��&��L� ��&���$��Center=�6�   $��0q�6  $��Center=�6  $��8��62  $��7��6<  $��1��6G  5       5      �� 5      $ False��6Q   	 6&   �� $
 RowData.IsFocused�.a�5      �� $
 text��6M    � �� �	#FF2A6DBD�� 5      $
 path��6G    � �� �	#FF2A6DBD5      ^� 5       5      �� 5   "    6,   �� $
 RowData.IsFocused�	 False6W   �� 5    "    6,   �� $
 IsMouseOver� �� *Self+ True6w   .0�5"      �� $
 text��6M    � �� �	#FF5E9AE2�� 5#      $
 path��6G    � �� �	#FF5E9AE25$      5&      5(      5)      �� 5*      .R�5+   
     ._�5,      �  5-      �� 5.      $
��Auto �6'   �� 5/      $��12 �6'   �� 50      .+�52        $	��150��6    $
��Left=�6/   $	��Top=�6J   $��0��6�    .   .  6b   �� $
 	Row.Photo�8   ��6�   �� $(
 "CellData[0].Column.ActualDataWidth�6  � 53      $��2��6    54      �� 55      $
��Auto �6+   �� 56      $��12 �6+   �� 57       59      �� 5:      �� 5;      �� 5<      �� 5=      �� 5>      .+�5@      �� $��0��6    $��0��60   $��Name:��6=   �� 5A      $��0��6    $��1��60   $��	Position:��6=   �� 5B      $��0��6    $��2��60   $��
Hire Date:��6=   �� 5C      $��0��6    $��3��60   $��About:��6=   �� 5D      $��0��6    $��4��60   $��Address:��6=   �� 5E      $��2��6    $��0��60   ��6=   �� *Row.FirstName+�� 5F      $��2��6    $��1��60   ��6=   �� *	Row.Title+�� 5G      $��2��6    $��2��60   ��6=   �� *Row.HireDate+8  �� 5H      $��2��6    $��3��60   $
��Left=�6=   $
��Wrap=�6s   ��6X   �� *	Row.Notes+6s   �� 5I      $��2��6    $��4��60   ��6=   �� *Row.Address+5J      5K      5L      5M      �� 5N      " ��66   .R�5O   
   �� $��0,1,0,0q�6   #��z��6+   $��Stretch=�6W   $��Stretch=�6u   $ 12,12,12,12q�6�   #��z��6�   ._�5P      �� 5Q      5R      �� 5S      .R�5T   
   �� $ Vertical=�6   .+�5U        ._�5V      �� $ defaultRowPresenter��6#   ��6@   �� ��6T   �� *View.DefaultDataRowTemplate+5W        5X      8��  #���  6t   ��6`   �� 6t   5Y      5Z      �� 5[      .R�5\   
   �� $ Vertical=�6   .+�5]        ._�5^      �� $ defaultRowPresenter��6#   ��6@   �� ��6T   �� *View.DefaultDataRowTemplate+5_        5`      $
 None=�6C   $ FromTopToBottom=�6[    6   �� $
 
IsSelected�._�5a        8��  #���  6x   ��6d   �� 6x   5b      5c      5d      �� 5e      .R�5f   
   �� ��6   �� ��6/   �� *View.DefaultDataRowTemplate+$ 10��5g   R    .   5i      �� 5j      #���  65   $  Transparent��6l   $	��600��6~   ��6!   �� 6~   5l      5m      5n      