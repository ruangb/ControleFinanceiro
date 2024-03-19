using System.Reflection;

namespace ControleFinanceiro.CrossCutting
{
    public class AppServiceBaseResult
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public Exception? Exception { get; protected set; }

        /// <summary>
        /// Monta o resultado de Sucesso na execução da ação
        /// </summary>
        public void BuildSucessResult()
        {
            Success = true;
        }

        /// <summary>
        /// Monta o resultado de Erro na execução da ação
        /// </summary>
        /// <param name="message">Mensagem opcional; caso não informada, a mensagem da exceção será adotada como mensagem</param>
        public void BuildErrorResult(Exception ex, string? message = null)
        {
            Success = false;
            Exception = ex;
            Message = message ?? ex.Message;
        }
    }
}
