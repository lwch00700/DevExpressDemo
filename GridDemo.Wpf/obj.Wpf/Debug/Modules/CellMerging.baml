   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      9!clr-namespace:DevExpress.DemoDataDevExpress.DemoData  5          GridDemo.CellMerging   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:GridDemo     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5	      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5
      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      *dd!clr-namespace:DevExpress.DemoData  5        � DataContext  5      )   #DevExpress.DemoData.IssueDataLoader  5   
    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $C =/GridDemo;component/ModuleResources/CellMergingTemplates.xaml?�6%   %    5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl     OptionsContent 5   
   �� 5      .+�5      &   DevExpress.Xpf.Editors.CheckEdit     Content)  #DevExpress.Xpf.Core.ObjectConverter$ Allow Cell Merging 6i   $��0,0,0,5q�6�      	IsChecked 6    ��  �� Path$ AllowCellMerge� �� ElementName$
 view��	 �� Mode$	 TwoWay=�6�   �� 5      $��Print Preview��6   ��61   �� $ Commands.ShowPrintPreview�$
 view��5      
   Content.
 5   
   %  DevExpress.Xpf.Grid.GridControl  -   5          Name$
 grid��5      ) 
 #DevExpress.Xpf.Grid.DataControlBase   ItemsSource 6(   �� $ Items�   Columns 5      $  DevExpress.Xpf.Grid.GridColumn  5       $ 
 DevExpress.Xpf.Grid.ColumnBase   	FieldName$ 	ProjectID��6!   $	 
 DevExpress.Xpf.Grid.BaseColumn 	  Header$ Project Name 67    	  Width$	 200��6M      	SortIndex$ 0��6Y      EditSettings 5!      :
  4DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings
  5"      <  6DevExpress.Xpf.Editors.Settings.LookUpEditSettingsBase   ValueMember$ ID��6Y      DisplayMember$
 Name��6j   8  2DevExpress.Xpf.Editors.Settings.ButtonEditSettings   IsTextEditable$ FalseX�6      ItemsSource 63   �� $ Projects�6   5$        5%      $
 Type��6!   $	 100��62   $ 1��6>    5&      
  5'      $ FalseX�6m    63   ,  &DevExpress.Xpf.Editors.EnumItemsSource     EnumType$ dd:IssueTypeL�6m   5)        5*      $ Priority��6!   $	 100��66   $ 2��6B      priorityPrintStyle   PrintCellStyle# [  6P    5+      
  5,      &   ApplyItemTemplateToSelectedItem . $ FalseX�6�   6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!   HorizontalContentAlignment$ Stretch=�5-   3     priorityTemplate   ItemTemplate# [ 6X    5,   3     $ dd:IssuePriorityL�   AllowImages .  5-   X   5/        50      $ Status��6!   $	 100��64    51      
  52      $ FalseX�6j    63     $ 	dd:StatusL�6j   54        55      $
 Name��6!   $	 200��62     56      $ CreatedDate��6!   $	 100��69     57      $ OwnerID��6!   $ Owner 65   $	 150��6D      AllowCellMerge$ FalseX�6P    58      
  59      $ ID��6V   $ FullName��6g   $ FalseX�6�    63   �� $ Users�6�   5;         View 5=      #  DevExpress.Xpf.Grid.TableView  5>      -   5          Name$
 view��5>           AllowCellMerge  . !   	AutoWidth! . "   FormatConditions" 5?      ) 
 #DevExpress.Xpf.Grid.FormatCondition  5@      - 
 'DevExpress.Xpf.Grid.FormatConditionBase#   	FieldName$# Status��6.   $   
Expression$$ [Status]='New'��6A     	newFormat1 
 +DevExpress.Xpf.Grid.ExpressionConditionBase%   Format#% [ 6]     5A      $
# Name��6.   $$ [Status]='New'��6?   #% [ 6[     5B      $# Status��6.   $$ [Status]='Fixed'��6A     fixedFormat#% [ 6_     5C      $
# Name��6.   $$ [Status]='Fixed'��6?   #% [ 6]   5E      5G      5H      5I      