﻿using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            return View(orders);
        }

        public ActionResult UpdateOrder(string id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = orderService.GetOrder(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order updatedOrder, string id)
        {
            Order order = orderService.GetOrder(id);
            order.OrderStatus = updatedOrder.OrderStatus;
            orderService.UpdateOrder(order);

            return RedirectToAction("Index");
        }
    }
}