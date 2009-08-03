using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Data
{
    public interface IStreamDataRepository
    {
        StreamData Insert(StreamData s); 
        List<StreamData> InsertAll(List<StreamData> s);
    }
}
