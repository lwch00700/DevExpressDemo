   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      C&clr-namespace:GridDemo.ModuleResourcesGridDemo.ModuleResources  5          GridDemo.CardWinExplorer   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:GridDemo     5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5	      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5
      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      /md&clr-namespace:GridDemo.ModuleResources  5        � DataContext  5      8   2GridDemo.ModuleResources.CardWinExplorerDataSource  5   
    � 	Resources 5       �� MergedDictionaries �� Source!   GridDemo.DemoValuesProvider   DemoValuesProvider�� 5   
    5      �� 5      $- '/GridDemo;component/Themes/generic.xaml?�6%   �� 5      $G A/GridDemo;component/ModuleResources/CardWinExplorerTemplates.xaml?�6%   %   &	          5      6'   5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl     OptionsContent 5   
   �� 5      .��5      ��  �� Orientation$ Vertical=�6   .+�5       $  DevExpress.Xpf.Core.GroupFrame  $��Layout��6$   $��0,0,0,12q�64   .��5!      �� $ Vertical=�6%   .+�5"      	� -   5        $��rbExrtaLarge��5"   ;   $��0,0,0,4q�6*   $��Extra large icons��6O    d� 	IsChecked 6k   ��  �� Path$ DataContext.SizeType� �� ElementName$
 View��  EnumBooleanConverter	 �� 	Converter#	 [ 
 �� ConverterParameter$
 
ExtraLarge��	� 5#      -   5        $��rbLarge��5#   ;   $��0,0,0,4q�6*   $��Large icons��6J    6`   �� $ DataContext.SizeType�$
 View��#	 [ $
 Large��	� 5$      -   5        $��rbMedium��5$   <   $��0,0,0,12q�6*   $��Medium icons��6L    6c   �� $ DataContext.SizeType�$
 View��#	 [ $
 Medium��5%      5&        5'      $��Group By��6$   $��0,0,0,12q�66   .��5(      �� $ Vertical=�6%   .+�5)      	� -   5        $��rbGroupName��5)   ;   $��0,0,0,4q�6*   $
��Name��6N    6]   �� $ 
GroupIndex�$ columnNameFirst�� �� UpdateSourceTrigger$ PropertyChanged=�  BoolToIndexConverter#	 [ 	� 5*      -   5        $��rbGroupType��5*   ;   $��0,0,0,4q�6*   $
��Type��6N    6]   �� $ 
GroupIndex�$ 
columnType��$ PropertyChanged=�#	 [ 	� 5+      -   5        $��rbGroupNone��5+   <   $��0,0,0,12q�6*   $
��None��6O   $
 TrueY�6^   5,      5-      5.      5/         Content. 51   
   �  � ColumnDefinitions 52      �� 54      $
��Auto �6#   �� 55      $��3* �6#   �� 56      $��1* �6#    � RowDefinitions 58      �� 59      $
��Auto �6    �� 5:      .+�5=      (  "DevExpress.Xpf.Bars.ToolBarControl  ,  &DevExpress.Xpf.Bars.ToolBarControlBase   Caption$ _=Navigation��6!      	GlyphSize$ Small=�68      UseWholeRow .    AllowQuickCustomization .     	AllowHide .   J� 	AllowDrop .     AllowCollapse .     ShowDragWidget .     AllowCustomizationMenu .  $��2,5,5,5q�6�      ShowBackground .     Items. 5>      '  !DevExpress.Xpf.Bars.BarButtonItem  -   5         �Name$ bBack��5>   $   !  DevExpress.Xpf.Bars.BarItem   Content)	  #DevExpress.Xpf.Core.ObjectConverter$ _Back	 61      Glyph 6A   *
  $DevExpress.Xpf.Core.DXImageExtension
   
  Image1  +DevExpress.Xpf.Core.Native.DXImageConverter$ Backward_16x16.png    
LargeGlyph 6o   
  $ Backward_32x32.png    Command 6�   �� *UndoCommand+  5?      -   5        $ bForward��5?   $   $ _Forward	 64    6G   
  $ Forward_16x16.png  6t   
  $ Forward_32x32.png  6�   �� *NextCommand+  5@      -	   5        $	 bUp��5@   $   $	 _Up	 6/    6=   
  $ Up_16x16.png  6e   
  $ Up_32x32.png  6�   �� *	UpCommand+5A      %  DevExpress.Xpf.Editors.TextEdit  5B      -
   5           Name$  tbPath��5B   <   $��0,5,5,5q�6   $��1��6,   $��Stretch=�6g   )  #DevExpress.Xpf.Editors.TextEditBase!   TextWrapping$! NoWrap=�6�   }�. %  DevExpress.Xpf.Editors.BaseEdit"   
IsReadOnly" . #   Text# 6L   �� $
 Path�6�   *  $DevExpress.Xpf.Editors.SearchControl  5C      -   5        $  Name$$ tbSearch��5C   A   $��0,5,5,5q�6    $��2��61   %   NullText$% 	Search...��6S   $��Stretch=�6h   &   
SearchText& 6�   �� $ SearchString�$
 View��' �� Mode$' TwoWay=�%  DevExpress.Xpf.Grid.GridControl  5I      -   5        $
��Grid��5I   ?   $��1��6   $��3��6+   (   AutoExpandAllGroups( . ) 
 #DevExpress.Xpf.Grid.DataControlBase)   SelectionMode$	) Row=�5L      *   ItemsSource* 5I   K   �� $ Source�+   CurrentItem+ 5J      �� $ CurrentItem�$' TwoWay=�, J� InputBindings, 5M      �� 5N      
- �� Key$
- Back��6"   . �� Command. 6-   �� *UndoCommand+�� 5O      $- BrowserBack��6"   . 64   �� *UndoCommand+�� 5P      $- Enter��6"   . 6.   �� *OpenCommand+�� 5Q      / �� 	Modifiers$	/ Altf�6!   $- Enter��62   . 6>   �� *PropertyCommand+0   Columns0 5S      $  DevExpress.Xpf.Grid.GridColumn  5T      $ 
 DevExpress.Xpf.Grid.ColumnBase1   	FieldName$
1 Icon��6%     5U      -   5        2  Name$2 
columnName��5U   %   $1 FileName��69   3   GroupInterval$3 Alphabetical=�6N     5V      -   5        $2 
columnType��5V   %   $
1 Type��69   $ 
 DevExpress.Xpf.Grid.BaseColumn4   Visible4 .  6J     5W      -   5        $2 columnNameFirst��5W   %   $1 FileNameFirst��6>   5   Header$5 	File Name	 6X   4 .  $3 Alphabetical=�6|   6   View6 5Y      "  DevExpress.Xpf.Grid.CardView  5Z      -   5        $
��View��5Z   #   & 
  DevExpress.Xpf.Grid.DataViewBase7   IsColumnChooserVisible7 .  8   SeparatorThickness$8 0Q�5\      9   
CardLayout$
9 Rows=�5]      :   ShowCardExpandButton: .  ;   ShowColumnHeaders; .  <   ShowFilterPanelMode$< Never=�5`      &   DevExpress.Xpf.Grid.GridViewBase=   ShowGroupedColumns= .  >   ShowGroupPanel> .  "?   SearchPanelHighlightResults? .  @   SearchPanelAllowFilter@ . A   ShowSearchPanelMode$A Never=�5e      B   SearchColumns$B FileName��5f        cardTemplateC   CardTemplate#C [ 5g        printCardTemplateD   PrintCardTemplate#D [ 5h      E   NavigationStyle$	E Row=�5i      5j      5l      5m      5n      5o      