using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

namespace MyLittlePetShop.Api.Models.error
{
    public class ErrorFormat
    {
        public string Error { get; set; }
        public int Status { get; set; }
        public Object Message { get; set; }
    }
}
