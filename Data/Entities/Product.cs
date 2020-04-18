using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldies.Data.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Category { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public string Title { get; set; }
    public string RecordDescription { get; set; }
    public string RecordReleaseDate { get; set; }
    public string RecordId { get; set; }
    public string Artist { get; set; }
    public DateTime ArtistFormed { get; set; }
    public string ArtistNationality { get; set; }
    public string ArtistGenre { get; set; }
    }
}
