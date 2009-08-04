// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Data
{
    public interface ISubscriptionsRepository
    {
        List<Subscription> GetAll();
        Subscription Insert(Subscription s);
        bool Delete(Guid guid);
    }
}
