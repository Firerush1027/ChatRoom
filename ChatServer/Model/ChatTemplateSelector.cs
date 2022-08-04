using ChatServer.Viewmodel;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatServer.Model
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ClientTemplate { get; set; }
        public DataTemplate ServerTemplate { get; set; }

        public override DataTemplate SelectTemplate(object input,
          DependencyObject container)
        {
            String name = ((MessageDataViewmodel)input).ClientData.Name;
            if (name == "System")
                return ServerTemplate;
            return ClientTemplate;
        }
    }
}
