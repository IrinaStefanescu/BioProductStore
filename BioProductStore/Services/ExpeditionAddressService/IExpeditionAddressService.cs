using BioProductStore.DTOs;
using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.ExpeditionAddressService
{
    public interface IExpeditionAddressService
    {
        public List<ExpeditionAddress> GetAllExpeditionAddresses();

        ExpeditionAddress GetDeliveryAddressByExpeditionAddressId(Guid Id);

        void CreateExpeditionAddress(RegisterExpeditionAddressDTO entity);

        void DeleteExpeditionAddressById(Guid id);

        void UpdateExpeditionAddress(RegisterExpeditionAddressDTO deliveryAddress, Guid id);
    }
}
