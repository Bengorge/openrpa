﻿using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenRPA.Interfaces.Selector;

namespace OpenRPA.Interfaces
{
    public interface IPlugin : INotifyPropertyChanged
    {
        string Name { get; }
        string Status { get; }
        void Initialize();
        void Start();
        void Stop();
        event Action<IPlugin, IRecordEvent> OnUserAction;
        event Action<IPlugin, IRecordEvent> OnMouseMove;
        bool parseUserAction(ref IRecordEvent e);
        bool parseMouseMoveAction(ref IRecordEvent e);
        Selector.treeelement[] GetRootElements(Selector.Selector anchor);
        Selector.Selector GetSelector(Selector.Selector anchor, Selector.treeelement item);
        IElement[] GetElementsWithSelector(Selector.Selector selector, IElement fromElement = null, int maxresults = 1);
        void LaunchBySelector(Selector.Selector selector, TimeSpan timeout);
        void CloseBySelector(Selector.Selector selector, TimeSpan timeout, bool Force);
        bool Match(SelectorItem item, IElement m);
    }
}
