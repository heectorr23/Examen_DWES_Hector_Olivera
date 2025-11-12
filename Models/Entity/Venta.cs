using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoCRUD2.Models.Entity
{
    [Table("venta")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int Id { get; set; }
        
        [Column("producto")]
        public string Producto { get; set; } = string.Empty;
        
        [Column("cantidad")]
        public int Cantidad { get; set; }
        
        [Column("precio")]
        public decimal Precio { get; set; }
    }
}
