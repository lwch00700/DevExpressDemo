   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5	      1&clr-namespace:System;assembly=mscorlibSystem 5
          GridDemo.BindingToXML   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a@dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      e aDevExpress.Xpf.DemoBase.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxdb6http://schemas.devexpress.com/winfx/2008/xaml/demobase  5      "localclr-namespace:GridDemo  5      Dcollections2clr-namespace:System.Collections;assembly=mscorlib  5	      0sys&clr-namespace:System;assembly=mscorlib  5
      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5        � 	Resources  5      #   GridDemo.XmlDateTimeConverter   XmlDateTimeConverter"   GridDemo.XmlIntegerConverter  XmlIntegerConverter��%:   &	        &	        5   
   6%     5   
   6$   5      .��5      /  )DevExpress.Xpf.DemoBase.DemoModuleControl     Content. 5   
   � .+�5      %  DevExpress.Xpf.Grid.GridControl  -   5          Name$
 grid��5         Columns 5      $  DevExpress.Xpf.Grid.GridColumn  5      $ 
 DevExpress.Xpf.Grid.BaseColumn   Header)  #DevExpress.Xpf.Core.ObjectConverter$ Id 6%      Width$ 50��61   $ 
 DevExpress.Xpf.Grid.ColumnBase   Binding 6<   ��  �� Path$ [Id].InnerText� �� Mode$ TwoWay=�	 �� 	Converter#	 [ 
   EditSettings
 5      6	  0DevExpress.Xpf.Editors.Settings.SpinEditSettings	  5       	  IsFloatValue .  63   5        5      $ 
First Name 6%    69   �� $ [FirstName].InnerText�$ TwoWay=�
 5      6
  0DevExpress.Xpf.Editors.Settings.TextEditSettings
  5      6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings   	NullValue$  ��63   5        5       $ 	Last Name 6%    68   �� $ [LastName].InnerText�$ TwoWay=�
 5!      
  5"      $  ��63   5$        5%      $	 Job 6%    62   �� $ [JobTitle].InnerText�$ TwoWay=�
 5&      
  5'      $  ��63   5)        5*      $ Email Address 6%   $	 200��6<    6H   �� $ [EmailAddress].InnerText�$ TwoWay=�
 5+      
  5,      $  ��63   5.        5/      $ 	BirthDate 6%   $	 100��68    6D   �� $ [BirthDate].InnerText�$ TwoWay=�#	 [  50         View 52      #  DevExpress.Xpf.Grid.TableView  53         	AutoWidth .    ShowAutoFilterRow .    AllowScrollAnimation . 6N   55      56      57      58      