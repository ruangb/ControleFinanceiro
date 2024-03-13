namespace ControleFinanceiro.CrossCutting
{
    public class AppServiceResult<T> where T : class
    {
        public bool Sucess { get; private set; }
        public string? Message { get; private set; }
        public Exception? Exception { get; private set; }
        public object? Model { get; private set; }

        /// <summary>
        /// Monta o resultado de Erro na execução da ação
        /// </summary>
        /// <param name="obj">Model a ser retornada</param>
        public void BuildSucessResult(T obj)
        {
            Sucess = true;
            Model = obj;
        }

        /// <summary>
        /// Monta o resultado de Erro na execução da ação
        /// </summary>
        /// <param name="message">Mensagem opcional; caso não informada, a mensagem da exceção será adotada como mensagem</param>
        public void BuildErrorResult(Exception ex, string? message = null)
        {
            Sucess = false;
            Exception = ex;
            Message = message ?? ex.Message;
        }
    }
}
