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
        private SimpleContainer _container;
        private IEventAggregator _event;

        public ShellViewModel(SimpleContainer container, IEventAggregator eventAggregator, SalesViewModel salesVM)
        {
            _container = container;
            _salesVM = salesVM;
            _event = eventAggregator;

            _event.Subscribe(this);
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
        }
    }
}
