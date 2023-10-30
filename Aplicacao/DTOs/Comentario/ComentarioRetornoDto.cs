namespace Aplicacao.DTOs.Comentario;

public class ComentarioRetornoDto : BaseDto
{
    public string Texto { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid NoticiaId { get; set; }
    public Guid? ModeradorResponsavelId { get; set; }
    public string MotivoRejeicao { get; set; }
    public string EstadoValidacao { get; set; }
}
