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

        public PersonDTO GetById(int id)
        {
            var dto = _mapper.Map<PersonDTO>(_baseManager.GetById(id));
            return dto;
        }

        public void Insert(PersonDTO dto)
        {
            var entity = _mapper.Map<Person>(dto);
            _baseManager.Insert(entity);
        }

        public AppServiceResult<PersonDTO> Update(PersonDTO dto)
        {
            AppServiceResult<PersonDTO> result = new();

            try
            {
                var entity = _mapper.Map<Person>(dto);
                _baseManager.Update(entity);
                result.BuildSucessResult(dto);
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public void Delete(int id)
        {
            _baseManager.Delete(id);
        }
    }
}
