namespace Aplicacao.DTOs
{
    public class AlterarSenhaDto : UsuarioSenhaDto
    {
        public string Senha { get; set; }
        public string ConfirmacaoDeSenha { get; set; }
    }
}
