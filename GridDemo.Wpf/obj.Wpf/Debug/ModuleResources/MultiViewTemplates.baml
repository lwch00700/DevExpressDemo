   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a] YDevExpress.Mvvm.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a  �� MergedDictionaries �� Source(    "GridDemo.GroupNameToImageConverter   GroupNameToImageConverter/   )GridDemo.BirthdayImageVisibilityConverter $  BirthdayImageVisibilityConverter*   $GridDemo.CountryToFlagImageConverter  CountryToFlagImageConverter?  9DevExpress.Xpf.Core.BooleanToVisibilityConverterExtension   BooleanToVisibilityConverter   Invert,  &DevExpress.Xpf.Core.ImageDataConverter  ImageDataConverter  colBirthDateCellTemplate � ColumnDefinitions%  DevExpress.Xpf.Editors.DateEdit  Name �� Path �� ElementName �� 	Converter ^� ToolTip " colGroupNameGroupValueTemplate  TextBlockStyle	 �� Padding  colGroupNameCellTemplate%   GridDemo.ViewToBooleanConverter 	 ViewToBooleanConverter *
 &customTemplateWithSummaryCustomization
 �� Orientation)  #DevExpress.Xpf.Editors.ComboBoxEdit  GroupSummaryComboBoxEdit � Cursor+  %DevExpress.Xpf.Editors.LookUpEditBase   ItemsSource  DemoDataProvider �� Source%	  DevExpress.Xpf.Editors.BaseEdit 	  	EditValue �� Mode �� UpdateSourceTrigger  FormatStringConverter&
    GridDemo.CustomNodeImageSelector  imageSelector  cardTemplate&   DevExpress.Xpf.Editors.ImageEdit   Stretch   ShowMenu 	  
IsReadOnly   Source&   DevExpress.Xpf.Core.DXTabControl  demoTabControl�  �DevExpress.Xpf.Core.Native.SelectorBase`2[[DevExpress.Xpf.Core.DXTabControl, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a],[DevExpress.Xpf.Core.DXTabItem, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   SelectedIndex#  DevExpress.Xpf.Core.DXTabItem�  �DevExpress.Xpf.Core.Native.HeaderedSelectorItemBase`2[[DevExpress.Xpf.Core.DXTabControl, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a],[DevExpress.Xpf.Core.DXTabItem, DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   Header  demoTabItem � ShowGridLines � 	ResourcesB  <DevExpress.Xpf.DemoBase.Helpers.DateTimeShortPatternCoverter  Key � RowDefinitions  dateTimeConverter  colInfoHeaderTemplate ��Name ��Name(   "GridDemo.ColumnHeaderTextConverter   
ColumnName2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   Triggers'  !DevExpress.Mvvm.UI.EventToCommand�  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName+  %DevExpress.Mvvm.UI.EventToCommandBase   CommandParameter    Command! �� RelativeSource  ordersDetailHeaderTemplate  cardHeaderTemplate  ImageDataCellTemplate"  Name�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5       localclr-namespace:GridDemo  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5      Adxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      Adxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5	        5      �� 5   
   $> 8/GridDemo;component/ModuleResources/SharedResources.xaml?�6   %3  &	        &	      &	 &     &	 9     &	 S     &	 f     7  7  &	 g    7   7  &	 �    7   &		 �    &	
 �    7  7  7  &	 �    &	 �    7  7  7  &	 .    7  7  &	     &	     &	 �       5      6&     5      6-     5      6(     5       . 6T     5      6   �� 5      .R�5   
   �  5      �� 5      �� 5      $
��Auto �6#   .+�5        $ PART_Editor��6   5      � 5      $��1��6   y�6#   �� $ IsEditorActive�$ PART_Editor��8   .+�5      �� $��4,0,4,0q�6   $8��2/GridDemo;component/Images/MultiView/BirthDate.png��6)   $
��None=�6�   $
��Left=�6�   $��Center=�6  y�6e   �� $ 	EditValue�$ PART_Editor��8   5      �� 5       $��Birthday Coming��6$   5"      5#      5$      5%      �� 5&      .R�5'   
   � $��5,0,0,0q�6    5(      �� 5)      $
��Auto �6#   �� 5*      .+�5,      �� $
��None=�6x   $��Center=�6�   ��6   �� $ RowData.Row.GroupName�8    5-      �� 5.      ��6    �� $ RowData.Row.GroupName�50      �� 51      8�� $	 5,0,0,0q�6l   $��1��6~   ��6@   �� $ RowData.Row.GroupName�6~   52      53      �� 54      .R�55   
   �� $��16��6t   $��Uniform=�6   ��6   �� $ RowData.Row.GroupName�8    56      �� 57      ��6   �� $ RowData.Row.GroupName�59      5:        5;      6#   �� 5<      .R�5=   
   �� $
 
Horizontal=�6   $��Center=�6.   $��0,0,10,0q�6I   .+�5>        #���  6   $ Select summary type��6
  
���   �$
 Hand��6J   6R   �� *SummaryItemTypes+8    6�   �� $ SummaryItem.SummaryType�$ TwoWay=�$ PropertyChanged=�6J  �� 5?      $��Center=�6   $��(��63   
���   �6<     5@      #���  6   $ Select summary column��6	  
���   �$
 Hand��6K   6R   �� *SummaryFieldNames+8   6�   �� $ SummaryItem.FieldName�$ TwoWay=�$ PropertyChanged=�6K  �� 5A      $��Center=�6   $��)=��63   
���   �6=   �� 5B      $��Center=�6   
���   ���63   �� *SummaryValue+8  6�   5C      5D      
  5E      6$   �� 5F      .R�5G   
   �� $
 Vertical=�6   .+�5H      � $��0q�6    5I      �� 5J      $
��Auto �6'   �� 5K      $��* �6'   .+�5M        $��0��6    $ UniformToFill=�60   $��70��6H   $��64��6T    .   .  6_   �� $ DataContext.Photo�8   6�   �� 5N      $
 Vertical=�6   $	��Top=�64   $��	12,0,12,0q�6L   $��1��6_   .+�5O      �� $
 
Horizontal=�6!   $��0,0,0,4q�6:   .+�5P      �� $
��Bold"�6$   $��Title: ��66   �� 5Q      $��CharacterEllipsis=�6L   ��6$   �� $ DataContext.Title�6L   5R      �� 5S      $
 
Horizontal=�6!   $��0,0,0,4q�6:   .+�5T      �� $
��Bold"�6$   $��First Name: ��66   �� 5U      $��CharacterEllipsis=�6P   ��6$   �� $ DataContext.FirstName�6P   5V      �� 5W      $
 
Horizontal=�6!   .+�5X      �� $
��Bold"�6$   $��Last Name: ��66   �� 5Y      $��CharacterEllipsis=�6O   ��6$   �� $ DataContext.LastName�6O   5Z      5[      5\        5]      $	��160��6   #���  6�   $��0,12,0,0q�6�    6.   �� $5 /RowState.(local:CardTemplates.SelectedTabIndex)�$ TwoWay=�.@�5^        $ Contact Info��6   #���  65   .��5_      �� $
 Vertical=�6!   .+�5`      �� $
 
Horizontal=�6%   $��0,0,0,4q�6>   .+�5a      �� $
��Bold"�6(   $��	Address: ��6:   �� 5b      $
��Wrap=�6R   ��6(   �� $ DataContext.Address�6R   5c      �� 5d      $
 
Horizontal=�6%   $��0,0,0,4q�6>   .+�5e      �� $
��Bold"�6(   $��City: ��6:   �� 5f      ��6(   �� $ DataContext.City�5g      �� 5h      $
 
Horizontal=�6%   $��0,0,0,4q�6>   .+�5i      �� $
��Bold"�6(   $��Postal Code: ��6:   �� 5j      ��6(   �� $ DataContext.PostalCode�5k      �� 5l      $
 
Horizontal=�6%   $��0,0,0,4q�6>   .+�5m      �� $
��Bold"�6(   $��	Country: ��6:   �� 5n      ��6(   �� $ DataContext.Country�5o      �� 5p      $
 
Horizontal=�6%   .+�5q      �� $
��Bold"�6(   $��Phone: ��6:   �� 5r      ��6(   �� $ DataContext.HomePhone�5s      5t      5u        5v      $ Notes��6   #���  6.   .��5w      �  .  $	��Top=�61    5x      ��  5y      dateTimeConverter 6@   5z       5{      �� 5|      �� 5}       5      �� 5�      �� 5�      .+�5�      �� $
��Bold"�6$   	Hired at:6A   �� 5�      $��1��6$   ��65   �� $ DataContext.HireDate�8  �� 5�      $
��Bold"�6$   $��1��66   Birth Date:6Q   �� 5�      $��1��6$   $��1��61   ��6A   �� $ DataContext.BirthDate�8  5�      5�      5�      5�      5�      �� 5�      .R�5�   
   �  5�      �� 5�      $
��Auto �6#   �� 5�      �� 5�      $
��Auto �6#   �� 5�      .+�5�      �� $7��1/GridDemo;component/Images/MultiView/Contacts.png��6   $
��None=�6O   �� 5�      $ phoneBorder��6   $
 Hand��6*   $��1��68   ._�5�      �� $ tbPhone��6D   8��  $��4,0,0,0q�6U   $��Phone��6f   ��5�      �� 5�       5�        5�   "   $ 	HomePhone��6B   5�       5�        5�      $ MouseLeftButtonDown��60   $ 	HomePhone��5�   O     5�   P   �� $N H(dxg:BaseGridColumnHeader.GridColumn).DataContext.ChangeFieldNameCommand�! �� *TemplatedParent+5�   O   5�      5�      �� 5�      $��/��6   $��4,0,0,0q�6!   $��Center=�62   $��2��6M   �� 5�      $ addressBorder��6   $
 Hand��6,   $��3��6:   ._�5�      �� $ 	tbAddress��6D   8�� $��4,0,0,0q�6W   $��Address��6h   ��5�      �� 5�       5�        5�   "   $ Address��6B   5�       5�        5�      $ MouseLeftButtonDown��60   $ Address��5�   4     5�   P   �� $N H(dxg:BaseGridColumnHeader.GridColumn).DataContext.ChangeFieldNameCommand�! �� *TemplatedParent+5�   4   5�      5�      5�      5�      �� 5�      .R�5�   
   �� $
 
Horizontal=�6   $��24��6.   .+�5�      �� $��0,0,3,0q�6   $5��//GridDemo;component/Images/MultiView/Orders.png��6%   $��Center=�6^   �� 5�      $��Orders��6   $��Center=�6&   5�      5�      �� 5�      .R�5�   
   �� $
 
Horizontal=�6   .+�5�      �� $��NoWrap=�6=   ��6   �� $ Data.FirstName�6=   �� 5�      $��NoWrap=�6<   $��8,0,0,0q�6R   ��6   �� $ Data.LastName�6R   5�      5�      �� 5�      .R�5�   
     $" PART_Editor��6   $ Uniform=�6-   $��70��6?    . 6K   5�      5�      