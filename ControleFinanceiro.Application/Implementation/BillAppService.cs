using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class BillAppService : IBillAppService
    {
        private readonly IBillManager _billManager;
        private readonly IMapper _mapper;

        public BillAppService(IBillManager billManager, IMapper mapper)
        {
            _billManager = billManager;
            _mapper = mapper;
        }

        public AppServiceResult<IEnumerable<BillDTO>> GetAll()
        {
            AppServiceResult<IEnumerable<BillDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<BillDTO>>(_billManager.GetAll()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<IEnumerable<BillDTO>> GetAllBills(BillDTO dto)
        {
            AppServiceResult<IEnumerable<BillDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<BillDTO>>(_billManager.GetAllBills(dto.IdPerson, dto.IdCreditCard, dto.StartDueDate, dto.OnlyThirds)));
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
                result.BuildSucessResult(_mapper.Map<BillDTO>(_billManager.GetById(id)));
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
                result.BuildSucessResult(_billManager.Insert(entity));
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
                _billManager.Update(entity);
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
                _billManager.Delete(id);
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
