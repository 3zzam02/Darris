using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace BestStoreMVC.Models
{
    // Models/ImageViewModel.cs
    public class Images
    {
       public int Id { get; set; }
            public string FileName { get; set; }
            public string Url { get; set; }
            public string Description { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    }

