using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class SortingObject
    {
        public string? SortBy { get; set; }  // Sıralama yapılacak sütun adı
        public SortDirection? SortDirection { get; set; } // Sıralama yönü (artan veya azalan)
    }
}
