using AutoMapper;
using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Repositories.OrderProductRelationshipRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.OrderProductRelationshipService
{
    public class OrderProductRelationService : IOrderProductService
    {
        public IOrderProductRepository _orderProductRelationRepository;
        public BioProductStoreContext _context;
        private readonly IMapper _mapper;

        public OrderProductRelationService(IOrderProductRepository orderProductRelationRepository, BioProductStoreContext context, IMapper mapper)
        {
            _orderProductRelationRepository = orderProductRelationRepository;
            _context = context;
            _mapper = mapper;
        }

        public void CreateOrderProductRelation(OrderProductRegisterDTO orderProductRelation)
        {
            var orderProductRelationToCreate = _mapper.Map<OrderProduct>(orderProductRelation);
            orderProductRelationToCreate.DateCreated = DateTime.Now;
            orderProductRelationToCreate.DateModified = DateTime.Now;

            _orderProductRelationRepository.Create(orderProductRelationToCreate);
            _orderProductRelationRepository.Save();
        }

        public void UpdateOrderProductRelation(OrderProductRegisterDTO newOrderProductRelation, Guid id)
        {
            OrderProduct orderProductToUpdate = _orderProductRelationRepository.FindById(id);
            orderProductToUpdate = _mapper.Map<OrderProductRegisterDTO, OrderProduct>(newOrderProductRelation, orderProductToUpdate);
            orderProductToUpdate.DateModified = DateTime.Now;

            try
            {
                _orderProductRelationRepository.Update(orderProductToUpdate);
                _orderProductRelationRepository.Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
 }
