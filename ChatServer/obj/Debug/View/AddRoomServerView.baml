   M S B A M L   `   `   `  ????E  AChatServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ChatServer.ViewChatServer.View  5   	   ;"clr-namespace:ChatServer.ViewmodelChatServer.Viewmodel  5   	   3clr-namespace:ChatServer.ModelChatServer.Model  5   	   /clr-namespace:ChatServer.VMLChatServer.VML  5	   	   '    !ChatServer.View.AddRoomServerView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ChatServer.View     5   	   8	viewModel"clr-namespace:ChatServer.Viewmodel     5   	   /dataclr-namespace:ChatServer.Model     5   	   ,vmlclr-namespace:ChatServer.VML     5	   	   -   5          ?Name$  AddDialogWindow??5   	   %   ChatServer.VML.ViewModelLocator   AutoHookedUpViewModel .  ? Title$ Add Room Server Dialog??5   	   $??310.228??6(   $??307.954??69   $??Arial Rounded MT Bold)?6I   $??16(?6l   .??5      ? .+?5   
   ?? $??Host Address??6   $??91,23,90,216q?6'    J? RenderTransformOrigin$ -0.334,-0.276&?6=   $	??119??6c   $??Center=?6o   ?? 5   
   $??IP??6   $
??Left=?6   $??	21,76,0,0q?68   $	??Top=?6K   $??39??6c   $??28??6n   ?? 5   
   $
??Port??6   $
??Left=?6   $??
21,134,0,0q?6:   $	??Top=?6N   $ 0.321,0.495&?6f   ?? 5   
   $
??Left=?6+   $??28??6F   $??	77,80,0,0q?6R    ?? TextWrapping$
 Wrap=?6e   $	??Top=?6y   $	??187??6?   ??6   ?? *	InputIP+6?   ?? 5   
   $
??Left=?6-   $??28??6H   $??
77,135,0,0q?6T   $
 Wrap=?6h   $	??Top=?6|   $	??187??6?   ??6   ?? *	InputPort+6?   ?? 5   
   $??Add new chat room??6p   $
??Left=?6?   $??10q?6?   $??
68,208,0,0q?6?   $	??Top=?6?   $	??173??6?   ??6   ?? *AddServerEventCmd+??67   ??  ?? ElementName$ AddDialogWindow??6?   ?? 5   
   -   5         ??Name$ 
error_text??5      $&?? This chat room is already exist.??6%   $
??Left=?6P   $??
21,168,0,0q?6k   $	??Top=?6   $	??253??6?   
??????????	#FFDC0000y?6?   ?? *PromptVisibility+5      5      