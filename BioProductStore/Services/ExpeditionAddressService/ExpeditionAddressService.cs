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
    public class ExpeditionAddressService
    {
        public IExpeditionAddressRepository _deliveryAddressRepository;
        private readonly IMapper _mapper;

        public ExpeditionAddressService(IExpeditionAddressRepository deliveryAddressRepository, IMapper mapper)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
            _mapper = mapper;
        }

        public List<ExpeditionAddress> GetAllDeliveryAddresses()
        {
            List<ExpeditionAddress> deliveryAddressesList = _deliveryAddressRepository.GetAllExpeditionAddresses();

            if (deliveryAddressesList.Count == 0)
                throw new Exception("There are no Delivery Addresses");

            return deliveryAddressesList;
        }

        public ExpeditionAddress GetDeliveryAddressByDeliveryAddressId(Guid Id)
        {
            ExpeditionAddress deliveryAddress = _deliveryAddressRepository.FindById(Id);

            if (deliveryAddress == null)
                throw new Exception("Delivery Address not found");

            return deliveryAddress;
        }

        public void CreateDeliveryAddress(RegisterExpeditionAddressDTO entity)
        {
            var deliveryAddressToCreate = _mapper.Map<ExpeditionAddress>(entity);
            deliveryAddressToCreate.DateCreated = DateTime.Now;
            deliveryAddressToCreate.DateModified = DateTime.Now;

            _deliveryAddressRepository.Create(deliveryAddressToCreate);
            _deliveryAddressRepository.Save();
        }

        public void DeleteDeliveryAddressById(Guid id)
        {
            ExpeditionAddress deliveryAddress = _deliveryAddressRepository.FindById(id);

            if (deliveryAddress == null)
                throw new Exception("Delivery Address not found");

            _deliveryAddressRepository.Delete(deliveryAddress);
            _deliveryAddressRepository.Save();
        }

        public void UpdateDeliveryAddress(UpdateExpeditionAddressDTO deliveryAddress, Guid id)
        {
            ExpeditionAddress deliveryAddressToUpdate = _deliveryAddressRepository.FindById(id);

            if (deliveryAddressToUpdate == null)
                throw new Exception("Delivery Address not found");

            deliveryAddressToUpdate =
                _mapper.Map<UpdateExpeditionAddressDTO, ExpeditionAddress>(deliveryAddress, deliveryAddressToUpdate);
            deliveryAddressToUpdate.DateModified = DateTime.Now;

            try
            {
                _deliveryAddressRepository.Update(deliveryAddressToUpdate);
                _deliveryAddressRepository.Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
