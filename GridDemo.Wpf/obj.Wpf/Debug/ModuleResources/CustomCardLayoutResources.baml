   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae
 aDevExpress.Xpf.Printing.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a  �� MergedDictionaries �� Source2   ,DevExpress.Xpf.Grid.PrintCardMarginConverter   PrintCardMarginConverter    GridDemo.FullNameConverter  FullNameConverter  headerTemplateFullName �� Orientation �� Path  cardTemplate2&   DevExpress.Xpf.Editors.ImageEdit   Stretch   ShowMenu%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly   Source � ColumnDefinitions	 � RowDefinitions2  ,DevExpress.Xpf.Grid.CardCellContentPresenter. 	 (DevExpress.Xpf.Grid.CellContentPresenter
   NavigationIndex   	FieldName , (CustomCardLayout_PrintCardHeaderTemplate%  DevExpress.Xpf.Editors.TextEdit   IsPrintingMode'    AllowUpdateTextBlockWhenPrinting0  *DevExpress.Xpf.Printing.TextExportSettings   TextTrimming   	EditValue `� 	Converter �� Mode & "CustomCardLayout_PrintCardTemplate,  &DevExpress.Xpf.Printing.ExportSettings   
TargetType   
Background   BorderColor   BorderThickness)	  #DevExpress.Xpf.Core.ObjectConverter)
  #DevExpress.Xpf.Editors.TextEditBase 
  TextTrimming 
  PrintTextWrapping�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 5      Bdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing 
  5       localclr-namespace:GridDemo  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5        5	      �� 5
   
   $- '/GridDemo;component/Themes/generic.xaml?�6   %�  &	        &	      &	 &     &	 �     &	 �    7  &	 �    7      5      6#     5      6   �� 5      .R�5   
   �� $ 
Horizontal=�6   .+�5      �� ��6   �� $ Data.FirstName��� 5      $�� ��6   �� 5      ��6   �� $ Data.LastName�5      5      �� 5      .R�5   
   �� $ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5        $
 Fill=�6    $��70��6/   $��64��6;   $	��Top=�6F    .   .  6^   �� $ DataContext.Photo�6�   � 5      $��12,0,0,0q�6    5      �� 5      $
��Auto �6+   �� 5      $	��150 �6+   	 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      .+�5'      �� $��0��6    $��Title: ��6-   $��Center=�6<   �� 5(      $��0��6   $��1��6*   ._�5)        $
 0��67   $ Title��6K   ��6]   �� $ View.CellStyle�5*      �� 5+      $��1��6    $��First Name: ��6-   $��Center=�6A   �� 5,      $��1��6   $��1��6*   ._�5-        $
 1��67   $ 	FirstName��6K   ��6a   �� $ View.CellStyle�5.      �� 5/      $��2��6    $��Last Name: ��6-   $��Center=�6@   �� 50      $��2��6   $��1��6*   ._�51        $
 2��67   $ LastName��6K   ��6`   �� $ View.CellStyle�52      �� 53      $��3��6    $��Birth Date: ��6-   $��Center=�6A   �� 54      $��3��6   $��1��6*   ._�55        $
 3��67   $ 	BirthDate��6K   ��6a   �� $ View.CellStyle�56      �� 57      $��4��6    $��Notes: ��6-   $��Center=�6<   �� 58      $��4��6   $��1��6*   ._�59        $	��200��67   $
 4��6F   $ Notes��6Z   ��6l   �� $ View.CellStyle�5:      5;      5<      �� 5=      $��Contact Info��6   $��0,12,0,0q�6-   .��5>      �  5?      �� 5@      $
��Auto �6+   �� 5A      $��* �6+   	 5C      �� 5D      �� 5E      �� 5F      �� 5G      �� 5H      .+�5J      �� $��0��6    $��	Address: ��6-   $��Center=�6>   �� 5K      $��0��6   $��1��6*   ._�5L        $
 5��67   $ Address��6K   ��6_   �� $ View.CellStyle�5M      �� 5N      $��1��6    $��City: ��6-   $��Center=�6;   �� 5O      $��1��6   $��1��6*   ._�5P        $
 6��67   $
 City��6K   ��6\   �� $ View.CellStyle�5Q      �� 5R      $��2��6    $��Postal Code: ��6-   $��Center=�6B   �� 5S      $��2��6   $��1��6*   ._�5T        $
 7��67   $ 
PostalCode��6K   ��6b   �� $ View.CellStyle�5U      �� 5V      $��3��6    $��	Country: ��6-   $��Center=�6>   �� 5W      $��3��6   $��1��6*   ._�5X        $
 8��67   $ Country��6K   ��6_   �� $ View.CellStyle�5Y      �� 5Z      $��4��6    $��Home Phone: ��6-   $��Center=�6A   �� 5[      $��4��6   $��1��6*   ._�5\        $
 9��67   $ 	HomePhone��6K   ��6a   �� $ View.CellStyle�5]      5^      5_      5`      5a      �� 5c      .R�5d   
      .  .  $��16(�6V   $��0q�6d   $
 None=�6o    5e      `� 5f      8   .1�5g      �� $ RowData.Row.FirstName�6   $ OneWay=�6;   �� 5h      $ RowData.Row.LastName�6   $ OneWay=�6:   5i      5k      5l      �� 5m      .R�5n   
   �� $ Panel=�6   $ White��6;   .+�5o      �� $ Vertical=�6   $ Panel=�60   $ White��6V   $ 	#FF989898��6|   $ 1q�6�   ��6�   �� $? 9(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardWidth���5p      `� 5q      8   .1�5r      �� $@ :(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardMargin�6"   �� 5s      $2 ,(dxg:CardViewPrintRowPanel.IsFirstCardInRow)�6"   5t      .+�5v      �� $��	17,14,8,7q�6�   ��6!   �� $ CardHeaderData�$ OneWay=���6V   �� $H B(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardHeaderTemplate�6�   � 5w      $��
17,9,17,17q�6    5x      �� 5y      $
��Auto �6+   �� 5z      $��* �6+   .+�5|      �� $ 
Horizontal=�6!   .+�5}      �� $ Panel=�6%   $ 1q�6K   $ 	#FFCCCCCC��6r   $
��Left=�6�   $	��Top=�6�   .+�5~        $	��102��6,   $	��127��68    . $	��-10q�6�   $��Center=�6�   $��Center=�6�    6[   �� $ 	Row.Photo�6�   5      5�      �� 5�      $��1��6!   $ Vertical=�61   $��9,0,0,0q�6H   .+�5�        $ 
FIRST NAME	 6'    . ���	#FF666666 .  6k     5�       . $��0,0,0,10q�6f   $
 None=�6x   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ Row.FirstName�6�     5�      $ 	LAST NAME	 6'    . ���	#FF666666 .  6j     5�       . $��0,0,0,10q�6e   $
 None=�6w   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ Row.LastName�6�     5�      $ TITLE	 6'    . ���	#FF666666 .  6f     5�       . $��0q�6b   $
 None=�6m   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ 	Row.Title�6�   5�      5�      5�      5�      5�      5�      