   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 
ResizeMode$ CanMinimize=�65    � WindowStartupLocation$ CenterScreen=�6N   .��5	      � .+�5
   
   �� -   5        $��btnAuth��5
      $��Авторизация��6    $
��Left=�66   $��
418,10,0,0q�6Q   $	��Top=�6e   $��89��6}   6�   �� 5   
   -   5        $��	btnRegist��5      $��Регистрация��6"   $
��Left=�68   $��
328,10,0,0q�6S   $	��Top=�6g   $��75��6   6�   �� 5   
   -   5        $��tblAuth��5      $
��Left=�6#   $��	10,10,0,0q�6>   $
��Wrap=�6Q   $	��Top=�6e   $	��278��6}   $��22��6�   �� 5   
   -   5        $��lvMobilePhone��5      $
��Left=�6)   $	��248��6D   $��	10,64,0,0q�6Q   $	��Top=�6d   $	��499��6|    � ContextMenu 5      �� 5      .@�5      m� -   5        $��Delete��5      $��Удалить��6-   6>   5       �� View 5      �� 5      -   5        chartGridView��P�5      �� 5       �� Width$ 25��6)   $��Id��64    �� DisplayMemberBinding 6@   ��  �� Path$ id�6k   �� 5      $	 160��6)   $��Модель��65    6E   �� $ model�6s   �� 5      $	 190��6)   $ ��Производитель��65    6L   �� $ manufacturer� �� StringFormat$ {0:C}��6�   �� 5      $	 100��6)   $��Цена��65    6C   �� $ price�$ {0:C}��6�   5!      5&      �� 5*   
   -   5        $��Добавить��5*      $
��Left=�6$   $��	10,32,0,0q�6?   $	��Top=�6R   $��72��6j   6u   5.      5/      