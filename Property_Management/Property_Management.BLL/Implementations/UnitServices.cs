using AutoMapper;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Enums;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class UnitServices : IUnitServices
    {
        private readonly IRepository<Unit> _unitRepo;
        private readonly IRepository<Lease> _leaseRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UnitServices(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unitRepo = _unitOfWork.GetRepository<Unit>();
            _leaseRepo = _unitOfWork.GetRepository<Lease>();
        }

        public async Task<bool> CreateUnitAsync(NewUnitRequest request)
        {
            if (request == null) throw new InvalidOperationException("Request body cannot be empty.");
            request.UnitId = Guid.NewGuid().ToString();
            Unit newUnit = _mapper.Map<Unit>(request);
            Unit response = await _unitRepo.AddAsync(newUnit);
            Lease newLeaseWithUnitId = await _leaseRepo.GetSingleByAsync(l => l.PropertyId == request.PropertyId);
            newLeaseWithUnitId.UnitId = request.UnitId;
            await _leaseRepo.UpdateAsync(newLeaseWithUnitId);
            if (response == null)
                throw new InvalidOperationException("Sorry something went wrong while trying to create the unit. Do try again.");
            return true;
        }

        public async Task<bool> DeleteUnitAsync(string unitId)
        {
            Unit unit = await _unitRepo.GetSingleByAsync(u => u.UnitId == unitId);
            if (unit == null)
                throw new InvalidOperationException("Unit with this Id was not found.");
            await _unitRepo.DeleteAsync(unit);
            return true;
        }

        public async Task<UnitResponse> GetUnitAsync(string unitId)
        {
            Unit unit = await _unitRepo.GetSingleByAsync(u => u.UnitId == unitId);
            if (unit == null)
                throw new InvalidOperationException("Unit with this Id was not found.");
            return new UnitResponse
            {
                UnitId = unit.UnitId,
                UnitName = unit.Name,
                UnitPrice = unit.Rent,
                UnitDescription = unit.Description,
                UnitType = unit.UnitType.GetStringValue(),
            };
        }

        public async Task<IEnumerable<UnitResponse>> GetUnitsAsync()
        {
            if (!await _unitRepo.AnyAsync())
                throw new InvalidOperationException("Unit is empty.");

            return (await _unitRepo.GetAllAsync()).Select(u => new UnitResponse
            {
                UnitId = u.UnitId,
                UnitName = u.Name,
                UnitPrice = u.Rent,
                UnitType = u.UnitType.GetStringValue(),
                UnitDescription = u.Description
            });
        }

        public async Task<UnitResponse> UpdateUnitAsync(NewUnitRequest request)
        {
            Unit unit = await _unitRepo.GetSingleByAsync(u => u.UnitId == request.UnitId);
            if (unit == null)
                throw new InvalidOperationException("Unit with this Id was not found.");
            Unit unitUpdate = _mapper.Map<Unit>(request);
            Unit updateUnit = await _unitRepo.UpdateAsync(unitUpdate);
            return new UnitResponse
            {
                UnitId = updateUnit.UnitId,
                UnitName = updateUnit.Name,
                UnitPrice = updateUnit.Rent,
                UnitType = updateUnit.UnitType.GetStringValue(),
                UnitDescription = updateUnit.Description,
                NumberOfRooms = updateUnit.NumOfBedRooms
            };
        }
    }
}
