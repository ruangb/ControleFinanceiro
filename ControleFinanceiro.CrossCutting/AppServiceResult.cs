namespace ControleFinanceiro.CrossCutting
{
    public class AppServiceResult<T> : AppServiceBaseResult
    {
        public object? Model { get; private set; }

        /// <summary>
        /// Monta o resultado de Sucesso na execução da ação com o objeto retornado
        /// </summary>
        /// <param name="obj">Model a ser retornada</param>
        public void BuildSucessResult(T obj)
        {
            Model = obj;
            base.BuildSucessResult();
        }
    }
}
