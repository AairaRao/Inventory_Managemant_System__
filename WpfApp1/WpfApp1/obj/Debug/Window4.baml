   M S B A M L   `   `   `  ����  WpfApp1    WpfApp1.Window4   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Window4��5   	   $	��450��6   $	��800��6&   .��5      �  J� RenderTransformOrigin$ 
0.497,0.52&�6   .+�5   
   �� $	��Top=�6   $��10q�6-   .+�5      �� $��Create Purchase Order��6   $��16(�65   $
��Bold"�6C   $��0,0,0,10q�6U   �� 5      $��Select Supplier��6   $��14(�6/   $
��0,10q�6=   �� 5      -   5         ��Name$ cmbSuppliers��5      $	��200��6-   .@�5      �� $��
Supplier A��6   �� 5      $��
Supplier B��6   �� 5      $��
Supplier C��6   5      �� 5      $��Select Product��6   $��14(�6.   $
��0,10q�6<   �� 5      -   5        $ cmbProducts��5      $	��200��6,   $
��0,10q�68   �� 5      $��Quantity��6   $��14(�6(   $
��0,10q�66   �� 5      -   5         ��Name$ txtQuantity��5      $	��200��6+   $
��0,10q�67   �� 5      -   5        $��Create Purchase Order��5      $	��200��65   $��30��6A   $
��0,10q�6M   6[   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
dgProducts��5         AutoGenerateColumns . $��10,150,10,10q�6B   
�������   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Product ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6W   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 69   �� *	ProductID+6W     5"      $ Product Name��6%   $ * 6[   	 6;   �� *ProductName+6[     5#      $ Price��6%   $ * 6N   	 64   �� *Price+6N   5%      5&      5'      