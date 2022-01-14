using AutoMapper;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Repositories.ExpeditionAddressRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.ExpeditionAddressService
{
    public class ExpeditionAddressService : IExpeditionAddressService
    {
        public IExpeditionAddressRepository _expeditionAddressRepository;
        private readonly IMapper _mapper;

        public ExpeditionAddressService(IExpeditionAddressRepository expeditionAddressRepository, IMapper mapper)
        {
            _expeditionAddressRepository = expeditionAddressRepository;
            _mapper = mapper;
        }

        public List<ExpeditionAddress> GetAllExpeditionAddresses()
        {
            List<ExpeditionAddress> expeditionAddressesList = _expeditionAddressRepository.GetAllExpeditionAddress();

            if (expeditionAddressesList.Count == 0)
                throw new Exception("There are no Delivery Addresses");

            return expeditionAddressesList;
        }

        public ExpeditionAddress GetExpeditionAddressByExpeditionAddressId(Guid Id)
        {
            ExpeditionAddress expeditionAddress = _expeditionAddressRepository.FindById(Id);

            if (expeditionAddress == null)
                throw new Exception("Delivery Address not found");

            return expeditionAddress;
        }

        public void CreateExpeditionAddress(RegisterExpeditionAddressDTO entity)
        {
            var expeditionAddressToCreate = _mapper.Map<ExpeditionAddress>(entity);
            expeditionAddressToCreate.DateCreated = DateTime.Now;
            expeditionAddressToCreate.DateModified = DateTime.Now;

            _expeditionAddressRepository.Create(expeditionAddressToCreate);
            _expeditionAddressRepository.Save();
        }

        public void DeleteExpeditionAddressById(Guid id)
        {
            ExpeditionAddress expeditionAddress = _expeditionAddressRepository.FindById(id);

            if (expeditionAddress == null)
                throw new Exception("Delivery Address not found");

            _expeditionAddressRepository.Delete(expeditionAddress);
            _expeditionAddressRepository.Save();
        }

        public void UpdateExpeditionAddress(UpdateExpeditionAddressDTO deliveryAddress, Guid id)
        {
            ExpeditionAddress expeditionAddressToUpdate = _expeditionAddressRepository.FindById(id);

            if (expeditionAddressToUpdate == null)
                throw new Exception("Delivery Address not found");

            expeditionAddressToUpdate =
                _mapper.Map<UpdateExpeditionAddressDTO, ExpeditionAddress>(deliveryAddress, expeditionAddressToUpdate);
            expeditionAddressToUpdate.DateModified = DateTime.Now;

            try
            {
                _expeditionAddressRepository.Update(expeditionAddressToUpdate);
                _expeditionAddressRepository.Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }

      
    }
}
