﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public interface IGameListener
    {
        void Notify(GameEvent gameEvent);
    }
}
