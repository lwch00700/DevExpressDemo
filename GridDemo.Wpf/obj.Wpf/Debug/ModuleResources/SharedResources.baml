   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a,   &DevExpress.Xpf.Core.ImageDataConverter   ImageDataConverter%   GridDemo.HeaderToImageConverter  HeaderToImageConverter/   )GridDemo.BooleanToDefaultBooleanConverter $  booleanToDefaultBooleanConverter   GridDemo.CountriesData  CountriesData  TextBlockStyle  �� 
TargetType �� Property �� Value  colCountryItemTemplate � ColumnDefinitions �� Path �� 	Converter  colCountryCellTemplate)  #DevExpress.Xpf.Editors.ComboBoxEdit  Name+  %DevExpress.Xpf.Editors.LookUpEditBase   ValueMember'  !DevExpress.Xpf.Editors.ButtonEdit   IsTextEditable%  DevExpress.Xpf.Editors.BaseEdit	   DisplayTemplate)  #DevExpress.Xpf.Editors.TextEditBase
   EditNonEditableTemplate   ItemsSource �� Source   ItemTemplate  colCountryNameCellTemplate  ColumnHeaderTemplate!	  DevExpress.Xpf.Bars.BarItem 	  UseAsBarItemGlyph ��Name�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5       localclr-namespace:GridDemo  5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      %  &	        &	      &	 &     &	 9     &	 L     &	 �     7   7  &	 t    7  7  7  7   7  &	 k    7  7  7  7   7  &	 \    7  7     5      6     5      6#     5	      6-     5
      6   �� 5      "  ��6#   .�5   
   ��  � ��$ CharacterEllipsis=�5   ;   5      �� 5   
    � ��$ Center=�6.   5      �� 5      .R�5   
   � 
������  5      �� 5      $
��Auto �6#   �� 5      .+�5      �� $��4,0,4,0q�6   $��Uniform=�6�   $��16��6�   $
��Left=�6�   $��Center=�6�   ��6%   �� $0 *(dxe:BaseEdit.OwnerEdit).SelectedItem.Flag�8   6�   �� 5      8�� $��4,0,0,0q�6@   $��1��6�   ��6Q   �� $0 *(dxe:BaseEdit.OwnerEdit).SelectedItem.Name�6�   5      5      �� 5      .R�5   
     $ PART_Editor��6   $ 
ActualName��5   /   $ FalseX�6H   8	   8
   5   0   �� $ 
DataSource�8   5      �� 5      .R�5       �  5!      �� 5"      $
��Auto �6/   �� 5#      .+�5%      �� $��4,0,4,0q�6    $��Uniform=�6}   $��16��6�   $
��Left=�6�   $��Center=�6�   ��61   �� $
 Flag�8  6�   �� 5&      8�� $��4,0,0,0q�6L   $��1��6x   ��6]   �� $
 Name�6x   5'      5(      5*      5+      �� 5,      .R�5-   
     $ PART_Editor��6   $
 Name��5.   /   $ FalseX�6B   8	   8
   5-   0   �� $ 
DataSource�8   5/      �� 50      .R�51      �  52      �� 53      $
��Auto �6/   �� 54      .+�56      �� $��4,0,4,0q�6    $��Uniform=�6}   $��16��6�   $
��Left=�6�   $��Center=�6�   ��61   �� $
 Flag�8  6�   �� 57      8�� $��4,0,0,0q�6L   $��1��6x   ��6]   �� $
 Name�6x   58      59      5;      5<      �� 5=      .R�5>   
   �  5?      �� 5@      $
��Auto �6#   �� 5A      .+�5C      �� $
��None=�6Y   $��Center=�6h    . $��0,0,4,0q�6�   ��6   �� 8   6�   �� 5D      $ tbPhone��6   8�� $��1��6b   ��6Q   �� 6b   5E      5F      5G      