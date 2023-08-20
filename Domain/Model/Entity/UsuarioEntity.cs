using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIPedidos.Domain.Model.Entity
{
    [Table("usuarios")]
    [Index(nameof(Login), IsUnique = true)]
    public class UsuarioEntity
    {
        private long? _id;
        private string? _nome;
        private string? _login;
        private string? _senha;
        private DateTime? _dataCadastro;
        [Key]

        public long? Id { get => _id; set => _id = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public string? Login { get => _login; set => _login = value; }
        public string? Senha { get => _senha; set => _senha = value; }
        public DateTime? DataCadastro { get => _dataCadastro; set => _dataCadastro = value; }
    }
}
