using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    [Table("USERHASH")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public String Nombre { get; set; }
        [Column("USUARIO")]
        public String UserName { get; set; }
        [Column("SALT")]
        public String Salt { get; set; }
        [Column("PASS")]
        public byte[] Password { get; set; }
    }
}
