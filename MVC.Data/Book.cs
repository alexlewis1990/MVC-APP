using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Data
{

   public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public string Author { get; set; }
        public string Customer { get; set; }
        
    }
}
