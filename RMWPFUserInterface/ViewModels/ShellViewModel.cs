using Caliburn.Micro;
using RMWPFUserInterface.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;
        private IEventAggregator _event;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM)
        {
            _salesVM = salesVM;
            _event = eventAggregator;

            _event.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
        }
    }
}
