   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae aDevExpress.Xpf.Printing.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a2   ,DevExpress.Xpf.Grid.PrintCardMarginConverter   PrintCardMarginConverter    GridDemo.FullNameConverter  FullNameConverter  headerTemplateFullName  �� Orientation �� Path " headerTemplateFullNameWithIcon  cardTemplate1 � ColumnDefinitions&   DevExpress.Xpf.Editors.ImageEdit   Stretch   ShowMenu%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly   Source&   DevExpress.Xpf.Core.DXTabControl  demoTabControl�  �DevExpress.Xpf.Core.Native.SelectorBase`2[[DevExpress.Xpf.Core.DXTabControl, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a],[DevExpress.Xpf.Core.DXTabItem, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   SelectedIndex �� Mode#  DevExpress.Xpf.Core.DXTabItem�  �DevExpress.Xpf.Core.Native.HeaderedSelectorItemBase`2[[DevExpress.Xpf.Core.DXTabControl, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a],[DevExpress.Xpf.Core.DXTabItem, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]	   Header  demoTabItem%  DevExpress.Xpf.Editors.TextEdit)	  #DevExpress.Xpf.Editors.TextEditBase"
 	  VerticalScrollBarVisibility 	  TextWrapping   	EditValue  cardTemplate2*
  $DevExpress.Xpf.DemoBase.DemoExpander 9� 
IsExpanded ) %CardTemplates_PrintCardHeaderTemplate   IsPrintingMode'    AllowUpdateTextBlockWhenPrinting0  *DevExpress.Xpf.Printing.TextExportSettings   TextTrimming `� 	Converter #	 CardTemplates_PrintCardTemplate,  &DevExpress.Xpf.Printing.ExportSettings   
TargetType   
Background   BorderColor   BorderThickness)  #DevExpress.Xpf.Core.ObjectConverter 	  TextTrimming 	  PrintTextWrapping�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      Adxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      Bdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing   5       localclr-namespace:GridDemo  5      %�  &	        &	      &	 &     &	 �     &	 �    &	 �    &	 �    7  &		 �    7      5	      6#     5
      6   �� 5      .R�5   
   �� $  
Horizontal=�6   .+�5      �� ��6   �� $ Data.FirstName��� 5      $�� ��6   �� 5      ��6   �� $ Data.LastName�5      5      �� 5      .R�5   
   �� $  
Horizontal=�6   .+�5      �� $��1q�6   ���	#FF303030$��0,1,0,1q�6A   ._�5      �� $��UniformToFill=�6;   $��16��6S   $��16��6^   ��6   �� $ 
Data.Photo�6^   5      �� 5      $��4,0,0,0q�6   $��Center=�6N   ��6)   �� $ Data.FirstName�6N   �� 5      $�� ��6   $��Center=�6!   �� 5      $��Center=�6<   ��6   �� $ Data.LastName�6<   5      5      �� 5      .R�5   
   �� $  Vertical=�6   .+�5      � $��0q�6    5      �� 5       $
��Auto �6'   �� 5!      $��* �6'   .+�5#        $��0��6    $ UniformToFill=�60   $��70��6H   $��64��6T    .   .  6_   �� $ DataContext.Photo�6�   �� 5$      $  Vertical=�6   $	��Top=�64   $��	12,0,12,0q�6L   $��1��6_   .+�5%      �� $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5&      �� $
��Bold"�6$   $��Title: ��66   �� 5'      $��CharacterEllipsis=�6L   ��6$   �� $ DataContext.Title�6L   5(      �� 5)      $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5*      �� $
��Bold"�6$   $��First Name: ��66   �� 5+      $��CharacterEllipsis=�6P   ��6$   �� $ DataContext.FirstName�6P   5,      �� 5-      $  
Horizontal=�6!   .+�5.      �� $
��Bold"�6$   $��Last Name: ��66   �� 5/      $��CharacterEllipsis=�6O   ��6$   �� $ DataContext.LastName�6O   50      51      52        53      #���  6z   $��0,12,0,0q�6�    6   �� $5 /RowState.(local:CardTemplates.SelectedTabIndex)�$ TwoWay=�.@�54        $	 Contact Info��6   #���  65   .��55      �� $  Vertical=�6!   .+�56      �� $  
Horizontal=�6%   $��0,0,0,4q�6>   .+�57      �� $
��Bold"�6(   $��	Address: ��6:   �� 58      $
��Wrap=�6R   ��6(   �� $ DataContext.Address�6R   59      �� 5:      $  
Horizontal=�6%   $��0,0,0,4q�6>   .+�5;      �� $
��Bold"�6(   $��City: ��6:   �� 5<      ��6(   �� $ DataContext.City�5=      �� 5>      $  
Horizontal=�6%   $��0,0,0,4q�6>   .+�5?      �� $
��Bold"�6(   $��Postal Code: ��6:   �� 5@      ��6(   �� $ DataContext.PostalCode�5A      �� 5B      $  
Horizontal=�6%   $��0,0,0,4q�6>   .+�5C      �� $
��Bold"�6(   $��	Country: ��6:   �� 5D      ��6(   �� $ DataContext.Country�5E      �� 5F      $  
Horizontal=�6%   .+�5G      �� $
��Bold"�6(   $��Home Phone: ��6:   �� 5H      ��6(   �� $ DataContext.HomePhone�5I      5J      5K        5L      $	 Notes��6   #���  6.   .��5M        $

 Auto=�6#   $	��Top=�6F   $ WrapWithOverflow=�6�    .  6e   �� $ DataContext.Notes�6�   5N      5O      5P      5Q      �� 5R      .R�5S   
   �� $  Vertical=�6   .+�5T      � $��0q�6    5U      �� 5V      $
��Auto �6'   �� 5W      $��* �6'   .+�5Y        $��0��6    $ UniformToFill=�60   $��70��6H   $��64��6T    .   .  6_   �� $ DataContext.Photo�6�   �� 5Z      $  Vertical=�6   $	��Top=�64   $��	12,0,12,0q�6L   $��1��6_   .+�5[      �� $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5\      �� $
��Bold"�6$   $��Title: ��66   �� 5]      $��CharacterEllipsis=�6L   ��6$   �� $ DataContext.Title�6L   5^      �� 5_      $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5`      �� $
��Bold"�6$   $��First Name: ��66   �� 5a      $��CharacterEllipsis=�6P   ��6$   �� $ DataContext.FirstName�6P   5b      �� 5c      $  
Horizontal=�6!   .+�5d      �� $
��Bold"�6$   $��Last Name: ��66   �� 5e      $��CharacterEllipsis=�6O   ��6$   �� $ DataContext.LastName�6O   5f      5g      5h      �� 5i      $��Contact Info��6   $��0,12,0,0q�6-   .��5j      �� $  Vertical=�6   .+�5k      �� $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5l      �� $
��Bold"�6$   $��	Address: ��66   �� 5m      $
��Wrap=�6N   ��6$   �� $ DataContext.Address�6N   5n      �� 5o      $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5p      �� $
��Bold"�6$   $��City: ��66   �� 5q      ��6$   �� $ DataContext.City�5r      �� 5s      $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5t      �� $
��Bold"�6$   $��Postal Code: ��66   �� 5u      ��6$   �� $ DataContext.PostalCode�5v      �� 5w      $  
Horizontal=�6!   $��0,0,0,4q�6:   .+�5x      �� $
��Bold"�6$   $��	Country: ��66   �� 5y      ��6$   �� $ DataContext.Country�5z      �� 5{      $  
Horizontal=�6!   .+�5|      �� $
��Bold"�6$   $��Home Phone: ��66   �� 5}      ��6$   �� $ DataContext.HomePhone�5~      5      5�      
  5�      $��Notes��6    $��0,12,0,0q�6�    6/   �� $4 .RowState.(local:CardTemplates.IsNotesExpanded)�$ TwoWay=�.��5�         . $

 Auto=�61   $ WrapWithOverflow=�6�   $
��Left=�6�    6T   �� $ DataContext.Notes�6�   5�      5�      5�      �� 5�      .R�5�   
      .  .  $��16(�6V   $��0q�6d   $
 None=�6o    5�      `� 5�      8   .1�5�      �� $ RowData.Row.FirstName�6   $ OneWay=�6;   �� 5�      $ RowData.Row.LastName�6   $ OneWay=�6:   5�      5�      5�      �� 5�      .R�5�   
   �� $ Panel=�6   $ White��6;   .+�5�      �� $  Vertical=�6   $ Panel=�60   $ White��6V   $ 	#FF989898��6|   $ 1q�6�   ��6�   �� $? 9(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardWidth���5�      `� 5�      8   .1�5�      �� $@ :(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardMargin�6"   �� 5�      $2 ,(dxg:CardViewPrintRowPanel.IsFirstCardInRow)�6"   5�      .+�5�      �� $��	17,14,8,7q�6�   ��6!   �� $ CardHeaderData�$ OneWay=���6V   �� $H B(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardHeaderTemplate�6�   � 5�      $��
17,9,17,17q�6    5�      �� 5�      $
��Auto �6+   �� 5�      $��* �6+   .+�5�      �� $  
Horizontal=�6!   .+�5�      �� $ Panel=�6%   $ 1q�6K   $ 	#FFCCCCCC��6r   $
��Left=�6�   $	��Top=�6�   .+�5�        $	��102��6,   $	��127��68    . $	��-10q�6�   $��Center=�6�   $��Center=�6�    6[   �� $ 	Row.Photo�6�   5�      5�      �� 5�      $��1��6!   $  Vertical=�61   $��9,0,0,0q�6H   .+�5�        $ 
FIRST NAME 6'    . ���	#FF666666 .  6k     5�       . $��0,0,0,10q�6f   $
 None=�6x   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ Row.FirstName�6�     5�      $ 	LAST NAME 6'    . ���	#FF666666 .  6j     5�       . $��0,0,0,10q�6e   $
 None=�6w   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ Row.LastName�6�     5�      $ TITLE 6'    . ���	#FF666666 .  6f     5�       . $��0q�6b   $
 None=�6m   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ 	Row.Title�6�   5�      5�      5�      5�      5�      5�      