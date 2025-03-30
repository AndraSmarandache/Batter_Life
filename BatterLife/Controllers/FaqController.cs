using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;
using System.Collections.Generic;

namespace BatterLife.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Index()
        {
            var faqs = new List<FaqItem>
            {
                new FaqItem
                {
                    Question = "How do I place an order?",
                    Answer = "You can place an order directly through our website by selecting the products you want and adding them to your cart. Once you're ready, proceed to checkout and fill in your delivery details."
                },
                new FaqItem
                {
                    Question = "What are your delivery options?",
                    Answer = "We offer both pickup and delivery services. Delivery fees and times may vary depending on your location. You can check the available options during checkout."
                },
                new FaqItem
                {
                    Question = "Do you offer gluten-free or vegan options?",
                    Answer = "Yes, we have a variety of gluten-free and vegan desserts. Please check the product descriptions or contact us for more details."
                },
                new FaqItem
                {
                    Question = "How can I track my order?",
                    Answer = "Once your order is confirmed, you will receive a tracking link via email or SMS. You can use this link to check the status of your delivery."
                },
                new FaqItem
                {
                    Question = "What is your return policy?",
                    Answer = "Due to the perishable nature of our products, we do not accept returns. However, if there is an issue with your order, please contact us immediately, and we will do our best to resolve it."
                },
                new FaqItem
                {
                    Question = "Can I customize my order?",
                    Answer = "For custom orders or special requests, please <a href='/Contact'>contact us</a>. You can call us or send a message with your specific requirements, and we'll be happy to assist you."
                }
            };

            return View(faqs);
        }
    }
}