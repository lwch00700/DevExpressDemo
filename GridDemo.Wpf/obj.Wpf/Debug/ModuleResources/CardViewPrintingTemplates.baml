   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5	      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]
 YDevExpress.Mvvm.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae aDevExpress.Xpf.Printing.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a   iconTemplate_Picture&    DevExpress.Xpf.Editors.ImageEdit     Stretch    ShowMenu%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly    Source �� Path2  ,DevExpress.Xpf.Grid.PrintCardMarginConverter  PrintCardMarginConverter0  *DevExpress.Mvvm.UI.ObjectToObjectConverter   SelectedIndexToBoolConverter   DefaultTarget
   Map   DevExpress.Mvvm.UI.MapItem   Source   Target  PrintCardHeaderTemplate%  DevExpress.Xpf.Editors.TextEdit	   IsPrintingMode'
    AllowUpdateTextBlockWhenPrinting   	EditValue �� Mode  PrintCardTemplate,  &DevExpress.Xpf.Printing.ExportSettings   
TargetType   
Background �� Orientation   BorderColor   BorderThickness `� 	Converter � ColumnDefinitions)  #DevExpress.Xpf.Core.ObjectConverter0  *DevExpress.Xpf.Printing.TextExportSettings   TextTrimming)	  #DevExpress.Xpf.Editors.TextEditBase 	  TextTrimming 	  PrintTextWrapping  DefaultCardViewPrintStyle �� 
TargetType"
  DevExpress.Xpf.Grid.CardView �� Property �� Value �� ElementName  UniformCardSizePrintStyle  CardView_PrintCardTemplate�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm  
 5      Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  5      Bdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing   5       localclr-namespace:GridDemo  5	      %�  &	        &	 �     &	 �     &	     &	 �    7  &	 s    &	 	    7  7  &	 �
    7  �� 5
      .R�5   
      $	��120��66   $	��120��6B   $��Center=�6O   $��8,3,0,3q�6l   $  Uniform=�6}    .   .  6   �� $ Value�6�   5        5      6#     5      $ False��6J   . 5   
     $ 0��6   $
 True��6$   5      �� 5      .R�5   
     	 . 
 .  $��16(�6�   $��0q�6�    6-   �� $ Value�$ OneWay=�6�   5      �� 5      .R�5   
   �� $ Panel=�6   $ White��6;   .+�5      �� $ Vertical=�6   $ Panel=�60   $ White��6V   $ 	#FF989898��6|   $ 1q�6�   ��6�   �� $? 9(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardWidth���5      `� 5      8   .1�5      �� $@ :(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardMargin�6"   �� 5      $2 ,(dxg:CardViewPrintRowPanel.IsFirstCardInRow)�6"   5      .+�5       �� $��	17,14,8,7q�6�   ��6!   �� $ CardHeaderData�$ OneWay=���6V   �� $H B(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardHeaderTemplate�6�   � 5!      $��
17,9,17,17q�6    5"      �� 5#      $
��Auto �6+   �� 5$      $��* �6+   .+�5&      �� $ 
Horizontal=�6!   .+�5'      �� $ Panel=�6%   $ 1q�6K   $ 	#FFCCCCCC��6r   $
��Left=�6�   $	��Top=�6�   .+�5(         $	��102��6,   $	��127��68   	 . $��-3q�6�   $��Center=�6�   $��Center=�6�    6[   �� $ Row.ImageData�6�   5)      5*      �� 5+      $��1��6!   $ Vertical=�61   $��9,0,0,0q�6H   .+�5,        $ 	JOB TITLE 6'   	 . ���	#FF666666
 .  6j     5-      	 . $��0,0,0,10q�6e   $
 None=�6w   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ Row.JobTitle�6�     5.      $ ADDRESS 6'   	 . ���	#FF666666
 .  6h     5/      	 . $
 None=�6i   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ Row.AddressLine1�6�     50      	 . $��0,0,0,10q�6a    6'   �� $ Row.City�6a     51      $ PHONE 6'   	 . ���	#FF666666
 .  6f     52      	 . $��0q�6b    6'   �� $ 	Row.Phone�6b   53      54      55      56      57      �� 59      " 
 6.   .�5:   
   ��  �@
 PrintAutoCardWidth 6/   �� $ 	IsChecked�$ cePrintAutoCardWidth��$ OneWay=��� 5;   
    �@
 PrintMaximumCardColumns 64   �� $ 	EditValue�$ sePrintMaximumCardColumns��$ OneWay=�5<      �� 5>      " 
 6.   .�5?   
   ��  �@
 PrintCardHeaderTemplate8   64   �� 5@   
    �@
 PrintCardTemplate8  6.   �� 5A   
    �@
 PrintAutoCardWidth . 6/   �� 5B   
    �@
 PrintMaximumCardColumns$ 2��64   �� 5C   
    �@
 PrintCardMargin$ 5,0,0,5q�6,   5D      �� 5F      .R�5G   
   �� $ Panel=�6   $ White��6;   .+�5H      �� $ Vertical=�6   $ Panel=�60   $ White��6V   $ 	#FF989898��6|   $ 1q�6�   ��6�   �� $? 9(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardWidth���5I      `� 5J      8   .1�5K      �� $@ :(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardMargin�6"   �� 5L      $2 ,(dxg:CardViewPrintRowPanel.IsFirstCardInRow)�6"   5M      .+�5O      �� $��	17,14,8,7q�6�   ��6!   �� $ CardHeaderData�$ OneWay=���6V   �� $H B(dxg:CardViewPrintingHelper.PrintCardInfo).PrintCardHeaderTemplate�6�   � 5P      $��
17,9,17,17q�6    5Q      �� 5R      $
��Auto �6+   �� 5S      $��* �6+   .+�5U      �� $ 
Horizontal=�6!   .+�5V      �� $ Panel=�6%   $ 1q�6K   $ 	#FFCCCCCC��6r   $
��Left=�6�   $��Center=�6�   .+�5W         $	��102��6,   	 . $��-3q�6r   $��Center=�6~   $��Center=�6�    6N   �� $ Row.Icon�6�   5X      5Y      �� 5Z      $��1��6!   $ Vertical=�62   $��9,0,0,0q�6I   .+�5[        $ CATEGORY 6'   	 . ���	#FF666666
 .  6i     5\      	 . $��0,0,0,10q�6e   $
 None=�6w   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�    6'   �� $ Row.Category�6�     5]      $ 	TRADEMARK 6'   	 . ���	#FF666666
 .  6j     5^      	 . $
 None=�6f   $	��Top=�6�   $
 None=�6�   $
 WrapX�6�   $��0,0,0,10q�6�    6'   �� $ Row.Trademark�6�     5_      $ MODEL 6'   	 . ���	#FF666666
 .  6f     5`      	 . $��0q�6b    6'   �� $ 	Row.Model�6b   5a      5b      5c      5d      5e      5f      