namespace Aplicacao.DTOs
{
    public class AlterarSenhaDto : UsuarioSenhaDto
    {
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmacaoDeNovaSenha { get; set; }
    }
}
