   M S B A M L   `   `   `  ����  GridDemo"    GridDemo.SQLConnectionWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5        � Title$  Configuration Window��5       � SizeToContent$ WidthAndHeight=�6"    � WindowStartupLocation$ CenterOwner=�6A    � WindowStyle$ 
ToolWindow=�6e    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6�   .��5      ��  �� Padding$ 12q�6
   ._�5      �� .+�5	      �� -   5        $��tbDescription��5	   )   $	��500��6   $��13(�6   $
��Wrap=�6>   $��0,0,0,32q�6R   � 5
      -   5        $��
DBSettings��5
   
   $��Center=�6   $��0,0,0,12q�69    � ColumnDefinitions 5      �� 5      $
��Auto �6   �� 5      $��6 �6   �� 5      $	��200 �6    � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $��4 �6   �� 5      $
��Auto �6   .+�5      �� -   5        $��label1��5   )   $��0��6   $��0��6   .��67   SQL Server:6D   %  DevExpress.Xpf.Editors.TextEdit  5      -   5        $��tbServer��5   0   $��2��6   $��0��6#   60   �� 5      -   5        $��label2��5   )   $��0��6   $��2��6   .��67   	Database:6B     5      -   5        $��
cbDatabase��5   0   $��2��6   $��2��6#   %  DevExpress.Xpf.Editors.BaseEdit	   
IsReadOnly	 . 6B   5      �� 5      
 �� Orientation$
 
Horizontal=�6   $��Center=�6)   $��0,0,0,24q�6F   .+�5      �� -   5        $��stackPanel1��5      $��Center=�6"   $
��Left=�6=   $��0,0,12,0q�6X   .��5      �� $
 Vertical=�6   .+�5      	� -   5        $��rbWindowsAuthentication��5       d� 	IsChecked$
 TrueY�63   $��0,0,0,4q�6D    	� 	GroupName$ AutentificationGroup��6U   .��6v   Windows authentication6�   	� 5      -	   5        $��rbSQLServerAuthentication��5      $ AutentificationGroup��65   .��6V   SQL Server authentication6q   5      5       � 5!      $��Center=�6    5"      �� 5#      $
��Auto �6   �� 5$      $��6 �6   �� 5%      $	��150 �6    5'      �� 5(      $
��Auto �6   �� 5)      $��4 �6   �� 5*      $
��Auto �6   .+�5,      �� -
   5        $��label6��5,   *   $��0��6   $��0��6   .��68   Login name:6E     5-      -   5        $��tbLogin��5-   ;   $��2��6   $��0��6$   )  #DevExpress.Xpf.Editors.TextEditBase   Text$ sa��61   {�. 6J   �� 5.      -   5        $��label7��5.   *   $��0��6   $��2��6   .��68   	Password:6C   ,  &DevExpress.Xpf.Editors.PasswordBoxEdit  5/      -   5        $��
tbPassword��5/   G   $��2��6*   $��2��6:   {�. 6Y   50      51      �� 52      $��Center=�6   $��0,0,0,32q�6h   ��6-   ��  �� Path$ ActualWidth� �� ElementName$ 
DBSettings��.+�53      �� -   5        $��lRecords��53      
������ $��Right=�65   $��11(�6Q   6a   ]� 54      ��. $��0,0,0,8q�6%   .+�55      �� -   5        $��label5��55      $��0,0,6,0q�6   .��6,   Record count:6;     56      -   5          Name$ 	tbRecords��56         	EditValue)  #DevExpress.Xpf.Core.ObjectConverter$ 100000 57         MaskType$ Numeric=�58         Mask$ n0��59         MaskUseAsDisplayFormat . 5:      5;      �� 5<      -   5        $��bGenerateDB��5<      $��Generate table and ��6    $��0,0,0,4q�6>   6Q   �� 5=      -   5        $��bAddRecords��5=      $��Add records and ��6    $��0,0,0,4q�6;   6N   �� 5>      -   5        $��bExit��5>      6   �� 5?       �� IsCancel . $��0��6)   $��0��63   5@      � 5A      -   5        $��progressBarCore��5A   .   $��13��6   $��0,0,0,0q�6    � Maximum$ 99Q�6E    � Minimum$ 0Q�6R   5B      5C      5D      