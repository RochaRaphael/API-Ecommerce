﻿using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Product> Produtos { get; set; }
        public DbSet<Order> Pedidos { get; set; }
        public DbSet<Category> Categorias { get; set; }

        
    }
}
