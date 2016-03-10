using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Links;

namespace Nop.Data.Mapping.Links
{
   public  class LinkMap:NopEntityTypeConfiguration<Link>
    {
       public LinkMap()
       {
           this.ToTable("Links");
           this.HasKey(t => t.Id);
           this.Property(l => l.Name).HasMaxLength(100);
           this.Property(l => l.Value).HasMaxLength(300);
       }
    }
}
