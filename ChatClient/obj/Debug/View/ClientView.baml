   M S B A M L   `   `   `  ????E  AChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ChatClient
ChatClient  5      1clr-namespace:ChatClient.ViewChatClient.View  5      /clr-namespace:ChatClient.VMLChatClient.VML  5           ChatClient.View.ClientView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:ChatClient     5      /viewsclr-namespace:ChatClient.View     5      ,vmlclr-namespace:ChatClient.VML     5      %   ChatClient.VML.ViewModelLocator    AutoHookedUpViewModel  . .??5      ? .+?5   
   ?? $??Connect to Server??69   $
??Left=?6U   $??	10,10,0,0q?6p   $	??Top=?6?   $	??201??6?   $??39??6?   ???	#FFB0B0B0$
??Bold"?6?   $??Arial Black)?6?   $??14(?6?   
?????????6   ?? *OpenServerDialogCmd+??6  Z? ? 5   
   $
??Left=?6   $	??399??6*   $??
216,10,0,0q?67   $	??Top=?6K   $	??695??6c   .+?5      ?  ? ColumnDefinitions 5      ?? 5      ?? 5      $	??195 ?6'   .+?5      ?? $??
0,0,10,363q?63   $??Center=?6G   $??Center=?6d   ??6   ?? *RoomIP+5      ?? 5      $??Disconnect Room??6A   $
??Left=?6[   $	??Top=?6v   $	??185??6?   $??39??6?   
???????$
??Bold"?6?   $??Arial Black)?6?   $??14(?6?   ???	#FFB0B0B0$??1??6   ??6   ?? *ChangeRoomStatusCmd+??6	  Z? 6   5      ?? 5      -   5        $??	bdrFilter??5   N   ???	#FF838383$??1q?6-   $	??400??6A   $	??Top=?6_   $??	0,0,10,-1q?6w   ._?5      ?? $??
0,37,182,0q?6   .+?5      "   ChatClient.View.ChatroomView  -   5          Name$ ChatroomControl??5   )   $	??321??6D   ? 5      $??40??6   .+?5      ?? $
??10,5q?6"   $
??Left=?6L   $??Center=?6g   $	??424??6?   $??40??6?   ??61   ?? *	InputText+6?   ?? 5      $
??Send??6D   $??	424,0,0,0q?6S   ??6!   ?? *SendMessageCmd+{?6f   ?? *IsBtnEnable+6?   5      5      5       $   ChatClient.View.OnlineUserView  5!      -   5          Name$ OnlineUserControl??5!   7   $??
502,36,0,0q?6#   67   5"      $   ChatClient.View.ServerListView  5#   
   -   5          Name$ ServerViewControl??5#      5$      5%      