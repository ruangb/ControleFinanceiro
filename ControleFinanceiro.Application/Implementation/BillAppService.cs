using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class BillAppService : IBillAppService
    {
        private readonly IBillRepository _BillRepository;
        private readonly IMapper _mapper;

        public BillAppService(IBillRepository BillRepository, IMapper mapper)
        {
            _BillRepository = BillRepository;
            _mapper = mapper;
        }

        public AppServiceResult<IEnumerable<BillDTO>> GetAll()
        {
            AppServiceResult<IEnumerable<BillDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<BillDTO>>(_BillRepository.GetAll()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<IEnumerable<BillDTO>> GetAllBills()
        {
            AppServiceResult<IEnumerable<BillDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<BillDTO>>(_BillRepository.GetAllBills()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<BillDTO> GetById(int id)
        {
            AppServiceResult<BillDTO> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<BillDTO>(_BillRepository.GetById(id)));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<int> Insert(BillDTO dto)
        {
            AppServiceResult<int> result = new();

            try
            {
                var entity = _mapper.Map<Bill>(dto);
                result.BuildSucessResult(_BillRepository.Insert(entity));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceBaseResult Update(BillDTO dto)
        {
            AppServiceBaseResult result = new();

            try
            {
                var entity = _mapper.Map<Bill>(dto);
                _BillRepository.Update(entity);
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
                _BillRepository.Delete(id);
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
