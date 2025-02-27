﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRPA.Interfaces
{
    public interface IElement
    {
        Object RawElement { get; set; }
        System.Drawing.Rectangle Rectangle { get; set; }
        string Value { get; set; }
        void Focus();
        void Click(bool VirtualClick, OpenRPA.Input.MouseButton Button, int OffsetX, int OffsetY);
        Task Highlight(bool Blocking, System.Drawing.Color Color, TimeSpan Duration);
        string ImageString();
    }
}
