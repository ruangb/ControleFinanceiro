using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class PersonAppService : IBaseAppService<PersonDTO>
    {
        private readonly IBaseManager<Person> _baseManager;
        private readonly IMapper _mapper;

        public PersonAppService(IBaseManager<Person> baseManager, IMapper mapper)
        {
            _baseManager = baseManager;
            _mapper = mapper;
        }

        public AppServiceResult<IEnumerable<PersonDTO>> GetAll()
        {
            AppServiceResult<IEnumerable<PersonDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<PersonDTO>>(_baseManager.GetAll()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<PersonDTO> GetById(int id)
        {
            AppServiceResult<PersonDTO> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<PersonDTO>(_baseManager.GetById(id)));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<int> Insert(PersonDTO dto)
        {
            AppServiceResult<int> result = new();

            try
            {
                var entity = _mapper.Map<Person>(dto);
                result.BuildSucessResult(_baseManager.Insert(entity));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceBaseResult Update(PersonDTO dto)
        {
            AppServiceBaseResult result = new();

            try
            {
                var entity = _mapper.Map<Person>(dto);
                _baseManager.Update(entity);
                result.BuildSucessResult();
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceBaseResult Delete(int id)
        {
            AppServiceBaseResult result = new();

            try
            {
                _baseManager.Delete(id);
                result.BuildSucessResult();
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }
    }
}
