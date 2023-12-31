﻿using AbilitySystem.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AbilitySystem.BL;

public class OrdersManager : IOrdersManager
{
    private readonly IOrdersRepo _orderRepo;

    public OrdersManager(IOrdersRepo orderRepo)
    {
        _orderRepo = orderRepo;
    }


    #region GET
    public List<OrderReadDto> GetAll()
    {
        List<Order> ordersFromDb = _orderRepo.GetAll();

        return ordersFromDb
            .Select(o => new OrderReadDto
            (
                o.OrderId,
                o.OrderStatus,
                o.OrderDate,
                o.UserId!,
                o.TotalPrice

            ))
            .ToList();
    }


    public OrderWithProductsReadDto? GetByIdWithProducts(int id)
    {
        Order? order = _orderRepo.GetByIdWithProducts(id);

        if (order == null) { return null; }

        return new OrderWithProductsReadDto
        {
            OrderDate = order.OrderDate,
            UserId = order.UserId!,
            TotalPrice = order.TotalPrice,
            OrderStatus = order.OrderStatus,
            Products = order.OrderProducts.Select(p => new OrderProductReadDto
            (
                p.ProductId,
                p.ProductQuantity
            )).ToList()
        };
    }

    public List<OrderByUserReadDto>? GetByUserWithProducts(string userId)
    {
        List<Order>? orders = _orderRepo.GetByUserWithProducts(userId);

        if (orders == null || orders.Count == 0) { return null; }

        List<OrderByUserReadDto> result = orders.GroupBy(order => order.OrderId).Select(group =>
        {
            Order order = group.First(); 
            return new OrderByUserReadDto
            {
                OrderId = order.OrderId,
                Products = order.OrderProducts.Select(orderProduct => new OrderProductNameReadDto
                (
                    orderProduct.Product!.Name,
                    orderProduct.Product.ProductId
                )).ToList(),
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                OrderStatus = order.OrderStatus
            };
        }).ToList();
        return result;
    }
    #endregion

    #region ADD
    public int Add(OrderAddDto orderDto)
    {
        var order = new Order
        {
            UserId = orderDto.UserId,
            OrderStatus = OrderStatus.Pending,
            OrderDate = DateTime.Now,
            TotalPrice = orderDto.TotalPrice
        };

        var orderProducts = orderDto.Products.Select(p => new OrderProduct
        {
            ProductId = p.ProductId,
            ProductQuantity = p.ProductQuantity
        }).ToList();

        order.OrderProducts = orderProducts;

        _orderRepo.Add(order);
        _orderRepo.SaveChanges();

        return order.OrderId;
    }
    #endregion

    #region EDIT
    public void Edit(int id, OrderEditDto orderDto)
    {
        Order? orderToEdit = _orderRepo.GetById(id);
        if (orderToEdit == null) { return; }

        orderToEdit.OrderStatus = orderDto.OrderStatus;


        _orderRepo.Update(orderToEdit);
        _orderRepo.SaveChanges();

    }
    #endregion

    #region DELETE
    public bool Delete(int id)
    {
        Order? order = _orderRepo.GetById(id);

        if (order is null) { return false; }

        _orderRepo.Delete(order);
        _orderRepo.SaveChanges();
        return true;
    }
    #endregion

    #region Count
    public int CountAll()
    {
        return _orderRepo.CountAll();
    }

    public int CountAccepted()
    {
        return _orderRepo.CountAccepted();
    }

    public int CountRejected()
    {
        return _orderRepo.CountRejected();
    }

    public int CountPending()
    {
        return _orderRepo.CountPending();
    }
    #endregion


}
