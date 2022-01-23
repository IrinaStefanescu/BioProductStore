using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BioProductStore.DataAccess;
using BioProductStore.DTOs;
using BioProductStore.Models;
using Microsoft.Data.SqlClient;

namespace BioProductStore.Services.ExpeditionAddressService
{
    public class ExpeditionAddressService : IExpeditionAddressService
    {
        private UnitOfWork _uow;
        private readonly IMapper _mapper;

        public ExpeditionAddressService(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public List<ExpeditionAddress> GetAllExpeditionAddresses()
        {
            var expeditionAddressesList = _uow.ExpeditionAddress.GetAllAsQueryable();

            if (expeditionAddressesList.Count() == 0)
                throw new Exception("There are no Delivery Addresses");

            return expeditionAddressesList.ToList();
        }

        public ExpeditionAddress GetExpeditionAddressByExpeditionAddressId(Guid Id)
        {
            ExpeditionAddress expeditionAddress = _uow.ExpeditionAddress.FindById(Id);

            if (expeditionAddress == null)
                throw new Exception("Delivery Address not found");

            return expeditionAddress;
        }

        public void CreateExpeditionAddress(RegisterExpeditionAddressDTO entity)
        {
            var expeditionAddressToCreate = _mapper.Map<ExpeditionAddress>(entity);
            expeditionAddressToCreate.DateCreated = DateTime.Now;
            expeditionAddressToCreate.DateModified = DateTime.Now;

            _uow.ExpeditionAddress.Create(expeditionAddressToCreate);
            _uow.SaveChanges();
        }

        public void DeleteExpeditionAddressById(Guid id)
        {
            ExpeditionAddress expeditionAddress = _uow.ExpeditionAddress.FindById(id);

            if (expeditionAddress == null)
                throw new Exception("Delivery Address not found");

            _uow.ExpeditionAddress.Delete(expeditionAddress);
            _uow.SaveChanges();
        }

        public void UpdateExpeditionAddress(UpdateExpeditionAddressDTO deliveryAddress, Guid id)
        {
            ExpeditionAddress expeditionAddressToUpdate = _uow.ExpeditionAddress.FindById(id);

            if (expeditionAddressToUpdate == null)
                throw new Exception("Delivery Address not found");

            expeditionAddressToUpdate =
                _mapper.Map<UpdateExpeditionAddressDTO, ExpeditionAddress>(deliveryAddress, expeditionAddressToUpdate);
            expeditionAddressToUpdate.DateModified = DateTime.Now;

            try
            {
                _uow.ExpeditionAddress.Update(expeditionAddressToUpdate);
                _uow.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }

      
    }
}
