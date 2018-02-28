using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Midterm.FileManipulator
{
    interface IFileManipulator <T>
    {
        string Path { get; set; }
        List<T> GetCollection();
        T ConvertItem(string item);
        void WriteToFile(T t);
    }
}
