   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af	 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]
 YDevExpress.Mvvm.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a #  PersistentRowStateZoomOutMargin " PersistentRowStateZoomInMargin ! PersistentRowStateImageMargin  control-emboss  �� EndPoint �� 
StartPoint � SpreadMethod � Color � Offset     GridDemo.ResizeableDataRow �� 
TargetType �� Property �� Value �� 
TargetType-  'DevExpress.Xpf.Core.MeasurePixelSnapper3  -DevExpress.Xpf.Grid.RowDetailContainerControl	  Name
 �� ElementName �Name �� Path � RowDefinitions ��Name �� CornerRadius � ColumnDefinitions&   DevExpress.Xpf.Editors.ImageEdit   ShowMenu%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly   Source �� TextWrapping �� AcceptsReturn �� 
IsReadOnly2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors-   'GridDemo.CommandManagerAttachedBehavior   CanExecuteHandlerCommand%  DevExpress.Xpf.Grid.RowSplitter � Cursor % !PersistentRowStateDataRowTemplate �� Orientation9  3DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension�	  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Grid.Themes.GridRowThemeKeys, DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]] 	  ResourceKey  ZoomButtonStyle  ZoomInButtonStyle �� BasedOn  ZoomOutButtonStyle
   GridDemo.ResizeableCard � Minimum � Maximum � Value  �� Mode! ��Name" �� DecreaseSmall# �� RelativeSource$   Triggers'  !DevExpress.Mvvm.UI.EventToCommand�  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]%   	EventName+  %DevExpress.Mvvm.UI.EventToCommandBase&   Command'   CommandParameter( �� IncreaseSmall)   Stretch* � LayoutTransform+ �� ScaleX, �� ScaleY#  DevExpress.Xpf.Grid.TableView  gridView&   DevExpress.Xpf.Grid.GridViewBase-   ShowGroupedColumns& 	  DevExpress.Xpf.Grid.DataViewBase.   NavigationStyle/   ShowHorizontalLines0   	AutoWidth1   DataRowTemplate2   AllowScrollAnimation3   UseLightweightTemplates1 	 +DevExpress.Xpf.Grid.UseLightweightTemplates"  DevExpress.Xpf.Grid.CardView 	 cardView4   MaxCardCountInRow5   CardAlignment6   CardHeaderTemplate7   CardTemplate�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   	 5      Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm  
 5       localclr-namespace:GridDemo  5      %�  &	        &	       &	 @     &	 _     '   G    &	 
    &	 �    &	 �    7  &	 �	    7  '
  �
    &	 D    &		 �    u�5	      0,0,12,0q�6B   u�5
      12,0,0,0q�6A   u�5      0,0,0,0q�6?   �� 5      $  	0.5,1.002&�61   $ 
0.5,-0.002&�6F   $	 Pad=�6^   .��5   
   � $ 	#3E4C6EB3��6   $ 0Q�6)   � 5   
   $ 	#80EDF4FF��6   $ 1Q�6)   � 5   
   $ 	#3F7098DC��6   $ 0.215Q�6)   5      �� 5      "   6   .�5   
   ��  � �� 5      �� 5      "   6"   .R�5        ._�5        $	 containerControl��68   ��5      �� 5   "   " ��62   .R�5   &   �� 5   #   .��5      �� $��0,1,0,0q�6%   $��Stretch=�6�   $��Stretch=�6�   ��6?   �� *BorderBrush+$
 containerControl��._�5   "   � $ template��6'   ��69   �� $2 ,RowState.(local:ResizeableDataRow.RowHeight)� 5   &   �� 5   *   $��* �68   6E   �� 5    *   $
��Auto �68   6H   .+�5"   &   �� $ content_container2��6f   $��0��6-   $ 0��6:   $
��Auto��6K   $
��Auto��6X   ��6�   �� *
Background+$
 containerControl��._�5#   *   �  5$   .   �� 5%   2   $
��Auto �6C   �� 5&   2   $��12 �6C   �� 5'   2   .+�5)   .     $��
12,12,0,12q�6y   $
��Left=�6�   $	��Top=�6�   $��0��6�    .   . ��6<   �� $( "CellData[0].Column.ActualDataWidth� 6�   �� $ DataContext.Picture�6  �� 5*   .   $��Visible=�6;   $��2��6a   .��5+   2   �� $��0q�5,   :   $
 Wrap=�6w    .  . ��5+   :   �� $ (TextBox.Foreground)�$
 containerControl����5,   N   �� *DataContext.Description+.��6�    5-   6     5.   :    6_   �� *+)View.DataContext.CanExecuteHandlerCommand+.��5/   T   50   3   51   /   53   +   54   '     55   &   $��PART_Resizer��66   $��1��6J   $ SizeNS��6W   56   #   57      58      59      5:      5<      5=      �� 5>      .R�5?   
   �� $ Vertical=�6   .+�5@        ._�5A      �� ��6#   C� *  $ DataRowTemplate=�+5B         5C      5D      5E      �� 5F      " ��6$   .�5G   
   ��  � ��$ Stretch=�67   �� 5H   
    � ��$ Stretch=�65   �� 5I   
    � ��$ 16��6"   �� 5J   
    � ��$ 16��6#   �� 5K   
    � ��$ 2q�6$   5L      �� 5M      " ��6&   8   .�5N   
   ��  � �� 5O      �� 5P      .R�5Q      � .+�5R      �� $��2��65   $��Center=�6?   $��Stretch=�6\   ��6$   �� 6\   �� 5S      $��2��65   $��Stretch=�6@   $��Center=�6^   ��6$   �� 6^   5T      5U      5W      5X      �� 5Y      " ��6'   8   .�5Z   
   ��  � �� 5[      �� 5\      .R�5]      � .+�5^      �� $��2��65   $��Stretch=�6@   $��Center=�6^   ��6$   �� 6^   5_      5`      5b      5c      �� 5d      " 
 6   .�5e   
   ��  � �� 5f      �� 5g      " 
 6"   .R�5h      ]� $��Center=�6    $��Center=�6=   .+�5i      �� $��Slider��68   $y�	Collapsed=�6!   $	 0.2Q�6F   $ 2Q�6T    6`   �� $1 +RowState.(local:ResizeableCard.ScaleFactor)�$  TwoWay=��� 5j      $! zoomOutButton��6!   $
��Left=�68   #���   6N   #���  5k   !   #��Z" 5l   !   ��6K   �� $
 Slider����5m   !   �� *
Foreground+# �� *Self+$ 5n        5o   "   $% Click��68   & 6J   �� *+)View.DataContext.SetCursorPositionCommand+' 6�   �� $
 zoomOutButton��5q      �� 5r      $! zoomInButton��6!   $��Right=�67   #���  6N   #���  6�   #��Z( 5s   !   ��6K   �� $
 Slider����5t   !   �� *
Foreground+# �� *Self+$ 5u        5v   &   $% Click��6<   & 6N   �� *+)View.DataContext.SetCursorPositionCommand+' 6�   �� $
 zoomInButton��5x        5y      #���  6(   $
) None=�6�    .   .  6a   �� $ DataContext.Picture�* 5z      �� 5{   "   + 61   �� $ Value�$
 Slider��, 6c   �� $ Value�$
 Slider��5}      5~      5      5�      5�        5�      - . $	. Row=�6?   / .  0 . #1 �  6�   2 . 3 5�   
    5�      None=�60   5�        5�      - . $4 1��6>   $5 Center=�6T   $
. None=�6k   6 5�   
   �� 5�      .R�5�      �� $ 
Horizontal=�6   .+�5�      �� $��NoWrap=�6E   ��6    �� $ Data.Trademark�6E   �� 5�      $��NoWrap=�6A   $��8,0,0,0q�6W   ��6    �� $ 
Data.Model�6W   5�      5�      7 5�   
   �� 5�      .R�5�      
  5�      5�      5�      