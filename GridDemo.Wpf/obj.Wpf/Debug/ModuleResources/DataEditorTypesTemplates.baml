   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DevExpress.DemoDataDevExpress.DemoData  5      #clr-namespace:GridDemoGridDemo  5
      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ai eDevExpress.Xpf.PropertyGrid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a    GridDemo.DemoDataProvider   DemoDataProvider)   #DevExpress.DemoData.NWindDataLoader  NWindDataLoader8  2DevExpress.Xpf.Editors.BytesToImageSourceConverter  imageConverter  displayTemplate  ��Name �� Path �� RelativeSource �� 	Converter  FilterHeaderContentTemplate  comboBoxItemTemplate �� Orientation  gridBeveragesTemplate%  DevExpress.Xpf.Grid.GridControl  Name   Columns$  DevExpress.Xpf.Grid.GridColumn$ 
 DevExpress.Xpf.Grid.ColumnBase   	FieldName$ 
 DevExpress.Xpf.Grid.BaseColumn   Header)  #DevExpress.Xpf.Core.ObjectConverter	   HeaderToolTip
   MinWidth   Width   AllowResizing] YDevExpress.Data.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter   Fixed   DisplayTemplate#   AutoFilterRowDisplayTemplate    AutoFilterRowEditTemplate!   FilterEditorHeaderTemplate   HeaderTemplate   EditSettings:	  4DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings8
  2DevExpress.Xpf.Editors.Settings.ButtonEditSettings 
  IsTextEditable<  6DevExpress.Xpf.Editors.Settings.LookUpEditSettingsBase   ValueMember   DisplayMember   ItemsSource �� Source   ItemTemplate6  0DevExpress.Xpf.Editors.Settings.TextEditSettings   EditNonEditableTemplate   View#  DevExpress.Xpf.Grid.TableView   ShowAutoFilterRow&   DevExpress.Xpf.Grid.GridViewBase   ShowGroupedColumns   	AutoWidth  NumericTextEdit%  DevExpress.Xpf.Editors.TextEdit   MaskType    Mask!   MaskUseAsDisplayFormat  TextEdit 	 AutoCompleteComboBoxEdit)  #DevExpress.Xpf.Editors.ComboBoxEdit+  %DevExpress.Xpf.Editors.LookUpEditBase"   AutoComplete'  !DevExpress.Xpf.Editors.ButtonEdit#   IsTextEditable$   ImmediatePopup%   ItemsSource 
 TokenComboBoxEdit&   ValueMember'   DisplayMember%  DevExpress.Xpf.Editors.BaseEdit(   StyleSettings7  1DevExpress.Xpf.Editors.TokenComboBoxStyleSettings  
LookUpEdit+  %DevExpress.Xpf.Grid.LookUp.LookUpEdit*  $DevExpress.Xpf.Editors.PopupBaseEdit)   PopupContentTemplate*   IncrementalFiltering+   AutoPopulateColumns,   IsPopupAutoWidth-   
PopupWidth  SearchLookUpEdit.   PopupHeight/   DisplayTemplate)  #DevExpress.Xpf.Editors.TextEditBase0   EditNonEditableTemplate1   FilterCondition2   StyleSettings>  8DevExpress.Xpf.Grid.LookUp.SearchLookUpEditStyleSettings  ComboBoxEdit3   ItemTemplate  SpinEdit%  DevExpress.Xpf.Editors.SpinEdit4   	Increment5   MinValue6   IsFloatValue  	CheckEdit&   DevExpress.Xpf.Editors.CheckEdit  RegExTextEdit  TrackBarEdit)  #DevExpress.Xpf.Editors.TrackBarEdit7   TickPlacement  ProgressBarEdit,  &DevExpress.Xpf.Editors.ProgressBarEdit8   ContentDisplayMode  ZoomTrackBarEdit9   StyleSettings6  0DevExpress.Xpf.Editors.TrackBarZoomStyleSettings  RangeTrackBarEdit: � UseLayoutRounding7  1DevExpress.Xpf.Editors.TrackBarRangeStyleSettings  MemoEdit%  DevExpress.Xpf.Editors.MemoEdit  DateEdit%   DevExpress.Xpf.Editors.DateEdit;   	NullValue< �� Member  ListBoxEdit(!  "DevExpress.Xpf.Editors.ListBoxEdit= !  ItemsSource> !  
ItemsPanel? � Orientation@ !  StyleSettings:"  4DevExpress.Xpf.Editors.RadioListBoxEditStyleSettings  
ButtonEditA   AllowDefaultButtonB   Buttons'#  !DevExpress.Xpf.Editors.ButtonInfo+$  %DevExpress.Xpf.Editors.ButtonInfoBaseC $  	ClickModeD #  	GlyphKindE #  Command  PopupColorEdit+%  %DevExpress.Xpf.Editors.PopupColorEditF %  Color  PopupBrushEdit0&  *DevExpress.Xpf.PropertyGrid.PopupBrushEdit  SparklineEdit*'  $DevExpress.Xpf.Editors.SparklineEdit7(  1DevExpress.Xpf.Editors.AreaSparklineStyleSettings  PasswordBoxEdit,)  &DevExpress.Xpf.Editors.PasswordBoxEdit  PopupCalcEdit**  $DevExpress.Xpf.Editors.PopupCalcEditG   MaskCultureH *  	Precision  PopupImageEdit++  %DevExpress.Xpf.Editors.PopupImageEditI +  ShowMenuMode  FontEdit%,  DevExpress.Xpf.Editors.FontEdit   BarCodeEdit(-  "DevExpress.Xpf.Editors.BarCodeEditJ -  ShowTextK -  StyleSettings1.  +DevExpress.Xpf.Editors.Code128StyleSettings�/  �DevExpress.Xpf.Editors.Code128BarCodeStyleSettings`1[[DevExpress.XtraPrinting.BarCode.Code128Generator, DevExpress.Printing.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]L /  CharacterSet�0  �DevExpress.Xpf.Editors.Internal.XamlEnumTypeConverter`1[[DevExpress.XtraPrinting.BarCode.Code128Charset, DevExpress.Printing.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      Adxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      *dxdd!clr-namespace:DevExpress.DemoData  5      Fdxprg:http://schemas.devexpress.com/winfx/2008/xaml/propertygrid  5	       localclr-namespace:GridDemo  5
      .sys&clr-namespace:System;assembly=mscorlib  5      %�  &	        &	      &	 &     &	 9     7  &	 �     &	     7  &	 ~    7  7  7  7  7  7  7  &	 T    &	 �    &		     7  &	
 �    7  &	 k    7  &	 f	    7  &	 z
    7  7  &	     &	 �    &	 �    &	 L    &	 �    &	 �    &	 ^    &	 �    &	     &	 �    7   &	 t    &	 C    &	 �    &	 �    &	 5    &	 t    &	 �    &	 ;    &	  z       5      6     5      6     5      6&   �� 5      .R�5   
   �� $  PART_Editor��6   $��1q�6%   ��60   �� $ SelectedItem.Picture� �� *TemplatedParent+8   5      �� 5      .R�5   
   �� ��6   �� 5      �� 5      .R�5   
   �� $ 
Horizontal=�6   
������ .+�5      �� $��17��6   $��17��6    ��6+   �� $2 ,(dxe:BaseEdit.OwnerEdit).SelectedItem.Icon17� �� *TemplatedParent+8   �� 5      $��2q�6   $��Center=�6�   ��6#   �� $8 2(dxe:BaseEdit.OwnerEdit).SelectedItem.CategoryName� �� *TemplatedParent+6�   5      5      �� 5      .R�5   
     $ PART_GridControl��6    5        5      $ 
CategoryID��6!   $ Category 68   $	 Category��6J   $
 36��6c   $ 36��6q   $ False 6|   $
 None=�6�   8   8  8  8   5"      �� 5#       5%      	  5&      $ FalseX�5'   %   $ 
CategoryID��5(   %   $ CategoryName��5)   %    5*   %   �� $ 
Categories�8   5+      �� 5,   "   .R�5-   &   �� $ 
Horizontal=�61   .+�5.   *   �� $��30��60   ��6;   �� $ Picture�8  �� 5/   *   $��2,0,0,0q�64   ��6F   �� $ CategoryName�50   '   51   #    53      �� 54   "   .R�55   &   �� $  PART_Editor��6,   $��30��6A   $
��Left=�6L   $��1q�6g   ��6r   �� $3 -(dxe:BaseEdit.OwnerEdit).SelectedItem.Picture�8  56   #   58      5:        5;      $ CategoryName��6!     5<      $ Description��6!    5>        5?      $
��view��6     .  .  . 6_   5A      5B      �� 5D      .R�5E   
     $��PART_Editor��6   $��Right=�6*   $ Numeric=�6M   $  d��6`   ! . 6i   5F      �� 5G      .R�5H   
     $��PART_Editor��6   5I      �� 5J      .R�5K   
     $��PART_Editor��6   " . $
# TrueX�5L   3   $ . % 5K   .   �� *	Countries+8   5L   I   5M      �� 5N      .R�5O   
     $��PART_Editor��6   $& 
CategoryID��5P      $' CategoryName��5Q      " . $ . % 5O   .   �� *
Categories+8   ( 5T        5U      5W      5X      �� 5Y      .R�5Z   
     $��PART_Editor��5[      $& 
CategoryID��5]      $' CategoryName��5^      #) �  5_      " . $
* TrueX�5a      $ . + .  , .  $	- 300Q�5d   2   % 5\      �� *
Categories+8   5d   2   5e      �� 5f      .R�5g   
     $��PART_Editor��5h      $& 
CategoryID��5j      $	. 400Q�62   $	- 500Q�6D   #/ �  5k      #0 �  5l      #) �  5m      + .  $1 ContainsX�5o      % 5i      �� *
Categories+8   2 5p        5q      5s      5t      �� 5u      .R�5v   
     $��PART_Editor��6   $& 
CategoryID��5w      $# FalseX�68   #/ �  6O   #0 �  6�   % 5v   .   �� *
Categories+8   3 5x      �� 5y      .R�5z      �� $ 
Horizontal=�6!   
������ .+�5{      �� $��17��6    $��17��6,   ��67   �� $ Icon17�8  �� 5|      $��2q�6G   ��6$   �� $ CategoryName�6G   5}      5~      5�      5�      �� 5�      .R�5�   
     $��PART_Editor��6   $  p��6*   ! . $
4 0.01~�6Q   $5 0X�6b   6 . 6o   5�      �� 5�      .R�5�   
     $��PART_Editor��6   5�      �� 5�      .R�5�   
     $��PART_Editor��6   $ RegEx=�6*   $  \d{5}-\d{3}-\d{4}��6;   ! . 6T   5�      �� 5�      .R�5�   
     $��PART_Editor��6   $
7 None=�6.   5�      �� 5�      .R�5�   
     $��PART_Editor��6   $8 Value=�61   5�      �� 5�      .R�5�   
     $��PART_Editor��6   $
7 None=�6.   9 5�        5�      5�      5�      �� 5�      .R�5�   
     $��PART_Editor��6   $
7 None=�6.   : . 9 5�        5�      5�      5�      �� 5�      .R�5�   
     $��PART_Editor��6   5�      �� 5�      .R�5�   
      $��PART_Editor��6   ; 5�      �� 5�      $< sys:DBNull.Value��6   5�      5�      �� 5�      .R�5�   
   !  $��PART_Editor��6   = 6-   �� *PalleteSizes+8   > 5�      �� 5�      .R�5�      � $��60��6    $? Vertical=�6,   5�      @ 5�      "  5�      5�      5�      �� 5�      .R�5�   
     $��PART_Editor��6   $A FalseX�6,   .B 5�      #  $C Release=�6   $D Regular=�6m   E 61   �� *)'View.DataContext.ButtonEditClickCommand+6m   5�      5�      �� 5�      .R�5�   
   %  $��PART_Editor��6   $F White��60   5�      �� 5�      .R�5�   
   &  $��PART_Editor��6   5�      �� 5�      .R�5�   
   '  $��PART_Editor��6   ( 5�      (  5�      5�      5�      �� 5�      .R�5�   
   )  $��PART_Editor��6   5�      �� 5�      .R�5�   
   *  $��PART_Editor��6   $  c��6/   $G en-US��68   $H 4��6L   ! . 6Z   5�      �� 5�      .R�5�   
   +  $��PART_Editor��6   $I Always=�60   5�      �� 5�      .R�5�   
   ,  $��PART_Editor��6   5�      �� 5�      .R�5�   
   -  $��PART_Editor��6   J .  $��10q�6>   K 5�      .  5�      $L CharsetC0 6+   5�      5�      5�      