   M S B A M L   `   `   `  ����   WpfApp1    WpfApp1.Window4   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Window4��$	��450��$	��800��.���  J� RenderTransformOrigin$ 
0.497,0.52&�.+��� $	��Top=�$��10q�.+��� $��Create Purchase Order��$��16(�$
��Bold"�$��0,0,0,10q��� $��Select Supplier��$��14(�$
��0,10q��� -    ��Name$ cmbSuppliers��$	��200��.@��� $��
Supplier A���� $��
Supplier B���� $��
Supplier C���� $��Select Product��$��14(�$
��0,10q��� -   $ cmbProducts��$	��200��$
��0,10q��� $��Quantity��$��14(�$
��0,10q��� -    ��Name$ txtQuantity��$	��200��$
��0,10q��� -   $��Create Purchase Order��$	��200��$��30��$
��0,10q�&   System.Windows.Controls.DataGrid  -     Name$ 
dgProducts��   AutoGenerateColumns . $��10,150,10,10q�
�������   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
Product ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *	ProductID+  $ Product Name��$ * 	 �� *ProductName+  $ Price��$ * 	 �� *Price+