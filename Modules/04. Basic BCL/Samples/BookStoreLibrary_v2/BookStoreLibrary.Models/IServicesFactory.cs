using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLibrary.Models
{
    public interface IServicesFactory
    {
        IBookService GetBookService(string storeFile);
    }
}
