   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.Window5   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  Window5��5   	   $	��450��6   $	��800��6&   .��5	      � .+�5   
   �� $	��Top=�6   $��10q�6-   .+�5      �� $��Create Sales Order��6   $��16(�62   $
��Bold"�6@   $��0,0,0,10q�6R   �� 5      $��Select Customer��6   $��14(�6/   $
��0,10q�6=   �� 5      -   5        $��cmbCustomers��5      $	��200��6+   $
��0,10q�67   .@�5      �� .��6   
Customer A6+   �� 5      .��6   
Customer B6+   �� 5      .��6   
Customer C6+   5      �� 5      $��Select Product��6   $��14(�6.   $
��0,10q�6<   �� 5      -   5        $��cmbProducts��5      $	��200��6*   $
��0,10q�66   .@�5      �� .��6   	Product A6*   �� 5      .��6   	Product B6*   �� 5      .��6   	Product C6*   5      �� 5      $��Quantity��6   $��14(�6(   $
��0,10q�66   �� 5       -   5        $��txtQuantity��5       $	��200��6)   $
��0,10q�65   �� 5#      -   5        $��Create Sales Order��5#      $	��200��62   $��30��6>   $
��0,10q�6J   6X   5$      &   System.Windows.Controls.DataGrid  5'   
   -   5        $��dgSalesOrders��5'         AutoGenerateColumns .    CanUserAddRows .  $��10,150,10,10q�6Z   
�������   Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ Order ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *	OrderID+6S     5*      $ Customer��6%   $ * 6T    67   �� *
Customer+6T     5+      $ Product��6%   $ * 6R    66   �� *	Product+6R     5,      $ Quantity��6%   $ * 6T    67   �� *
Quantity+6T     5-      $ Status��6%   $ * 6P    65   �� *Status+6P   5/      50      51      