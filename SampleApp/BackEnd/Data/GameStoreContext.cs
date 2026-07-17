namespace BackEnd.Data;

using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>(); 

    public DbSet<Genre> Genres => Set<Genre>();
}