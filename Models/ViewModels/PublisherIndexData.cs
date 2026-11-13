using Danut_Sara_Lab2.Models;
using System.Collections.Generic;

namespace Danut_Sara_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}